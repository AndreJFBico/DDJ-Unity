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

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collidedWithEnemy(collision, damage)) ;
        else if (collidedWithBreakable(collision)) ;
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Unhitable"))
            return;
        base.OnCollisionEnter2D(collision);
    }

    public override void applyMovement()
    {
        ConstantForce2D constantForce = gameObject.AddComponent<ConstantForce2D>();
        constantForce.relativeForce = new Vector2(AbilityStats.Fire.ability1.movementForce, 0.0f);
    }
}
