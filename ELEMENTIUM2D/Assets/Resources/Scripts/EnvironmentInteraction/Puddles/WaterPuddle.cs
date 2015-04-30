using UnityEngine;
using System.Collections;
using Includes;

public class WaterPuddle : ElementalyModifiable{

    private GameObject iceWall;

    // Use this for initialization
    void Start()
    {
        maxDurability = 10;
        durability = maxDurability;
        iceWall = GameManager.Instance.IceWall;
    }

    protected override void dealWithAgent(Collider other)
    {
        applyStatus(other, StatusEffects.WET, 1);
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

        if(type == Elements.WATER)
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
