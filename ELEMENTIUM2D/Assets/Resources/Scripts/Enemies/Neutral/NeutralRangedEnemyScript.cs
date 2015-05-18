using UnityEngine;
using System.Collections;
using Includes;

public class NeutralRangedEnemyScript : RangedEnemyScript
{

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
        _type = Elements.NEUTRAL;
        projectile = Resources.Load(EnemyStats.RangedNeutral.neutralEnemyProjectile) as GameObject;
        rangedRadius = EnemyStats.RangedNeutral.rangedRadius;
        maxHealth = EnemyStats.RangedNeutral.maxHealth;
        health = maxHealth;
        damage = EnemyStats.RangedNeutral.damage;
        attackSpeed = EnemyStats.RangedNeutral.rangedAttackSpeed;
        defence = EnemyStats.RangedNeutral.defence;
        waterResist = EnemyStats.RangedNeutral.waterResist;
        earthResist = EnemyStats.RangedNeutral.earthResist;
        fireResist = EnemyStats.RangedNeutral.fireResist;

        visionRadius = EnemyStats.RangedNeutral.visionRadius;
        pathAgent.GetComponent<CapsuleCollider>().radius = visionRadius;

        gameObject.GetComponent<SphereCollider>().radius = rangedRadius;
        pathAgent.UnalertedSpeed = EnemyStats.RangedNeutral.unalertedSpeed;
        pathAgent.AlertedSpeed = EnemyStats.RangedNeutral.alertedSpeed;

        activeWeapon = left;
        currentFireTransform = left_firepoint;
        right.gameObject.SetActive(false);
        
    }
}
