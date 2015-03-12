using UnityEngine;
using System.Collections;
using Includes;

public class BreakableWall : Breakable {

    public BreakableWalls type;

	// Use this for initialization
    void Start()
    {
        maxHealth = 50;
        health = maxHealth;
	}

    public override void dealWithProjectile(Elements projType, float damage)
    {
        switch (type)
        {
            case BreakableWalls.NEUTRAL:
                switch (projType)
                {
                    case Elements.NEUTRAL:
                        health -= damage;
                        break;
                    default:
                        break;
                }
                break;
            case BreakableWalls.FIRE:
                switch (projType)
                {  
                    case Elements.FROST:
                        health -= damage;
                        break;
                    default:
                        break;
                }
                break;
            case BreakableWalls.EARTH:
                switch (projType)
                {
                    case Elements.FIRE:
                        health -= damage;
                        break;
                    default:
                        break;
                }
                break;
            case BreakableWalls.FROST:
                switch (projType)
                {
                    case Elements.EARTH:
                        health -= damage;
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        if (health <= maxHealth/4)
        {
            transform.localScale = new Vector3(0.33f, 0.33f, 0.33f);
        }
        else if(health <= maxHealth/2)
        {
            transform.localScale = new Vector3(0.66f, 0.66f, 0.66f);
        }
        if(health <= 0)
            Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
