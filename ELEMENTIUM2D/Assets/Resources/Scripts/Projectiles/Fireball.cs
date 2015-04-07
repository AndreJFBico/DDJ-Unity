using UnityEngine;
using System.Collections;
using Includes;

public class Fireball : AbilityBehaviour 
{
    private int collisionNumber = 0;
    protected override void Awake()
    {
        explosion = Resources.Load("Prefabs/Explosions/fireExplosion") as GameObject;
    }

    protected override void Start()
    {
        base.Start();
        damage = AbilityStats.Fire.ability1.damage;
        type = Elements.FIRE;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collidedWith(collision, damage)) ;
        else if (collidedWithBreakable(collision)) ;
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Unhitable"))
            return;
        //base.OnCollisionEnter(collision);
        if (collisionNumber >= AbilityStats.Fire.ability1.collisionNumber)
            base.OnCollisionEnter(collision);
        collisionNumber++;
    }

    public override void initiate(GameObject startingObject)
    {
        float rndm = Random.Range(AbilityStats.Fire.ability1.minForce, AbilityStats.Fire.ability1.maxForce);
        ConstantForce constantForce = gameObject.AddComponent<ConstantForce>();
        constantForce.relativeForce = new Vector3(0.0f, 0.0f, rndm);
    }
}
