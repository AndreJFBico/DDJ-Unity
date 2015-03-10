using UnityEngine;
using System.Collections;
using Includes;

public class Fireball : ProjectileBehaviour 
{

    protected override void Start()
    {
        base.Start();
        damage = AbilityStats.Fire.ability1.damage;
        type = Elements.FIRE;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collidedWithEnemy(collision, damage)) ;
        else if (collidedWithBreakable(collision)) ;
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Unhitable"))
            return;
        base.OnCollisionEnter(collision);
    }

    void OnTriggerEnter(Collider other)
    {
        enteredBreakableTrigger(other);
    }

    public override void applyMovement()
    {
        float rndm = Random.Range(AbilityStats.Fire.ability1.minForce, AbilityStats.Fire.ability1.maxForce);
        ConstantForce constantForce = gameObject.AddComponent<ConstantForce>();
        constantForce.relativeForce = new Vector3(0.0f, 0.0f, rndm);
    }
}
