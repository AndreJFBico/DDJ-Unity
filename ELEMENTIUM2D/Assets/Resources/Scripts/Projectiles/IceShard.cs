using UnityEngine;
using System.Collections;
using Includes;

public class IceShard : AbilityBehaviour
{
    protected int numWaterToIce = 3;

    protected override void Awake()
    {
        explosion = Resources.Load("Prefabs/Explosions/frostExplosion") as GameObject;
    }

    protected override void Start()
    {
        base.Start();
        damage = AbilityStats.Frost.ability1.damage;
        type = Elements.FROST;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collidedWith(collision.gameObject, damage)) ;
        else if (collidedWithBreakable(collision.gameObject)) ;
        //else if ();
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Unhitable"))
            return;
        base.OnCollisionEnter(collision);
    }

    public override void initiate(GameObject startingObject)
    {
        ConstantForce constantForce = gameObject.AddComponent<ConstantForce>();
        constantForce.relativeForce = new Vector3(0.0f, 0.0f, AbilityStats.Frost.ability1.movementForce);
    }
}
