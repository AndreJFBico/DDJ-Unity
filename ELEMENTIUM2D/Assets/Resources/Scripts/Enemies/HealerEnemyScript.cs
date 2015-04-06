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
        //lr = gameObject.AddComponent<LineRenderer>();
        //lr.SetWidth(0.01f, 0.01f);
        //lr.SetVertexCount(2);
        activeWeapon = left;
        currentFireTransform = left_firepoint;
        right.gameObject.SetActive(false);
        base.Awake();
    }

    void alignLineRenderer()
    {
        //lr.SetPosition(0, transform.position);
        //lr.SetPosition(1, pathAgent.target.transform.position);
    }

    // Update is called once per frame
   /* protected override void Update()
    {
        if (pathAgent.target != null)
        {
            alignLineRenderer();
        }
        base.Update();
    }*/

    protected bool hasEnemyTarget()
    {
        return targetedEnemy != null;
    }

    // Attack Range Radius
    public override void OnTriggerEnter(Collider collider)
    {
        if(collider.tag.CompareTo("Enemy") == 0)
        {
            targetedEnemies.Add(collider.transform);
        }
    }

    public override void OnTriggerExit(Collider collider)
    {
        if (collider.tag.CompareTo("Enemy") == 0)
        {
            targetedEnemies.RemoveAll(x => x.GetInstanceID() == collider.GetInstanceID());
            if (hasEnemyTarget() && collider.GetInstanceID() == targetedEnemy.GetInstanceID())
            {
                targetedEnemy = null;
            }
        }
    }

    void OnEnable()
    {
        StartCoroutine("heal");
    }

    protected IEnumerable heal()
    {
        while(true)
        {
            if (hasEnemyTarget())
            {
                GameObject p = Instantiate(projectile, targetedEnemy.position, Quaternion.identity) as GameObject;
                p.transform.parent = targetedEnemy.transform;
                targetedEnemy.GetComponent<EnemyScript>().takeDamage(-EnemyStats.HealerNeutral.healAmount, type);
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
            activeWeapon.LookAt(targetedEnemy);
            }
        else
        {
            updateTargetedEnemy();
            activeWeapon.rotation = Quaternion.identity;
        }
        base.LateUpdate();
    }

    /*protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }*/

    // Health bar
    /*protected override void OnGUI()
    {
        base.OnGUI();
    }*/
}
