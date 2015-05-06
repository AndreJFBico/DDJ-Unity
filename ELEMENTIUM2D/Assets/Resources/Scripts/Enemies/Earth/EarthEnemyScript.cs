using UnityEngine;
using System.Collections;
using Includes;

public class EarthEnemyScript : EnemyScript
{
    private float statusIntensity;
    private float statusDurability;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
        type = EnemyStats.EarthBasic.type;
        maxHealth = EnemyStats.EarthBasic.maxHealth;
        health = maxHealth;
        damage = EnemyStats.EarthBasic.damage;
        defence = EnemyStats.EarthBasic.defence;
        waterResist = EnemyStats.EarthBasic.waterResist;
        earthResist = EnemyStats.EarthBasic.earthResist;
        fireResist = EnemyStats.EarthBasic.fireResist;
        statusDurability = EnemyStats.EarthBasic.statusDurability;
        statusIntensity = EnemyStats.EarthBasic.statusIntensity;
        pathAgent.UnalertedSpeed = EnemyStats.EarthBasic.unalertedSpeed;
        pathAgent.AlertedSpeed = EnemyStats.EarthBasic.alertedSpeed;

    }


    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override void dealDamage(Player player)
    {
        player.takeDamage(damage, type, false);

        StatusEffectManager.Instance.applySlow(player.gameObject, statusIntensity, statusDurability);
    }
}