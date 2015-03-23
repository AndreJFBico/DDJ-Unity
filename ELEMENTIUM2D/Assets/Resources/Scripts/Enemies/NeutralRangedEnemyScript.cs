using UnityEngine;
using System.Collections;
using Includes;

[RequireComponent(typeof(PathAgent))]
public class NeutralRangedEnemyScript : EnemyScript
{

    protected LineRenderer lr;
    protected float rangedRadius;
    protected PathAgent agent;
    protected GameObject projectile;
    protected Vector3 latestTargetPosition;
    protected bool isShooting = false;

    // Use this for initialization
    protected override void Awake()
    {
        type = Elements.NEUTRAL;
        agent = gameObject.GetComponentInChildren<PathAgent>();
        projectile = Resources.Load(EnemyStats.Neutral.neutralEnemyProjectile) as GameObject;
        rangedRadius = EnemyStats.Neutral.rangedRadius;
        maxHealth = EnemyStats.Neutral.maxHealth;
        health = maxHealth;
        damage = EnemyStats.Neutral.damage;
        defence = EnemyStats.Neutral.defence;
        waterResist = EnemyStats.Neutral.waterResist;
        earthResist = EnemyStats.Neutral.earthResist;
        fireResist = EnemyStats.Neutral.fireResist;
        gameObject.GetComponent<SphereCollider>().radius = EnemyStats.Neutral.rangedRadius;
        //lr = gameObject.AddComponent<LineRenderer>();
        //lr.SetWidth(0.01f, 0.01f);
        //lr.SetVertexCount(2);
        base.Awake();
    }

    void alignLineRenderer()
    {
        //lr.SetPosition(0, transform.position);
        //lr.SetPosition(1, pathAgent.target.transform.position);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (pathAgent.target != null)
        {
            alignLineRenderer();
        }
        base.Update();
    }

    // Attack Range Radius
    /*protected void OnTriggerStay(Collider collider)
    {
        if (agent.hasTarget() && collider.tag.CompareTo("Enemy") == 0 && !isShooting)
        {
            InvokeRepeating("sendProjectile", 0f, EnemyStats.Neutral.rangedAttackSpeed);
            isShooting = true;
        }
    }*/

    // Attack Range Radius
    protected void OnTriggerEnter(Collider collider)
    {
        if(agent.hasTarget() && collider.tag.CompareTo("Enemy") == 0 && !isShooting)
        {
            InvokeRepeating("sendProjectile", 0f, EnemyStats.Neutral.rangedAttackSpeed);
            isShooting = true;
        }
    }

    protected void sendProjectile()
    {
        if(agent.hasTarget())
        {
            GameObject p = Instantiate(projectile, transform.position, Quaternion.LookRotation(agent.target.position - transform.position)) as GameObject;
            p.GetComponent<ProjectileBehaviour>().initiate(this.gameObject);
        }
        else
        {
            isShooting = false;
            CancelInvoke();
        }
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    /*protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }*/

    // Health bar
    protected override void OnGUI()
    {
        base.OnGUI();
    }
}
