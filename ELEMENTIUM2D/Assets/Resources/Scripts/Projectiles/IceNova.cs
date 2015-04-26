using UnityEngine;
using System.Collections;
using Includes;

public class IceNova : AbilityBehaviour
{

    protected override void Awake()
    {
        explosion = Resources.Load("Prefabs/Explosions/frostExplosion") as GameObject;
        damage = 1;
    }

    protected override void Start()
    {
        base.Start();
        Invoke("destroyClone", 6f);
        type = Elements.FROST;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.CompareTo("Enemy") == 0)
        {
            if (collision.gameObject.GetComponent<FrozenStatusEffect>() == null)
            {
                FrozenStatusEffect sse = collision.gameObject.AddComponent<FrozenStatusEffect>();
                sse.setDuration(5.0f);
                sse.applyStatusEffect(collision.gameObject.GetComponent<EnemyScript>());
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Unhitable"))
            return;
        base.OnCollisionEnter(collision);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * AbilityStats.Frost.ability3.movementForce);
    }

    private void destroyClone()
    {
        Destroy(gameObject);
    }

    public override void initiate(GameObject startingObject, float dmg)
    {
        base.initiate(startingObject, dmg);
        // Rotates the missile randomly
        damage = dmg;
        int result = Random.Range(0, 360);
        transform.Rotate(new Vector3(0.0f, result, 0.0f));
    }
}
