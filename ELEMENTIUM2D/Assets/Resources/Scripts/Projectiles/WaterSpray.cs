using UnityEngine;
using System.Collections;
using Includes;

public class WaterSpray : ProjectileBehaviour {

    private ConstantForce constantForce;
    private SpriteRenderer spriteR;
    private float time = 0.1f;
    private bool waterPuddle = false;
    private bool deltDamage = false;

    protected override void Awake()
    {
        spriteR = GetComponentInChildren<SpriteRenderer>();
        explosion = Resources.Load("Prefabs/Explosions/frostExplosion") as GameObject;
    }

    protected override void Start()
    {
        base.Start();
        damage = AbilityStats.Frost.ability2.damage;
        type = Elements.FROST;
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if(!waterPuddle)
        {
            if (!deltDamage && collidedWith(collision, damage)) 
                deltDamage = true;
            else if (collidedWithBreakable(collision)) ;
            else if (collision.gameObject.layer == LayerMask.NameToLayer("Unhitable"))
                return;
        }
    }

    protected void Update()
    {
        time += Time.deltaTime;
        if (Vector3.Dot(transform.GetComponent<Rigidbody>().velocity, transform.forward) < 0.0f)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            GetComponent<ConstantForce>().enabled = false;
            waterPuddle = true;
            //transform.gameObject.layer = LayerMask.GetMask("Unhitable");
        }
        if(time >= AbilityStats.Frost.ability2.deathTimer)
        {
            // Kill it
            Destroy(gameObject);
        }
        else
        {
            // Set alpha acordingly
            Color color = spriteR.material.GetColor("_TintColor");
            color.a =  1.0f - (time / AbilityStats.Frost.ability2.deathTimer);
            spriteR.material.SetColor("_TintColor", color);
        }
    }

    public override void initiate(GameObject startingObject)
    {
        // Rotates the missile randomly
        float randomVal = Random.Range(-15, 15);
        transform.Rotate(new Vector3(0.0f, randomVal, 0.0f));

        randomVal = Random.Range(70.0f, 105.0f);
        transform.GetComponent<Rigidbody>().AddExplosionForce(randomVal, transform.position - transform.forward * 2.0f, 5.0f);
        constantForce = gameObject.AddComponent<ConstantForce>();
        randomVal = Random.Range(0.9f, 1.5f);
        constantForce.force = -transform.forward * randomVal;
    }
}
