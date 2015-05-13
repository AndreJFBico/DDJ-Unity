using UnityEngine;
using System.Collections;
using Includes;

public class IceWall : BreakableWall{

    private GameObject waterPuddle;

    // Use this for initialization
    void Start()
    {
        maxDurability = 20;
        durability = maxDurability;
        waterPuddle = GameManager.Instance.WaterPuddle;
        Invoke("destroy", 10);
    }

    void destroy()
    {
        Destroy(gameObject);
    }

    public override void dealWithProjectile(Elements type, float damage)
    {
        if (type == Elements.EARTH)
        {
            durability -= damage;
            if (durability <= 0)
            {
                destroy();
            }
        }

        if (type == Elements.FIRE)
        {
            durability -= damage;
            if (durability <= 0)
            {
                resetValues();
                genWaterPuddle();
                destroy();
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
