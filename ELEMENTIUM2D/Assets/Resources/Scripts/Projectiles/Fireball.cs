using UnityEngine;
using System.Collections;
using Includes;

public class Fireball : ProjectileBehaviour {

    protected override void Start()
    {
        base.Start();
        damage = ProjectileStats.Fireball.damage;
    }

    public override void handleCollision(Transform collision)
    {
        //if (collision.gameObject.tag.CompareTo("Enemy") == 0)
        //{  
            EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
            enemy.takeDamage(damage, Elements.FIRE);
            Destroy(this.gameObject);
        //}
        //ignores what is unhitable
    }
}
