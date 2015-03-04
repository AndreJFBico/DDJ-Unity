using UnityEngine;
using System.Collections;
using Includes;

public class EnemyScript : MonoBehaviour
{
    public RectTransform healthbar_background;
    public RectTransform healthbar;
    
    private float health = 10;
    //private SpawnScript script;

    protected Elements type;
    protected float maxHealth = 100;
    protected float damage;
    protected float defence;
    protected float waterResist;
    protected float earthResist;
    protected float fireResist;

	// Use this for initialization
    protected virtual void Start()
    {
        Vector2 targetPos = Camera.main.WorldToScreenPoint(transform.position);
        healthbar_background.position = targetPos;
        health = maxHealth;
	}
	
	// Update is called once per frame
    protected virtual void Update()
    {

	}

    // Health bar
    protected virtual void OnGUI()
    {
        Vector2 targetPos;
        targetPos = Camera.main.WorldToScreenPoint(transform.position);
        healthbar_background.position = targetPos;
        float percentage = health / maxHealth;
        //float distance = percentage * healthbar.rect.width;
        //float desiredX = healthbar.position.x + distance;
        healthbar.transform.localScale = new Vector3(percentage, 1.0f, 1.0f);
    }

    /*protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.CompareTo("Projectile") == 0)
        {
            collision.gameObject.GetComponent<ProjectileBehaviour>().handleCollision(transform);
        }
    }*/

    public void takeDamage(float amount, Elements type)
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
        Destroy(gameObject);
    }

    // Is initiated by the spawner
    public void setSpawner(SpawnScript scrpt)
    {
        //script = scrpt;
    }
}
