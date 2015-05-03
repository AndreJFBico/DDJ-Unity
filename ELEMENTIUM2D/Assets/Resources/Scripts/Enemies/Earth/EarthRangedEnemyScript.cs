using UnityEngine;
using System.Collections;
using Includes;

[RequireComponent(typeof(PathAgent))]
public class EarthRangedEnemyScript : EnemyScript
{

    protected LineRenderer lr;
    protected GameObject projectile;
    protected Vector3 latestTargetPosition;
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
        projectile = Resources.Load(EnemyStats.RangedNeutral.neutralEnemyProjectile) as GameObject;
        rangedRadius = EnemyStats.RangedNeutral.rangedRadius;
        maxHealth = EnemyStats.RangedNeutral.maxHealth;
        health = maxHealth;
        damage = EnemyStats.RangedNeutral.damage;
        defence = EnemyStats.RangedNeutral.defence;
        waterResist = EnemyStats.RangedNeutral.waterResist;
        earthResist = EnemyStats.RangedNeutral.earthResist;
        fireResist = EnemyStats.RangedNeutral.fireResist;
        gameObject.GetComponent<SphereCollider>().radius = EnemyStats.RangedNeutral.rangedRadius;
        unalertedSpeed = EnemyStats.FireBasic.unalertedSpeed;
        alertedSpeed = EnemyStats.FireBasic.alertedSpeed;

        activeWeapon = left;
        currentFireTransform = left_firepoint;
        right.gameObject.SetActive(false);
        
        base.Awake();
    }

    void OnEnable()
    {
        StartCoroutine("sendProjectile");
    }

    protected IEnumerator sendProjectile()
    {
        while(true)
        {
            if (pathAgent.hasTarget())
            {
                GameObject p = Instantiate(projectile, currentFireTransform.position, Quaternion.LookRotation(pathAgent.target.position - transform.position)) as GameObject;
                p.GetComponent<AbilityBehaviour>().initiate(this.gameObject, damage);
            }
            yield return new WaitForSeconds(EnemyStats.RangedNeutral.rangedAttackSpeed);
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

    public override void dealDamage(Player player)
    {
        player.takeDamage(damage, type, false);
    }
}
