using UnityEngine;
using System.Collections;
using Includes;

public class EnemyScript : Agent
{  
    //private SpawnScript script;

    protected Elements type;
    protected bool isAlerted = false;
    protected float rangedRadius;

    protected SpawnScript spawnScript;
    protected PathAgent pathAgent;
    protected GameObject gui;
    protected GameObject myGui;
    public RectTransform alertedSign;

	// Use this for initialization
    protected virtual void Awake()
    {
        base.Awake();
        Vector2 targetPos = Camera.main.WorldToScreenPoint(transform.position);
        healthbar_background.position = targetPos;
        pathAgent = GetComponentInChildren<PathAgent>();
        centerHealthBar = true;
        gui = GameObject.Find("GUI");
        myGui = transform.FindChild("Ui").gameObject;
        sendGuiToCanvas();
	}

    protected override void OnGUI()
    {
        base.OnGUI();

        // Alerted sign
        if (isAlerted)
        {
            if(pathAgent.hasTarget())
            {
                alertedSign.transform.gameObject.SetActive(true);
            }
        }
        else
        {
            alertedSign.transform.gameObject.SetActive(false);
        }
        alertedSign.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z + 0.1f));

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

    public void sendGuiToCanvas()
    {
        myGui.transform.parent = gui.transform;
    }

    public void retrieveGuiFromCanvas()
    {
        myGui.transform.parent = transform;
    }

    private void Eliminate()
    {
        if (spawnScript != null)
        {
            // I was spawned!!!
            health = maxHealth;
            spawnScript.despawn(transform);
        }
        else
        {
            retrieveGuiFromCanvas();
            Destroy(gameObject);
        } 
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

    public override void setAlerted(bool val)
    {
        isAlerted = val;
    }

    public override bool getAlerted()
    {
        return isAlerted;
    }

    public void stop()
    {
        pathAgent.stop();
    }

    public void restart(bool retarget)
    {
        pathAgent.restart(retarget);
    }

    public bool inRangeWithPlayer()
    {
        return (Vector3.Distance(GameManager.Instance.Player.transform.position, transform.position) <= rangedRadius);
    }

    public void playerSighted()
    {
        if (pathAgent != null)
        {
            pathAgent.playerSighted(GameManager.Instance.Player.transform);
            pathAgent.stopChasing();
        }
    }
}
