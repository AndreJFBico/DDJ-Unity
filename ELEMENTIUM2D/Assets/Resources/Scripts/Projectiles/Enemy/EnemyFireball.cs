using UnityEngine;
using System.Collections;
using Includes;

public class EnemyFireball : AbilityBehaviour
{

    protected override void Awake()
    {
        explosion = Resources.Load("Prefabs/Explosions/fireExplosion") as GameObject;
        damage = 1;
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

    protected override bool collidedWith(GameObject collidedObj, float damage)
    {
        if (collidedObj.tag.CompareTo("Enemy") == 0)
        {
            Agent enemy = collidedObj.GetComponent<Agent>();
            collidedObj.GetComponent<EnemyScript>().playerSighted();
            enemy.takeDamage(damage, type);
            StatusEffectManager.Instance.applyBurning(enemy.gameObject, damage / 2, 5);
            return true;
        }
        else if (LayerMask.NameToLayer("Player") == collidedObj.layer)
        {
            Agent player = collidedObj.GetComponent<Agent>();
            player.takeDamage(damage, type);
            StatusEffectManager.Instance.applyBurning(player.gameObject, damage / 2, 5);
            return true;
        }
        return false;
    }

    public override void initiate(GameObject startingObject, float dmg)
    {
        base.initiate(startingObject, dmg);
        GetComponent<Rigidbody>().AddForce(transform.forward * startSpeed);
        damage = dmg;
        ConstantForce constantForce = gameObject.AddComponent<ConstantForce>();
        constantForce.relativeForce = new Vector3(0.0f, 0.0f, EnemyStats.FireRanged.movementForce);
    }
}
