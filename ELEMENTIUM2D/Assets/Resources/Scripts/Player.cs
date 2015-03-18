using UnityEngine;
using System.Collections;
using Includes;

public class Player : Agent {

    public int currentElement = (int)Elements.NEUTRAL;
    public GameObject reloadButton;
    private float damageTimer = 0.0f;
    private bool timerRunning = false;
    private CharacterMove characterMoveScrpt;
    private bool alphaed = false;

    void Start()
    {
        maxHealth = PlayerStats.maxHealth;
        health = maxHealth;
        damage = PlayerStats.damage;
        defence = PlayerStats.defence;
        waterResist = PlayerStats.waterResist;
        earthResist = PlayerStats.earthResist;
        fireResist = PlayerStats.fireResist;
        characterMoveScrpt = transform.gameObject.GetComponent<CharacterMove>();
        InvokeRepeating("blink", 0f, 0.10f);
    }

    public override void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag.CompareTo("Enemy") == 0)
        {
            /*if(!timerRunning)
            {
                EnemyScript agent = collision.gameObject.GetComponent<EnemyScript>();
                takeDamage(agent.getDamage(), agent.getElementType());
            }
            else if (damageTimer >= PlayerStats.damageTimer)
            {
                damageTimer = 0.0f;
                EnemyScript agent = collision.gameObject.GetComponent<EnemyScript>();
                takeDamage(agent.getDamage(), agent.getElementType());
            }*/
            EnemyScript agent = collision.gameObject.GetComponent<EnemyScript>();
            takeDamage(agent.getDamage(), agent.getElementType());
        }
        characterMoveScrpt.CollisionStay(collision);
    }

    public override void Update()
    {
        if (timerRunning)
            damageTimer += Time.deltaTime;
        if (damageTimer >= PlayerStats.damageTimer)
        {
            timerRunning = false;
            damageTimer = 0.0f;
        }
            
 	    base.Update();
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


    // ATTENTION THIS SHOULD NOT BE HERE, this should either be implemented in the enemies themselves or in the corresponding projectile behaviours
    public override void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collided with play");
        if (collision.transform.tag.CompareTo("Enemy") == 0)
        {
            /*if (!timerRunning)
            {
                EnemyScript agent = collision.gameObject.GetComponent<EnemyScript>();
                takeDamage(agent.getDamage(), agent.getElementType());
            }
            else if (damageTimer >= PlayerStats.damageTimer)
            {
                damageTimer = 0.0f;
                EnemyScript agent = collision.gameObject.GetComponent<EnemyScript>();
                takeDamage(agent.getDamage(), agent.getElementType());
            }*/
            EnemyScript agent = collision.gameObject.GetComponent<EnemyScript>();
            takeDamage(agent.getDamage(), agent.getElementType());
        }
        characterMoveScrpt.CollisionEnter(collision);
        base.OnCollisionEnter(collision);
    }

    public override void OnCollisionExit(Collision collision)
    {
        characterMoveScrpt.CollisionExit(collision);
        base.OnCollisionExit(collision);
    }

    public override void takeDamage(float amount, Elements type)
    {
        if (timerRunning)
        {
            if (!(damageTimer >= PlayerStats.damageTimer))
            {
                return;
            }
            else
            {
                timerRunning = false;
                damageTimer = 0.0f;
            }
        }
        else
        {
            timerRunning = true;          
        }

        switch (type)
        {
            case Elements.NEUTRAL:
                health -= amount * (1 - ((defence + waterResist + earthResist + fireResist) / 100.0f));
                break;

            case Elements.FIRE:
                health -= amount * (1 - ((defence + fireResist) / 100.0f));
                break;

            case Elements.EARTH:
                health -= amount * (1 - ((defence + earthResist) / 100.0f));
                break;

            case Elements.FROST:
                health -= amount * (1 - ((defence + waterResist) / 100.0f));
                break;

            default:
                break;
        }
        if (health <= 0)
        {
            reloadButton.SetActive(true);
        }
        OnGUI();
    }
}
