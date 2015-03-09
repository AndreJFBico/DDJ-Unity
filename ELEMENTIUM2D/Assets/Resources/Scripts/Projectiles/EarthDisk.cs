using UnityEngine;
using System.Collections;
using Includes;

public class EarthDisk : ProjectileBehaviour {

    protected override void Start()
    {
        base.Start();
        damage = AbilityStats.Earth.ability1.damage;
        type = Elements.EARTH;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collidedWithEnemy(collision, damage)) ;
        else if (collidedWithBreakable(collision)) ;
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
