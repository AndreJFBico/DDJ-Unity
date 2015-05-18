using UnityEngine;
using System.Collections;
using Includes;

public class WaterEnemyScript : EnemyScript
{
    protected LineRenderer lr;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
        _type = EnemyStats.WaterBasic.type;
        maxHealth = EnemyStats.WaterBasic.maxHealth;
        health = maxHealth;
        damage = EnemyStats.WaterBasic.damage;
        defence = EnemyStats.WaterBasic.defence;
        waterResist = EnemyStats.WaterBasic.waterResist;
        earthResist = EnemyStats.WaterBasic.earthResist;
        fireResist = EnemyStats.WaterBasic.fireResist;

        visionRadius = EnemyStats.WaterBasic.visionRadius;
        pathAgent.GetComponent<CapsuleCollider>().radius = visionRadius;

        pathAgent.UnalertedSpeed = EnemyStats.WaterBasic.unalertedSpeed;
        pathAgent.AlertedSpeed = EnemyStats.WaterBasic.alertedSpeed;

    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override void dealDamage(Player player)
    {
        player.takeDamage(damage, _type, false);
    }
}