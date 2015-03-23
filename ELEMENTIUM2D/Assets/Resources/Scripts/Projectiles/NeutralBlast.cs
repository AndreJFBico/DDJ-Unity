using UnityEngine;
using System.Collections;
using Includes;

public class NeutralBlast : ProjectileBehaviour 
{

    protected override void Awake()
    {
        explosion = Resources.Load("Prefabs/Explosions/neutralExplosion") as GameObject;
    }

    protected override void Start()
    {
        base.Start();
        damage = AbilityStats.Neutral.ability1.damage;
        type = Elements.NEUTRAL;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collidedWith(collision, damage));
        else if (collidedWithBreakable(collision));
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Unhitable"))
            return;
        base.OnCollisionEnter(collision);
    }

    public override void initiate(GameObject startingObject)
    {
        ConstantForce constantForce = gameObject.AddComponent<ConstantForce>();
        constantForce.relativeForce = new Vector3(0.0f, 0.0f, AbilityStats.Neutral.ability1.movementForce);
    }
}
