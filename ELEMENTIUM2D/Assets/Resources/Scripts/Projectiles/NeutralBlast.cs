using UnityEngine;
using System.Collections;
using Includes;

public class NeutralBlast : AbilityBehaviour 
{

    protected override void Awake()
    {
        explosion = Resources.Load("Prefabs/Explosions/neutralExplosion") as GameObject;
        damage = 1;
    }

    protected override void Start()
    {
        base.Start();
        type = Elements.NEUTRAL;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collidedWith(collision.gameObject, damage));
        else if (collidedWithBreakable(collision.gameObject));
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Unhitable"))
            return;
        base.OnCollisionEnter(collision);
    }

    public override void initiate(GameObject startingObject, float dmg)
    {
        base.initiate(startingObject, dmg);
        GetComponent<Rigidbody>().AddForce(transform.forward * startSpeed);
        damage = dmg;
        ConstantForce constantForce = gameObject.AddComponent<ConstantForce>();
        constantForce.relativeForce = new Vector3(0.0f, 0.0f, AbilityStats.Neutral.ability1.movementForce);
    }
}
