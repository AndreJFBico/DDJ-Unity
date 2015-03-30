using UnityEngine;
using System.Collections;
using Includes;

public class EnemySpawner : EnemyScript
{
	// Use this for initialization
	protected override void Awake()
    {
        type = Elements.NEUTRAL;
        maxHealth = EnemyStats.Neutral.maxHealth;
        health = maxHealth;
        damage = EnemyStats.Neutral.damage;
        defence = EnemyStats.Neutral.defence;
        waterResist = EnemyStats.Neutral.waterResist;
        earthResist = EnemyStats.Neutral.earthResist;
        fireResist = EnemyStats.Neutral.fireResist;

        base.Awake();
	}
}
