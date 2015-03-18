using UnityEngine;
using System.Collections;
using Includes;

public class IceWall : BreakableWall{

    public GameObject waterPuddle;

    // Use this for initialization
    void Start()
    {
        maxDurability = 5;
        durability = maxDurability;
    }

    public override void dealWithProjectile(Elements type, float damage)
    {
        if (type == Elements.EARTH)
        {
            durability -= damage;
            if (durability <= 0)
            {
                Destroy(transform.parent.gameObject);
            }
        }

        if (type == Elements.FIRE)
        {
            durability -= damage;
            if (durability <= 0)
            {
                resetValues();
                waterPuddle.SetActive(true);
                gameObject.SetActive(false);
            }
            
        }
    }

    public void resetValues()
    {
        durability = maxDurability;
    }
}
