using UnityEngine;
using System.Collections;
using Includes;

public class BreakableWall : MonoBehaviour {

    private float health = 12;
    public BreakableWalls type;

	// Use this for initialization
	void Start () {
	
	}

    public void dealWithProjectile(Elements projType)
    {
        switch (type)
        {
            case BreakableWalls.NEUTRAL:
                switch (projType)
                {
                    case Elements.NEUTRAL:
                        health -= 1;
                        break;
                    default:
                        break;
                }
                break;
            case BreakableWalls.FIRE:
                switch (projType)
                {  
                    case Elements.FROST:
                        health -= 1;
                        break;
                    default:
                        break;
                }
                break;
            case BreakableWalls.EARTH:
                switch (projType)
                {
                    case Elements.FIRE:
                        health -= 1;
                        break;
                    default:
                        break;
                }
                break;
            case BreakableWalls.FROST:
                switch (projType)
                {
                    case Elements.EARTH:
                        health -= 1;
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        if(health == 8)
        {
            transform.localScale = new Vector3(0.66f, 0.66f, 0.66f);
        }
        if (health == 4)
        {
            transform.localScale = new Vector3(0.33f, 0.33f, 0.33f);
        }
        if(health <= 0)
            Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
