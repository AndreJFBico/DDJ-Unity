﻿using UnityEngine;
using System.Collections;
using Includes;

public class FireEnemyScript : EnemyScript
{
    private float statusIntensity;
    private float statusDurability;

    // Use this for initialization
    protected override void Awake()
    {
        type = EnemyStats.FireBasic.type;
        maxHealth = EnemyStats.FireBasic.maxHealth;
        health = maxHealth;
        damage = EnemyStats.FireBasic.damage;
        defence = EnemyStats.FireBasic.defence;
        waterResist = EnemyStats.FireBasic.waterResist;
        earthResist = EnemyStats.FireBasic.earthResist;
        fireResist = EnemyStats.FireBasic.fireResist;
        statusDurability = EnemyStats.FireBasic.statusDurability;
        statusIntensity = EnemyStats.FireBasic.statusIntensity;
        unalertedSpeed = EnemyStats.FireBasic.unalertedSpeed;
        alertedSpeed = EnemyStats.FireBasic.alertedSpeed;

        base.Awake();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override void dealDamage(Player player)
    {
        player.takeDamage(damage, type);
        int i = RandomGenerator.Next(4);
        if(i == 0)
        {
            StatusEffectManager.Instance.applyBurning(player.gameObject, statusIntensity, statusDurability);
        }
    }
}