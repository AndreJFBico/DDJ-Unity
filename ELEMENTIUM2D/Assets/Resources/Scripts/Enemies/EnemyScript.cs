using UnityEngine;
using System.Collections;
using Includes;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyScript : Agent
{  
    protected Elements _type;
    protected bool isAlerted = false;
    protected float rangedRadius;

    protected float maxHealth;
    protected float health;
    protected float damage;
    protected float defence;
    protected float fireResist;
    protected float waterResist;
    protected float earthResist;
    protected float visionRadius;

    protected EnemySpawner spawnScript;
    protected PathAgent pathAgent;
    protected GameObject gui;
    protected GameObject myGui;
    public Transform alertedSign;

    protected int multiplier;

    private bool inCombat = false;
    private float outOfCombatTimer = 2f;
    private float curOutOfCombatTimer = 2f;

	// Use this for initialization
    protected virtual void Awake()
    {
        base.Awake();
        gameObject.name = this.GetType().Name;
        Vector2 targetPos = Camera.main.WorldToScreenPoint(transform.position);
        pathAgent = GetComponentInChildren<PathAgent>();
        centerHealthBar = true;
        gui = GameObject.Find("GUI");
        myGui = transform.FindChild("Ui").gameObject;
        multiplier = 1;
	}

    protected virtual void LateUpdate()
    {
        transform.position.Set(transform.position.x, 0.1f, transform.position.z);
    }

    public override void OnCollisionStay(Collision collision) { base.OnCollisionStay(collision); }
    public override void OnCollisionExit(Collision collision) { base.OnCollisionExit(collision); }
    public override void OnCollisionEnter(Collision collision) { base.OnCollisionEnter(collision); }

    public override void applyStatusEffect(StatusEffect scrpt)
    {
        scrpt.applyStatusEffect(this);
    }

    #region Damage, Heal and Regen
    public override bool isHurt()
    {
        return health < maxHealth;
    }

    //Currently enemies do not blink some of them will in the future!
    public override void takeDamage(float amount, Elements type, bool goTroughBlink, string source)
    {
        float totalDamage = 0;
        Color color = Color.white;
        switch (type)
        {
            case Elements.NEUTRAL:
                totalDamage = amount * (1 - ((defence + (waterResist + earthResist + fireResist) * 0.1f) / 100.0f));
                color = Color.white;
                break;

            case Elements.FIRE:
                totalDamage = amount * (1 - ((defence * 0.1f + fireResist) / 100.0f));
                color = Color.red;
                break;

            case Elements.EARTH:
                totalDamage = amount * (1 - ((defence * 0.1f + earthResist) / 100.0f));
                color = Color.green;
                break;

            case Elements.WATER:
                totalDamage = amount * (1 - ((defence * 0.1f + waterResist) / 100.0f));
                color = Color.blue;
                break;

            default:
                break;
        }
        health -= totalDamage;
        FloatingText.Instance.createFloatingText(transform, "" + (int)totalDamage + "", color);
        setInCombat();
        if (health <= 0)
        {
            LoggingManager.Instance.getEntry(typeof(NumTypeEnemieAndAbility)).writeEntry(gameObject.name, _type, source, type);
            GameManager.Instance.Player.resetKillTimer();
            GameManager.Instance.Player.increaseMultiplier(multiplier);
            Eliminate();
        }
        if (health >= maxHealth)
            health = maxHealth;

        GameManager.Instance.Player.resetHitTimer();
        

        updateGUI();
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
                heal = amount * (1 - ((defence * 0.1f + fireResist) / 100.0f));
                color = Color.red;
                break;

            case Elements.EARTH:
                heal = amount * (1 - ((defence * 0.1f + earthResist) / 100.0f));
                color = Color.green;
                break;

            case Elements.WATER:
                heal = amount * (1 - ((defence * 0.1f + waterResist) / 100.0f));
                color = Color.blue;
                break;

            default:
                break;
        }

        if (health + heal > maxHealth)
            heal = maxHealth - health;
        health += heal;
        FloatingText.Instance.createFloatingText(transform, "+" + (int)heal + "", color);

        updateGUI();
    }

    private void setInCombat()
    {
        if (!inCombat)
        {
            inCombat = true;
            StopCoroutine("outOfCombatRegen");
            StartCoroutine("outOfCombat");
        }
        curOutOfCombatTimer = outOfCombatTimer;
    }

    private IEnumerator outOfCombat()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            curOutOfCombatTimer -= Time.deltaTime;
            if (curOutOfCombatTimer < 0)
            {
                inCombat = false;
                break;
            }
        }
        StartCoroutine("outOfCombatRegen");
    }

    private IEnumerator outOfCombatRegen()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            healSelf(maxHealth / 10, Elements.NEUTRAL);
            if (health == maxHealth)
                break;
            updateGUI();
        }
    }
    
    #endregion

    void updateGUI()
    {
        if(health != maxHealth)
            healthbar_background.gameObject.SetActive(true);
        else healthbar_background.gameObject.SetActive(false);

        float percentage = health / maxHealth;
        healthbar.transform.localScale = new Vector3(percentage, 1.0f, 1.0f);
    }

    private void uniformValues(Transform obj)
    {
        obj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        obj.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public override void Init()
    {
        Enable();
    }

    public override void Enable()
    {
        gameObject.SetActive(true);
    }

    public override void Disable()
    {
        gameObject.SetActive(false);
    }

    public override void push(Vector3 forwardVector, float strength)
    {
        base.push(forwardVector, strength);
        pathAgent.push(forwardVector, strength);
    }

    public virtual void Eliminate()
    {
        if (spawnScript != null)
        {
            health = maxHealth;
            restart();
            spawnScript.despawn(transform);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void restart()
    {
        pathAgent.restart(false);
        StopAllCoroutines();
    }

    // Is initiated by the spawner
    public void setSpawner(EnemySpawner scrpt)
    {
        spawnScript = scrpt;
    }

    public float getDamage()
    {
        return damage;
    }

    public Elements getElementType()
    {
        return _type;
    }

    public override void setAlerted(bool val)
    {
        if (val)
            alertedSign.transform.gameObject.SetActive(true);

        else
            alertedSign.transform.gameObject.SetActive(false);
        pathAgent.setAlerted(val);
        isAlerted = val;
    }

    public override bool getAlerted()
    {
        return isAlerted;
    }

    public void stop()
    {
        if (pathAgent != null)
            pathAgent.stop();
    }

    public void restart(bool retarget)
    {
        if(pathAgent != null)
            pathAgent.restart(retarget);
    }

    public bool inRangeWithPlayer()
    {
        return (Vector3.Distance(GameManager.Instance.Player.transform.position, transform.position) <= rangedRadius);
    }

    public override void playerSighted()
    {
        if (pathAgent != null)
        {
            PlayerStats.setPlayerInCombat();
            setAlerted(true);
            pathAgent.playerSighted(GameManager.Instance.Player.transform);
            pathAgent.stopChasing();
        }
    }

    public override void slowSelf(float intensity)
    {
        pathAgent.slowSelf(intensity);
    }

    public override void restoreMoveSpeed(float intensity)
    {
        pathAgent.restoreMoveSpeed(intensity);
    }

    public virtual void dealDamage(Player player) {}

    public bool isFullHealth()
    {
        return health == maxHealth;
    }
}
