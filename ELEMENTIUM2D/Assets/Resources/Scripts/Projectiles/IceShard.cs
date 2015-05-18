﻿using UnityEngine;
using System.Collections;
using Includes;

public class IceShard : AbilityBehaviour
{
    protected int numWaterToIce = 3;
    protected int bounce = AbilityStats.Frost.ability1.bounce;

    protected override void Awake()
    {
        base.Awake();
        explosion = Resources.Load("Prefabs/Explosions/frostExplosion") as GameObject;
        damage = 1;
    }

    protected override void Start()
    {
        base.Start();
        type = Elements.WATER;
    }

    float signedAngleRadian(Vector3 vec1, Vector3 vec2)
    {
        //Get the dot product
        float dot = Vector3.Dot(vec1, vec2);
        // Divide the dot by the product of the magnitudes of the vectors
        dot = dot / (vec1.magnitude * vec2.magnitude);
        //Get the arc cosin of the angle, you now have your angle in radians 
        var acos = Mathf.Acos(dot);

        return acos * Mathf.Sign(Vector3.Cross(vec1, vec2).y);
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collidedWith(collision.gameObject, damage)) 
            base.OnCollisionEnter(collision);
        else if (collidedWithBreakable(collision.gameObject)) 
            base.OnCollisionEnter(collision);
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Unhitable"))
            base.OnCollisionEnter(collision);
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            if (bounce == 0)
            {
                base.OnCollisionEnter(collision);
            }
            //Bounce Code
            if(bounce > 0)
            {
                ContactPoint cp = collision.contacts[0];
                bounce--;
                Vector3 impactNormal = cp.normal;

                transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f), Mathf.Rad2Deg * (signedAngleRadian(transform.forward, impactNormal)));
                //GetComponent<Rigidbody>().velocity = Vector3.Reflect(GetComponent<Rigidbody>().velocity, cp.normal);

            }
        }
        else base.OnCollisionEnter(collision);
    }

    public override void initiate(GameObject startingObject, float dmg, int projectileID, int totalProjectiles)
    {
        base.initiate(startingObject, dmg, projectileID, totalProjectiles);
        GetComponent<Rigidbody>().AddForce(transform.forward * startSpeed);
        damage = dmg;
        ConstantForce constantForce = gameObject.AddComponent<ConstantForce>();
        constantForce.relativeForce = new Vector3(0.0f, 0.0f, AbilityStats.Frost.ability1.movementForce);
    }
}
