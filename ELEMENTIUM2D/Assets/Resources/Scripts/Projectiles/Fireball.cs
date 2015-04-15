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
    }

    protected override void Start()
    {
        base.Start();
        damage = AbilityStats.Fire.ability1.damage;
        type = Elements.FIRE;
    }

    public override void OnTriggerEnter(Collider collision)
    {
        // We do not interact with other triggers
        if(!collision.gameObject.GetComponent<Collider>().isTrigger)
        {
            if((previousCollidedObject != null && previousCollidedObject.GetInstanceID() != collision.transform.GetInstanceID()) || (previousCollidedObject == null))
            {
                if (collidedWith(collision.gameObject, damage))
                {
                    createExplosion();
                    previousCollidedObject = collision.gameObject.transform;
                    collisionNumber++;
                    Debug.Log(collision.gameObject.name);
                }
            }
            else if (collidedWithBreakable(collision.gameObject)) ;
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Unhitable"))
                return;
            //base.OnCollisionEnter(collision);
            if(collision.gameObject.tag.CompareTo("Enemy") == 0)
            {
                if (collisionNumber >= AbilityStats.Fire.ability1.collisionNumber)
                    base.destroyBehaviour();   
            }
            else base.destroyBehaviour();  
        }
    }

    public override void initiate(GameObject startingObject)
    {
        float rndm = Random.Range(AbilityStats.Fire.ability1.minForce, AbilityStats.Fire.ability1.maxForce);
        ConstantForce constantForce = gameObject.AddComponent<ConstantForce>();
        constantForce.relativeForce = new Vector3(0.0f, 0.0f, rndm);
    }
}
