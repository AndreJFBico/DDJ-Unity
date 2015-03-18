using UnityEngine;
using System.Collections;
using Includes;

public class WaterPuddle : Breakable{

    public GameObject iceWall;

    // Use this for initialization
    void Start()
    {
        maxDurability = 5;
        durability = maxDurability;
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
            durability--;
            if (durability <= 0)
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
        durability = maxDurability;
    }
}
