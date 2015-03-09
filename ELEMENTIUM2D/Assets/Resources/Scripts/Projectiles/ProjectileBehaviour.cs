﻿using UnityEngine;
using System.Collections;
using Includes;

public class ProjectileBehaviour : MonoBehaviour
{

    protected float damage;
    protected Elements type;

    protected virtual void Start() { }

    public virtual void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    protected bool collidedWithEnemy(Collision collision, float damage)
    {
        if (collision.gameObject.tag.CompareTo("Enemy") == 0)
        {
            EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
            enemy.takeDamage(damage, type);
            return true;
        }
        return false;
    }

    protected bool collidedWithBreakable(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.breakable))
        {
            collision.gameObject.GetComponent<BreakableWall>().dealWithProjectile(type);
            return true;
        }
        return false;
    }

    // This function applys initial movement to the projectile
    public virtual void applyMovement()
    {

    }
}
