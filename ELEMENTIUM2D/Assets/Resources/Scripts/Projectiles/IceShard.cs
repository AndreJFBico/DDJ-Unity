using UnityEngine;
using System.Collections;
using Includes;

public class IceShard : ProjectileBehaviour {

    void Start()
    {
        base.Start();
        damage = ProjectileStats.Iceshard.damage;
    }

    public override void handleCollision(Transform collision)
    {
        //if (collision.gameObject.tag.CompareTo("Enemy") == 0)
        //{
            EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
            enemy.takeDamage(damage, Elements.FROST);
            Destroy(this.gameObject);
        //}
        //ignores what is unhitable
    }
}
