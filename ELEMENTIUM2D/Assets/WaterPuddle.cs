using UnityEngine;
using System.Collections;
using Includes;

public class WaterPuddle : ElementalyModifiable{

    private float health;
    private float maxHealth;
    public GameObject iceWall;

    // Use this for initialization
    void Start()
    {
        maxHealth = 5;
        health = maxHealth;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.CompareTo("Enemy") == 0)
        {
        }
    }

    public override void dealWithProjectile(Elements type)
    {
        if (type == Elements.FIRE)
        {
            health--;
            if(health <= 0)
            {
                Destroy(transform.parent.gameObject);
            }
        }

        if(type == Elements.FROST)
        {
            resetValues();
            iceWall.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void resetValues()
    {
        health = 5;
    }
}
