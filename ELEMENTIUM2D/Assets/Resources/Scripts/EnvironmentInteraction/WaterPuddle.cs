using UnityEngine;
using System.Collections;
using Includes;

public class WaterPuddle : Breakable{

    private GameObject iceWall;

    // Use this for initialization
    void Start()
    {
        maxDurability = 10;
        durability = maxDurability;
        iceWall = GameManager.Instance.IceWall;
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
                Destroy(gameObject);
                Destroy(this);
            }
        }

        if(type == Elements.FROST)
        {
            resetValues();
            genIceWall();
            Destroy(gameObject);
            Destroy(this);
        }
    }

    private void genIceWall()
    {
        Instantiate(iceWall, transform.position, Quaternion.identity);
    }

    public void resetValues()
    {
        durability = maxDurability;
    }
}
