using UnityEngine;
using System.Collections;
using Includes;
public class FireHeal : AbilityBehaviour
{

    private float timer = 0.0f;
    private bool alphaed = false;
    private GameObject sobj;

    protected override void Awake()
    {

    }

    public override void OnCollisionEnter(Collision collision)
    {

    }

    public override void OnTriggerEnter(Collider other)
    {
    }

    protected void Update()
    {
        timer += Time.deltaTime;
        if (timer >= AbilityStats.Fire.ability3.abilityTimer)
        {
            Agent ag = sobj.GetComponent<Agent>();
            ag.takeDamage(damage, type, true);
            Destroy(this);
        }
    }

    public override void initiate(GameObject startingObject, float dmg)
    {
        base.initiate(startingObject, dmg);
        damage = dmg;
        transform.position = startingObject.transform.position;
        transform.parent = startingObject.transform;
        transform.rotation = Quaternion.identity;
        sobj = startingObject;
        //ConstantForce constantForce = gameObject.AddComponent<ConstantForce>();
        //constantForce.relativeForce = new Vector3(0.0f, 0.0f, AbilityStats.Earth.ability2.movementForce);
    }
}
