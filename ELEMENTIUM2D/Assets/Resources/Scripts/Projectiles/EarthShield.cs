using UnityEngine;
using System.Collections;
using Includes;

public class EarthShield : AbilityBehaviour {

    private float timer = 0.0f;
    private float blinkIncrement = 0.5f;
    private float blinkTimer = 0.5f;
    private bool alphaed = false;

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
        if(timer >= AbilityStats.Earth.ability2.abilityTimer)
        {
            Destroy(gameObject);
        }
        else
        {
            if (timer >= blinkTimer)
            {
                blink();
                blinkTimer = timer + blinkIncrement;
                blinkIncrement = blinkIncrement * (timer / blinkTimer);
            }
        }
    }

    void blink()
    {
        if (!alphaed)
        {
            foreach(SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
            {
                sr.color = new Color(1f, 1f, 1f, 0.50f);
            }
            alphaed = true;
        }
        else
        {
            foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
            {
                sr.color = new Color(1f, 1f, 1f, 1.0f);
            }
            alphaed = false;
        }
    }

    public override void initiate(GameObject startingObject)
    {
        transform.position = startingObject.transform.position;
        transform.parent = startingObject.transform;
        transform.rotation = Quaternion.identity;
        //ConstantForce constantForce = gameObject.AddComponent<ConstantForce>();
        //constantForce.relativeForce = new Vector3(0.0f, 0.0f, AbilityStats.Earth.ability2.movementForce);
    }
}
