using UnityEngine;
using System.Collections;
using Includes;

public class FireEnemyScript : EnemyScript
{
    private float statusIntensity;
    private float statusDurability;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
        _type = EnemyStats.FireBasic.type;
        maxHealth = EnemyStats.FireBasic.maxHealth;
        health = maxHealth;
        damage = EnemyStats.FireBasic.damage;
        defence = EnemyStats.FireBasic.defence;
        waterResist = EnemyStats.FireBasic.waterResist;
        earthResist = EnemyStats.FireBasic.earthResist;
        fireResist = EnemyStats.FireBasic.fireResist;

        visionRadius = EnemyStats.FireBasic.visionRadius;
        pathAgent.GetComponent<CapsuleCollider>().radius = visionRadius;

        statusDurability = EnemyStats.FireBasic.statusDurability;
        statusIntensity = EnemyStats.FireBasic.statusIntensity;
        pathAgent.UnalertedSpeed = EnemyStats.FireBasic.unalertedSpeed;
        pathAgent.AlertedSpeed = EnemyStats.FireBasic.alertedSpeed;

    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override void dealDamage(Player player)
    {
        player.takeDamage(damage, _type, false);
        int i = RandomGenerator.Next(4);
        if(i == 0)
        {
            StatusEffectManager.Instance.applyBurning(player.gameObject, statusIntensity, statusDurability);
        }
    }
}