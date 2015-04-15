using UnityEngine;
using System.Collections;
using Includes;

public class EarthStun : AbilityBehaviour {

    protected override void Awake()
    {
        explosion = Resources.Load("Prefabs/Explosions/earthExplosion") as GameObject;
    }

    protected override void Start()
    {
        base.Start();
        //This has to change one thing is the damage the enemy does when it touches the player the other is the ranged projectile damage
        damage = EnemyStats.Earth.damage;
        type = Elements.EARTH;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.CompareTo("Enemy") == 0)
        {
            if (collision.gameObject.GetComponent<StunnedStatusEffect>() == null)
            {
                StunnedStatusEffect sse = collision.gameObject.AddComponent<StunnedStatusEffect>();
                sse.setDuration(5.0f);
                sse.applyStatusEffect(collision.gameObject.GetComponent<EnemyScript>());
            }
        }
        if (collidedWith(collision.gameObject, damage)) ;
        else if (collidedWithBreakable(collision.gameObject)) ;
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Unhitable"))
            return;
        base.OnCollisionEnter(collision);
    }


    public override void initiate(GameObject startingObject)
    {
        ConstantForce constantForce = gameObject.AddComponent<ConstantForce>();
        constantForce.relativeForce = new Vector3(0.0f, 0.0f, AbilityStats.Earth.ability2.movementForce);
    }
}
