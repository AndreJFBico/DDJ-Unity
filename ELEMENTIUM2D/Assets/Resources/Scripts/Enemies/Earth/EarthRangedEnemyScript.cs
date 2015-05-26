using UnityEngine;
using System.Collections;
using Includes;

public class EarthRangedEnemyScript : RangedEnemyScript
{

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
        _type = Elements.NEUTRAL;
        projectile = Resources.Load(EnemyStats.RangedNeutral.neutralEnemyProjectile) as GameObject;
        rangedRadius = EnemyStats.EarthRanged.rangedRadius;
        maxHealth = EnemyStats.EarthRanged.maxHealth;
        health = maxHealth;
        damage = EnemyStats.EarthRanged.damage;
        attackSpeed = EnemyStats.EarthRanged.rangedAttackSpeed;
        defence = EnemyStats.EarthRanged.defence;
        waterResist = EnemyStats.EarthRanged.waterResist;
        earthResist = EnemyStats.EarthRanged.earthResist;
        fireResist = EnemyStats.EarthRanged.fireResist;

        visionRadius = EnemyStats.EarthRanged.visionRadius;
        pathAgent.GetComponent<CapsuleCollider>().radius = visionRadius;

        gameObject.GetComponent<SphereCollider>().radius = rangedRadius;
        pathAgent.UnalertedSpeed = EnemyStats.EarthRanged.unalertedSpeed;
        pathAgent.AlertedSpeed = EnemyStats.EarthRanged.alertedSpeed;

        activeWeapon = left;
        currentFireTransform = left_firepoint;
        right.gameObject.SetActive(false);
        
    }
}
