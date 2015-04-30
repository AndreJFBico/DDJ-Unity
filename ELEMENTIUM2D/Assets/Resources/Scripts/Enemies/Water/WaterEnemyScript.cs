using UnityEngine;
using System.Collections;
using Includes;

public class WaterEnemyScript : EnemyScript
{
    protected LineRenderer lr;

    // Use this for initialization
    protected override void Awake()
    {
        type = EnemyStats.WaterBasic.type;
        maxHealth = EnemyStats.WaterBasic.maxHealth;
        health = maxHealth;
        damage = EnemyStats.WaterBasic.damage;
        defence = EnemyStats.WaterBasic.defence;
        waterResist = EnemyStats.WaterBasic.waterResist;
        earthResist = EnemyStats.WaterBasic.earthResist;
        fireResist = EnemyStats.WaterBasic.fireResist;
        unalertedSpeed = EnemyStats.WaterBasic.unalertedSpeed;
        alertedSpeed = EnemyStats.WaterBasic.alertedSpeed;

        base.Awake();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override void dealDamage(Player player)
    {
        player.takeDamage(damage, type);
    }
}