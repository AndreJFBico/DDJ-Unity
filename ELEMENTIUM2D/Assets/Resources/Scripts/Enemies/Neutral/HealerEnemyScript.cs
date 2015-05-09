using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Includes;

public class HealerEnemyScript : EnemyScript {

    protected LineRenderer lr;
    protected GameObject projectile;
    protected Vector3 latestTargetPosition;
    protected Transform activeWeapon;

    public Transform left;
    public Transform left_firepoint;
    public Transform right;
    public Transform right_firepoint;

    private Transform currentFireTransform;

    // A healer sends healing projectiles 
    private List<Transform> targetedEnemies;
    private Transform targetedEnemy;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
        targetedEnemies = new List<Transform>();
        type = Elements.NEUTRAL;
        projectile = Resources.Load(EnemyStats.HealerNeutral.neutralEnemyProjectile) as GameObject;
        rangedRadius = EnemyStats.HealerNeutral.rangedRadius;
        maxHealth = EnemyStats.HealerNeutral.maxHealth;
        health = maxHealth;
        damage = EnemyStats.HealerNeutral.damage;
        defence = EnemyStats.HealerNeutral.defence;
        waterResist = EnemyStats.HealerNeutral.waterResist;
        earthResist = EnemyStats.HealerNeutral.earthResist;
        fireResist = EnemyStats.HealerNeutral.fireResist;
        gameObject.GetComponent<SphereCollider>().radius = EnemyStats.HealerNeutral.rangedRadius;
        pathAgent.UnalertedSpeed = EnemyStats.HealerNeutral.unalertedSpeed;
        pathAgent.AlertedSpeed = EnemyStats.HealerNeutral.alertedSpeed;

        activeWeapon = left;
        currentFireTransform = left_firepoint;
        right.gameObject.SetActive(false);
    }

    protected bool hasEnemyTarget()
    {
        return targetedEnemy != null;
    }

    // Attack Range Radius
    public override void OnTriggerEnter(Collider collider)
    {
        if(collider.tag.CompareTo("Enemy") == 0 )
        {
            targetedEnemies.Add(collider.transform);
        }
    }

    public override void OnTriggerExit(Collider collider)
    {
        if (targetedEnemies.Contains(collider.transform))
        {
            targetedEnemies.Remove(collider.transform);
            if (hasEnemyTarget() && collider.transform.GetInstanceID() == targetedEnemy.GetInstanceID())
            {
                targetedEnemy = null;
            }
        }
    }

    void OnEnable()
    {
        StartCoroutine("heal");
    }

    protected IEnumerator heal()
    {
        while(true)
        {
            if (hasEnemyTarget())
            {
                GameObject p = Instantiate(projectile, targetedEnemy.position, Quaternion.identity) as GameObject;
                p.transform.parent = targetedEnemy.transform;
                targetedEnemy.GetComponent<EnemyScript>().healSelf(EnemyStats.HealerNeutral.healAmount, type);
                //p.GetComponent<AbilityBehaviour>().initiate(this.gameObject);
            }
            yield return new WaitForSeconds(EnemyStats.HealerNeutral.rangedAttackSpeed);
        }

    }

    protected void updateTargetedEnemy()
    {
        if(targetedEnemies.Count > 0)
        {
            float lowestDistance = float.MaxValue;
            Transform closestEnemy = targetedEnemy;
            foreach(Transform t in targetedEnemies)
            {
                if (t != null)
                {
                    float distance = Vector3.Distance(t.position, transform.position);
                    if (distance < lowestDistance && t.GetComponent<EnemyScript>().isHurt())
                    {
                        lowestDistance = distance;
                        closestEnemy = t;
                    }
                }
            }
            targetedEnemy = closestEnemy;
        }
    }

    protected override void LateUpdate()
    {
        if (hasEnemyTarget())
        {
            if(targetedEnemy.position.x >= transform.position.x)
            {
                left.gameObject.SetActive(true);
                activeWeapon = left;
                right.gameObject.SetActive(false);
                currentFireTransform = left_firepoint;
            }
            else
            {
                right.gameObject.SetActive(true);
                activeWeapon = right;
                left.gameObject.SetActive(false);
                currentFireTransform = right_firepoint;
            }
            if((targetedEnemy.position - transform.position).magnitude < 1)
            {

            }
            activeWeapon.LookAt(targetedEnemy);
            if(!targetedEnemy.GetComponent<EnemyScript>().isHurt())
            {
                targetedEnemy = null;
            }
        }
        else
        {
            updateTargetedEnemy();
            activeWeapon.rotation = Quaternion.identity;
        }
        base.LateUpdate();
    }

    public override void playerSighted()
    {
        if(targetedEnemy == null)
        {
            base.playerSighted();
        }
        else
        {
            pathAgent.target = targetedEnemy;
        }
    }

    public override void dealDamage(Player player)
    {
        player.takeDamage(damage, type, false);
    }
}
