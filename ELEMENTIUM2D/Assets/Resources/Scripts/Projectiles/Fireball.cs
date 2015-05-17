using UnityEngine;
using System.Collections;
using Includes;

public class Fireball : AbilityBehaviour 
{
    private int collisionNumber = 0;

    private Transform previousCollidedObject;

    protected override void Awake()
    {
        explosion = Resources.Load("Prefabs/Explosions/fireExplosion") as GameObject;
        damage = 1;
    }

    protected override void Start()
    {
        base.Start();
        type = Elements.FIRE;
    }

    public override void OnTriggerEnter(Collider collision)
    {
        // We do not interact with other triggers
        if (!collision.isTrigger)
        {
            if (collidedWithBreakable(collision.gameObject)) ;
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Unhitable"))
                return;
            else if ((previousCollidedObject != null && previousCollidedObject.GetInstanceID() != collision.transform.GetInstanceID()) || (previousCollidedObject == null))
            {
                if (collidedWith(collision.gameObject, damage))
                {
                    createExplosion();
                    previousCollidedObject = collision.gameObject.transform;
                    collisionNumber++;
                }
            }
            //base.OnCollisionEnter(collision);
            if (collision.gameObject.tag.CompareTo("Enemy") == 0)
            {
                if (collisionNumber >= AbilityStats.Fire.ability1.collisionNumber)
                    base.destroyBehaviour();
            }
            else base.destroyBehaviour();
        }
        else
        {
            enteredBreakableTrigger(collision.gameObject);
        }
    }

    //Receive also damage
    public override void initiate(GameObject startingObject, float dmg, int projectileID, int totalProjectiles)
    {
        base.initiate(startingObject, dmg, projectileID, totalProjectiles);
        GetComponent<Rigidbody>().AddForce(transform.forward * startSpeed);
        damage = dmg;
        float rndm = Random.Range(AbilityStats.Fire.ability1.minForce, AbilityStats.Fire.ability1.maxForce);
        ConstantForce constantForce = gameObject.AddComponent<ConstantForce>();
        constantForce.relativeForce = new Vector3(0.0f, 0.0f, rndm);
    }
}
