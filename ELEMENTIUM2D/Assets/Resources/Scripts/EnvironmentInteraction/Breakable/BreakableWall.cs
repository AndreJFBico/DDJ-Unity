using UnityEngine;
using System.Collections;
using Includes;

public class BreakableWall : Breakable {

    public BreakableWalls type;

	// Use this for initialization
    void Start()
    {
        maxDurability = 20;
        durability = maxDurability;
	}

    public override void dealWithProjectile(Elements projType, float damage)
    {
        switch (type)
        {
            case BreakableWalls.NEUTRAL:
                switch (projType)
                {
                    case Elements.NEUTRAL:
                        durability -= damage;
                        break;
                    default:
                        break;
                }
                break;
            case BreakableWalls.FIRE:
                switch (projType)
                {  
                    case Elements.WATER:
                        durability -= damage;
                        break;
                    default:
                        break;
                }
                break;
            case BreakableWalls.EARTH:
                switch (projType)
                {
                    case Elements.FIRE:
                        durability -= damage;
                        break;
                    default:
                        break;
                }
                break;
            case BreakableWalls.FROST:
                switch (projType)
                {
                    case Elements.EARTH:
                        durability -= damage;
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        if (durability <= maxDurability / 4)
        {
            transform.localScale = new Vector3(0.33f, 0.33f, 0.33f);
        }
        else if (durability <= maxDurability / 2)
        {
            transform.localScale = new Vector3(0.66f, 0.66f, 0.66f);
        }
        if (durability <= 0)
            Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
