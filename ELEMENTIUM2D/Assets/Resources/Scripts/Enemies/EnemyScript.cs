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
    protected SpawnScript spawnScript;

    private float burningTimer = 10;
    private float burningDamage = 0;
    private bool isBurning = false;


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

    void OnTriggerStay(Collider other)
    {
        Debug.LogWarning("EnteredTrigger");
        if (other.gameObject.layer == LayerMask.NameToLayer(Constants.elementalyModifiable))
        {
            other.gameObject.GetComponent<ElementalyModifiable>().dealWithEnemy(this);
        }
    }

    private IEnumerator burning(float damage)
    {
        while(burningTimer > 0)
        {
            takeDamage(damage, Elements.FIRE);
            burningTimer -= Time.deltaTime;
            yield return new WaitForSeconds(1);
        }
        isBurning = false;
        burningDamage = 0;
    }

    public void applyBurningStatus(float damage)
    {
        Debug.LogWarning("Taking Damage");
        if(isBurning)
        {
            if (damage > burningDamage)
            {
                StopCoroutine("burning");
                StartCoroutine("burning", damage);
                burningDamage = damage;
            }
            burningTimer = 10;
        }
        else
        {
            isBurning = true;
            burningTimer = 10;
            burningDamage = damage;
            StartCoroutine("burning", damage);
        }
    }

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
}
