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

    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag.CompareTo("Enemy") == 0)
        {
            if(collider.gameObject.GetComponent<StunnedStatusEffect>() == null)
            {
                StunnedStatusEffect sse = collider.gameObject.AddComponent<StunnedStatusEffect>();
                sse.setDuration(2.0f);
                sse.applyStatusEffect(collider.gameObject.GetComponent<EnemyScript>());
            }             
        }
    }

    public override void initiate(GameObject startingObject)
    {
        transform.parent = startingObject.transform;
    }
}
