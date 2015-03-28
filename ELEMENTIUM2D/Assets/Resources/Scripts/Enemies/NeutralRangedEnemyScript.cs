using UnityEngine;
using System.Collections;
using Includes;

[RequireComponent(typeof(PathAgent))]
public class NeutralRangedEnemyScript : EnemyScript
{

    protected LineRenderer lr;
    protected GameObject projectile;
    protected Vector3 latestTargetPosition;
    protected bool isShooting = false;
    protected Transform activeWeapon;

    public Transform left;
    public Transform left_firepoint;
    public Transform right;
    public Transform right_firepoint;

    private Transform currentFireTransform;

    // Use this for initialization
    protected override void Awake()
    {
        type = Elements.NEUTRAL;
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
    protected override void Update()
    {
        if (pathAgent.target != null)
        {
            alignLineRenderer();
        }
        base.Update();
    }

    // Attack Range Radius
    protected void OnTriggerEnter(Collider collider)
    {
        if(pathAgent.hasTarget() && collider.tag.CompareTo("Enemy") == 0 && !isShooting)
        {
            InvokeRepeating("sendProjectile", 0f, EnemyStats.Neutral.rangedAttackSpeed);
            isShooting = true;
        }
    }

    protected void sendProjectile()
    {
        if (pathAgent.hasTarget())
        {
            GameObject p = Instantiate(projectile, currentFireTransform.position, Quaternion.LookRotation(pathAgent.target.position - transform.position)) as GameObject;
            p.GetComponent<AbilityBehaviour>().initiate(this.gameObject);
        }
        else
        {
            isShooting = false;
            CancelInvoke();
        }
    }

    protected override void LateUpdate()
    {
        if (pathAgent.hasTarget())
        {
            if(pathAgent.target.position.x >= transform.position.x)
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
            activeWeapon.LookAt(pathAgent.target.position);
        }
        else
        {
            activeWeapon.rotation = Quaternion.identity;
        }
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
