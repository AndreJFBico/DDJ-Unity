﻿using UnityEngine;
using System.Collections;
using Includes;

public class Player : Agent {

    public int currentElement = (int)Elements.NEUTRAL;
    public GameObject reloadButton;
    private float damageTimer = 0.0f;
    private bool timerRunning = false;
    private CharacterMove characterMoveScrpt;
    private bool alphaed = false;

    private float previousSpeed = 0;

    void Awake()
    {
        base.Awake();
        GameManager.Instance.sceneInit();
        OilPuddleManager.Instance.sceneInit();
        maxHealth = GameManager.Instance.Stats.maxHealth;
        health = maxHealth;
        damage = GameManager.Instance.Stats.damage;
        defence = GameManager.Instance.Stats.defence;
        waterResist = GameManager.Instance.Stats.waterResist;
        earthResist = GameManager.Instance.Stats.earthResist;
        fireResist = GameManager.Instance.Stats.fireResist;
        characterMoveScrpt = transform.gameObject.GetComponent<CharacterMove>();
        GameManager.Instance.Stats.dumpStats();
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

    protected void Update()
    {
        if (timerRunning)
            damageTimer += Time.deltaTime;
        if (damageTimer >= GameManager.Instance.Stats.damageTimer)
        {
            timerRunning = false;
            damageTimer = 0.0f;
        }
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

    public override void applyStatusEffect(StatusEffect scrpt)
    {
        scrpt.applyStatusEffect(this);
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
            if (!(damageTimer >= GameManager.Instance.Stats.damageTimer))
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
            Application.LoadLevel("SkillTree");
        }
    }

    protected virtual void OnGUI()
    {
        // Health bar
        /*Vector2 targetPos = healthbar_background.position;
        if (centerHealthBar)
        {
            targetPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f));
        }*/

        //healthbar_background.position = targetPos;
        float percentage = health / maxHealth;
        healthbar.transform.localScale = new Vector3(percentage, 1.0f, 1.0f);
    }

    public override void slowSelf(float intensity)
    {
        characterMoveScrpt.Slow *= intensity;
    }

    public override void restoreMoveSpeed(float intensity)
    {
        characterMoveScrpt.Slow /= intensity;
    }
}
