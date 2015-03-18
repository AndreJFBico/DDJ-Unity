using UnityEngine;
using System.Collections;
using Includes;

public class Musk : ProjectileBehaviour
{

    protected override void Awake()
    {
        explosion = Resources.Load("Prefabs/Explosions/earthExplosion") as GameObject;
    }

    protected override void Start()
    {
        base.Start();
        //This has to change one thing is the damage the enemy does when it touches the player the other is the ranged projectile damage
        damage = EnemyStats.Neutral.damage;
        type = Elements.NEUTRAL;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collidedWith(collision, damage)) ;
        else if (collidedWithBreakable(collision)) ;
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Unhitable"))
            return;
        base.OnCollisionEnter(collision);
    }

    public override void applyMovement()
    {
        ConstantForce constantForce = gameObject.AddComponent<ConstantForce>();
        constantForce.relativeForce = new Vector3(0.0f, 0.0f, AbilityStats.Neutral.ability1.movementForce / 2.0f);
    }
}
