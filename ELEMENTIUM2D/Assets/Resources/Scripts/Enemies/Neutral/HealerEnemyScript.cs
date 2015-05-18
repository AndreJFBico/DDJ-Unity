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
        _type = Elements.NEUTRAL;
        projectile = Resources.Load(EnemyStats.HealerNeutral.neutralEnemyProjectile) as GameObject;
        rangedRadius = EnemyStats.HealerNeutral.rangedRadius;
        maxHealth = EnemyStats.HealerNeutral.maxHealth;
        health = maxHealth;
        damage = EnemyStats.HealerNeutral.damage;
        defence = EnemyStats.HealerNeutral.defence;
        waterResist = EnemyStats.HealerNeutral.waterResist;
        earthResist = EnemyStats.HealerNeutral.earthResist;
        fireResist = EnemyStats.HealerNeutral.fireResist;

        visionRadius = EnemyStats.HealerNeutral.visionRadius;
        pathAgent.GetComponent<CapsuleCollider>().radius = visionRadius;

        gameObject.GetComponent<SphereCollider>().radius = rangedRadius;
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
                targetedEnemy.GetComponent<EnemyScript>().healSelf(EnemyStats.HealerNeutral.healAmount, _type);
                if(targetedEnemy.GetComponent<EnemyScript>().isFullHealth())
                {
                    targetedEnemy = null;
                    pathAgent.target = null;
                    updateTargetedEnemy();
                }
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
            if (closestEnemy != null)
            {
                pathAgent.setAlerted(true);
                pathAgent.target = targetedEnemy;
                pathAgent.setStoppingDistance(rangedRadius);
            }
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
            
            if(targetedEnemy.gameObject != GameManager.Instance.Player.gameObject)
                pathAgent.setStoppingDistance(rangedRadius);
            else
                pathAgent.resetStoppingDistance();

            activeWeapon.LookAt(targetedEnemy);
            if(!targetedEnemy.GetComponent<EnemyScript>().isHurt())
            {
                targetedEnemy = null;
            }
        }
        else
        {
            updateTargetedEnemy();
            pathAgent.resetStoppingDistance();
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

    public override void setAlerted(bool val)
    {
        if (targetedEnemy == null)
        {
            if (val)
                alertedSign.transform.gameObject.SetActive(true);

            else
                alertedSign.transform.gameObject.SetActive(false);
            pathAgent.setAlerted(val);
            isAlerted = val;
        }
    }

    public override void dealDamage(Player player)
    {
        player.takeDamage(damage, _type, false);
    }

    public override void Eliminate()
    {
        base.Eliminate();
        targetedEnemies.Clear();
        targetedEnemy = null;
    }
}
