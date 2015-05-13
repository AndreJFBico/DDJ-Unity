using UnityEngine;
using System.Collections;
using Includes;
using System;

public class Player : Agent {

    public Transform staminabar;
    public Transform staminabar_background;
    private GUIManager gui;
    private float damageTimer = 0.0f;
    private bool timerRunning = false;
    private CharacterMove characterMoveScrpt;
    private bool alphaed = false;

    protected Func<float> stamina;
    protected Func<float> maxStamina;
    protected Func<float, float> addStamina;
    protected Func<float> maxHealth;
    protected Func<float, float> setMaxHealth;
    protected Func<float> health;
    protected Func<float, float> addHealth;
    protected Func<float> damage;
    protected Func<float> defence;
    protected Func<float> waterResist;
    protected Func<float> earthResist;
    protected Func<float> fireResist;

    protected MultiplierManager multiplierManager;

    private float previousSpeed = 0;

    private Transform lastCollidedWith = null;

    void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 60;
        GameManager.Instance.sceneInit();
        OilPuddleManager.Instance.sceneInit();
        ChestManager.Instance.sceneInit();
        gui = GameObject.Find("GUI").GetComponent<GUIManager>();
        stamina = () => { return GameManager.Instance.Stats.stamina; };
        maxStamina = () => { return GameManager.Instance.Stats.maxStamina; };
        addStamina = (a) => { return GameManager.Instance.Stats.stamina += a; };
        maxHealth = () => { return GameManager.Instance.Stats.maxHealth; };
        setMaxHealth = (a) => { float diff = a - GameManager.Instance.Stats.maxHealth;
                                GameManager.Instance.Stats.maxHealth = a;
                                GameManager.Instance.Stats.health += a;
                                return 1;};
        health = () => { return GameManager.Instance.Stats.health; };
        addHealth = (a) => { return GameManager.Instance.Stats.health += a; };
        damage = () => { return GameManager.Instance.Stats.damage; };
        defence = () => { return GameManager.Instance.Stats.defence; };
        waterResist = () => { return GameManager.Instance.Stats.waterResist; };
        earthResist = () => { return GameManager.Instance.Stats.earthResist; };
        fireResist = () => { return GameManager.Instance.Stats.fireResist; };
        multiplierManager = GetComponent<MultiplierManager>();
        characterMoveScrpt = transform.gameObject.GetComponent<CharacterMove>();
        GameManager.Instance.Stats.dumpStats();
        InvokeRepeating("blink", 0f, 0.10f);
        StartCoroutine("regenStamina");
    }

    void Start()
    {
        FloatingText.Instance.sceneInit();
    }

    protected void Update()
    {
        if (timerRunning)
            damageTimer += Time.deltaTime;
        if (damageTimer >= GameManager.Instance.Stats.damageTimer)
        {
            timerRunning = false;
            damageTimer = 0.0f;
        }
        if ((lastCollidedWith != null && !lastCollidedWith.gameObject.activeSelf) || lastCollidedWith == null)
        {
            characterMoveScrpt.setInContactWithEnemy(false);     
        }
        //else if (lastCollidedWith == null)
        //{
        //    characterMoveScrpt.setInContactWithEnemy(false);
        //}
    }

    void blink()
    {
         if(timerRunning)
         {
            if(!alphaed)
            {
                GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.50f);
                alphaed = true;
            }
            else
            {
                alphaed = false;
                GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1.0f);
            }
         }
         else
         {
             GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1.0f);
         }
    }


    #region OnCollision Handlers
    //TODO ATTENTION THIS SHOULD NOT BE HERE, this should either be implemented in the enemies themselves or in the corresponding projectile behaviours
    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.CompareTo("Enemy") == 0)
        {
            takeDamageFromEnemyContact(collision.gameObject);
        }
        characterMoveScrpt.CollisionEnter(collision);
        lastCollidedWith = collision.transform;
        base.OnCollisionEnter(collision);
    }

    public override void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag.CompareTo("Enemy") == 0)
        {
            takeDamageFromEnemyContact(collision.gameObject);
        }
        characterMoveScrpt.CollisionStay(collision);
    }

    public override void OnCollisionExit(Collision collision)
    {
        characterMoveScrpt.CollisionExit(collision);
        base.OnCollisionExit(collision);
    } 
    #endregion

    private void takeDamageFromEnemyContact(GameObject obj)
    {
        if (timerRunning) return;
        EnemyScript agent = obj.GetComponent<EnemyScript>();
        agent.dealDamage(this);
    }

    public override void applyStatusEffect(StatusEffect scrpt)
    {
        scrpt.applyStatusEffect(this);
    }

    public override bool isHurt()
    {
        return health() < maxHealth();
    }

    public bool isTired()
    {
        return (stamina() <= 1.7f);
    }

    public void consumeStamina(float val)
    {
        if((stamina() - val) < 0)
        {
            addStamina(-stamina());
        }
        else addStamina(-val);
    }

    IEnumerator regenStamina()
    {
        while(true)
        {
            if (!characterMoveScrpt.isDoubleTapping())
            {
                if ((stamina() + 0.1f) > maxStamina())
                {
                    addStamina(maxStamina() - stamina());
                }
                else addStamina(0.1f);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
   
    public override void takeDamage(float amount, Elements type, bool goTroughBlink)
    {
        if (health() <= 0) return;
        if (health() - amount > maxHealth()) return;

        if (timerRunning)
        {
            if (isBlinking() && !goTroughBlink)
            {
                return;
            }
        }
        else if(!timerRunning && !goTroughBlink)
        {
            timerRunning = true;          
        }

        calculateDamageTaken(amount, type);

        checkIfDead();
    }

    public override void healSelf(float amount, Elements type)
    {
        float heal = 0;
        Color color = Color.white;
        switch (type)
        {
            case Elements.NEUTRAL:
                heal = amount;
                color = Color.white;
                break;

            case Elements.FIRE:
                heal = amount * (1 - ((defence() * 0.1f + fireResist()) / 100.0f));
                color = Color.red;
                break;

            case Elements.EARTH:
                heal = amount * (1 - ((defence() * 0.1f + earthResist()) / 100.0f));
                color = Color.green;
                break;

            case Elements.WATER:
                heal = amount * (1 - ((defence() * 0.1f + waterResist()) / 100.0f));
                color = Color.blue;
                break;

            default:
                break;
        }

        if (health() + heal > maxHealth())
            heal = maxHealth() - health();
        addHealth(heal);
        FloatingText.Instance.createFloatingText(transform, "+" + (int)heal + "", color);
    }


    private bool isBlinking()
    {
        return damageTimer < GameManager.Instance.Stats.damageTimer;
    }

    private void calculateDamageTaken(float amount, Elements type)
    {
        switch (type)
        {
            case Elements.NEUTRAL:
                addHealth(-amount * (1 - (defence() / 100.0f)));
                break;

            case Elements.FIRE:
                addHealth(-amount * (1 - ((defence() + fireResist()) / 100.0f)));
                break;

            case Elements.EARTH:
                addHealth(-amount * (1 - ((defence() + earthResist()) / 100.0f)));
                break;

            case Elements.WATER:
                addHealth(-amount * (1 - ((defence() + waterResist()) / 100.0f)));
                break;

            default:
                break;
        }
    }

    private void checkIfDead()
    {
        if (health() <= 0)
        {
            transform.FindChild("Sprite").gameObject.SetActive(false);
            transform.FindChild("Rotator").gameObject.SetActive(false);
            transform.FindChild("RIP").gameObject.SetActive(true);
            characterMoveScrpt.playerDead();
            gui.playerDeath();
        }
    }

    protected virtual void OnGUI()
    {
        float percentage = health() / maxHealth();
        if (health() < 0) addHealth(-health());
        healthbar.transform.localScale = new Vector3(percentage, 1.0f, 1.0f);

        float staminaPercentage = stamina() / maxStamina();
        staminabar.transform.localScale = new Vector3(staminaPercentage, 1.0f, 1.0f);
    }

    public override void slowSelf(float intensity)
    {
        characterMoveScrpt.Slow *= intensity;
    }

    public override void restoreMoveSpeed(float intensity)
    {
        characterMoveScrpt.Slow /= intensity;
    }

    public override void createFloatingText(string message)
    {
        FloatingText.Instance.createFloatingText(transform, message, Color.yellow);
    }

    public override void createFloatingText(string message, float time)
    {
        FloatingText.Instance.createFloatingText(transform, message, Color.yellow, time);
    }

    public void increaseMultiplier(int inc)
    {
        multiplierManager.increaseMultiplier(inc);
    }

    public Vector3 getCharacterMoveDirection()
    {
        return characterMoveScrpt.getCharacterDirection();
    }
}
