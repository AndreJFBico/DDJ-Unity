using UnityEngine;
using System.Collections;
using Includes;

public class EnemyScript : Agent
{  
    //private SpawnScript script;

    protected Elements type;
    protected float visionRadiusValue = 5.46f;

    protected SpawnScript spawnScript;
    protected PathAgent pathAgent;


	// Use this for initialization
    protected virtual void Start()
    {
        Vector2 targetPos = Camera.main.WorldToScreenPoint(transform.position);
        healthbar_background.position = targetPos;
        pathAgent = GetComponentInChildren<PathAgent>();
	}
	
	// Update is called once per frame
    protected virtual void Update()
    {
        
	}

    protected virtual void LateUpdate()
    {
        transform.position.Set(transform.position.x, 0.1f, transform.position.z);
    }

    public override void OnCollisionStay(Collision collision)
    {
        base.OnCollisionStay(collision);
    }

    public override void OnCollisionExit(Collision collision)
    {
        base.OnCollisionExit(collision);
    }

    public override void OnCollisionEnter(Collision collision)
    {
        //
        base.OnCollisionEnter(collision);
    }

    /*protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.CompareTo("Projectile") == 0)
        {
            collision.gameObject.GetComponent<ProjectileBehaviour>().handleCollision(transform);
        }
    }*/

    public void applyStatusEffect(StatusEffect scrpt)
    {
        scrpt.applyStatusEffect(this);
    }

    
    public override void takeDamage(float amount, Elements type)
    {
        switch (type)
        {
            case Elements.NEUTRAL:
                health -= amount * (1-((defence + waterResist + earthResist + fireResist) / 100.0f));
                break;

            case Elements.FIRE:
                health -= amount * (1-((defence + fireResist) / 100.0f));
                break;

            case Elements.EARTH:
                health -= amount * (1-((defence + earthResist) / 100.0f));
                break;

            case Elements.FROST:
                health -= amount * (1-((defence + waterResist) / 100.0f));
                break;

            default:
                break;
        }
        if(health <= 0)
        {
            Eliminate();
        }
    }

    private void Eliminate()
    {
        if (spawnScript != null)
        {
            // I was spawned!!!
            health = maxHealth;
            spawnScript.despawn(transform);
        }
        else Destroy(gameObject);
    }

    // Is initiated by the spawner
    public void setSpawner(SpawnScript scrpt)
    {
        spawnScript = scrpt;
    }

    public float getDamage()
    {
        return damage;
    }

    public Elements getElementType()
    {
        return type;
    }
}
