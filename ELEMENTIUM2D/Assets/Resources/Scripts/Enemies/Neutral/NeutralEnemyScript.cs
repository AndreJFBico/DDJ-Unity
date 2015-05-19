using UnityEngine;
using System.Collections;
using Includes;

public class NeutralEnemyScript : EnemyScript
{
    protected LineRenderer lr;

    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
        _type = Elements.NEUTRAL;
        maxHealth = EnemyStats.BasicNeutral.maxHealth;
        health = maxHealth;
        damage = EnemyStats.BasicNeutral.damage;
        defence = EnemyStats.BasicNeutral.defence;
        waterResist = EnemyStats.BasicNeutral.waterResist;
        earthResist = EnemyStats.BasicNeutral.earthResist;
        fireResist = EnemyStats.BasicNeutral.fireResist;

        visionRadius = EnemyStats.BasicNeutral.visionRadius;
        pathAgent.GetComponent<CapsuleCollider>().radius = visionRadius;

        pathAgent.UnalertedSpeed = EnemyStats.BasicNeutral.unalertedSpeed;
        pathAgent.AlertedSpeed = EnemyStats.BasicNeutral.alertedSpeed;

    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override void dealDamage(Player player)
    {
        player.takeDamage(damage, _type, false, gameObject.name);
    }
}