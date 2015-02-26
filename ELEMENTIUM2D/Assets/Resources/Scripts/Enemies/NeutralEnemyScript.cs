using UnityEngine;
using System.Collections;
using Includes;

public class NeutralEnemyScript : EnemyScript {

	// Use this for initialization
	void Start () {
        base.Start();
        type = Elements.NEUTRAL;
        maxHealth = EnemyStats.Neutral.maxHealth;
        damage = EnemyStats.Neutral.damage;
        defence = EnemyStats.Neutral.defence;
        waterResist = EnemyStats.Neutral.waterResist;
        earthResist = EnemyStats.Neutral.earthResist;
        fireResist = EnemyStats.Neutral.fireResist;
	}
	
	// Update is called once per frame
	void Update () {
        base.Update();
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    // Health bar
    void OnGUI()
    {
        base.OnGUI();
    }
}
