﻿using UnityEngine;
using System.Collections;
using Includes;

public class Musk : AbilityBehaviour{

    protected override void Awake()
    {
        base.Awake();
        explosion = Resources.Load("Prefabs/Explosions/earthExplosion") as GameObject;
        damage = 1;
        startSpeed = 250;
    }

    protected override void Start()
    {
        base.Start();
        type = Elements.NEUTRAL;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collidedWith(collision.gameObject, damage)) ;
        else if (collidedWithBreakable(collision.gameObject)) ;
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Unhitable"))
            return;
        base.OnCollisionEnter(collision);
    }

    public override void initiate(GameObject startingObject, float dmg, int projectileID, int totalProjectiles)
    {
        base.initiate(startingObject, dmg, projectileID, totalProjectiles);
        GetComponent<Rigidbody>().AddForce(transform.forward * startSpeed);
        damage = dmg;
        ConstantForce constantForce = gameObject.AddComponent<ConstantForce>();
        constantForce.relativeForce = new Vector3(0.0f, 0.0f, EnemyStats.RangedNeutral.movementForce);
    }
}
