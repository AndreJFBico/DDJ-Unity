using UnityEngine;
using System.Collections;
using Includes;

public class FireRangedEnemyScript : RangedEnemyScript
{

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
        type = Elements.NEUTRAL;
        projectile = Resources.Load(EnemyStats.FireRanged.neutralEnemyProjectile) as GameObject;
        rangedRadius = EnemyStats.FireRanged.rangedRadius;
        maxHealth = EnemyStats.FireRanged.maxHealth;
        health = maxHealth;
        damage = EnemyStats.FireRanged.damage;
        attackSpeed = EnemyStats.FireRanged.rangedAttackSpeed;
        defence = EnemyStats.FireRanged.defence;
        waterResist = EnemyStats.FireRanged.waterResist;
        earthResist = EnemyStats.FireRanged.earthResist;
        fireResist = EnemyStats.FireRanged.fireResist;

        visionRadius = EnemyStats.FireRanged.visionRadius;
        pathAgent.GetComponent<CapsuleCollider>().radius = visionRadius;

        gameObject.GetComponent<SphereCollider>().radius = rangedRadius;
        pathAgent.UnalertedSpeed = EnemyStats.FireRanged.unalertedSpeed;
        pathAgent.AlertedSpeed = EnemyStats.FireRanged.alertedSpeed;

        activeWeapon = left;
        currentFireTransform = left_firepoint;
        right.gameObject.SetActive(false);
        
    }
}
