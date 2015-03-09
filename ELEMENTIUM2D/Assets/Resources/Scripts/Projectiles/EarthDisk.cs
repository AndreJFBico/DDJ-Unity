using UnityEngine;
using System.Collections;
using Includes;

public class EarthDisk : ProjectileBehaviour {

    protected override void Start()
    {
        base.Start();
        damage = AbilityStats.Earth.ability1.damage;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.CompareTo("Enemy") == 0)
        {
            EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
            enemy.takeDamage(damage, Elements.EARTH);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Unhitable"))
            return;
        base.OnCollisionEnter(collision);
    }

    public override void applyMovement()
    {
        ConstantForce constantForce = gameObject.AddComponent<ConstantForce>();
        constantForce.relativeForce = new Vector3(0.0f, 0.0f, AbilityStats.Earth.ability1.movementForce);
    }
}
