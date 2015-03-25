using UnityEngine;
using System.Collections;
using Includes;

public class IceWall : BreakableWall{

    private GameObject waterPuddle;

    // Use this for initialization
    void Start()
    {
        maxDurability = 50;
        durability = maxDurability;
        waterPuddle = GameManager.Instance.WaterPuddle;
    }

    public override void dealWithProjectile(Elements type, float damage)
    {
        if (type == Elements.EARTH)
        {
            durability -= damage;
            if (durability <= 0)
            {
                Destroy(gameObject);
                Destroy(this);
            }
        }

        if (type == Elements.FIRE)
        {
            durability -= damage;
            if (durability <= 0)
            {
                resetValues();
                genWaterPuddle();
                Destroy(gameObject);
                Destroy(this);
            }
            
        }
    }

    private void genWaterPuddle()
    {
        Instantiate(waterPuddle,transform.position, Quaternion.identity);
    }

    public void resetValues()
    {
        durability = maxDurability;
    }
}
