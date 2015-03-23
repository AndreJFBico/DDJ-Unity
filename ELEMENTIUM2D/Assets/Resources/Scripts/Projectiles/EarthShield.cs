using UnityEngine;
using System.Collections;
using Includes;

public class EarthShield : ProjectileBehaviour {

    protected override void Awake()
    {
        base.Awake();
        explosion = Resources.Load("Prefabs/Explosions/earthExplosion") as GameObject;
    }

    protected override void Start()
    {
        base.Start();
        damage = AbilityStats.Earth.ability2.damage;
        type = Elements.EARTH;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collidedWith(collision, damage)) ;
        else if (collidedWithBreakable(collision)) ;
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Unhitable"))
            return;
        base.OnCollisionEnter(collision);
    }


    public override void initiate(GameObject startingObject)
    {
        var center = startingObject.transform.position;
        transform.parent = startingObject.transform;
        //ConstantForce constantForce = gameObject.AddComponent<ConstantForce>();
        //constantForce.relativeForce = new Vector3(0.0f, 0.0f, AbilityStats.Earth.ability2.movementForce);
    }
}
