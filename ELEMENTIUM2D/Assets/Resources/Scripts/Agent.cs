using UnityEngine;
using System.Collections;
using Includes;

public class Agent : MonoBehaviour {

    public RectTransform healthbar_background;
    public RectTransform healthbar;
    public RectTransform alertedSign;
    protected Sprite figure;
    protected float maxHealth;
    protected float health;
    protected float damage;
    protected float defence;
    protected float waterResist;
    protected float earthResist;
    protected float fireResist;
    protected bool centerHealthBar = false;
    protected bool isAlerted = false;
    
    protected void Awake()
    {
        // Findes the sprite associated to the agent
        figure = transform.gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
    }

    protected virtual void OnGUI()
    {
        // Health bar
        Vector2 targetPos = healthbar_background.position;
        if(centerHealthBar)
        {
            targetPos = Camera.main.WorldToScreenPoint(transform.position);
        }
        
        healthbar_background.position = targetPos;
        float percentage = health / maxHealth;
        //float distance = percentage * healthbar.rect.width;
        //float desiredX = healthbar.position.x + distance;
        healthbar.transform.localScale = new Vector3(percentage, 1.0f, 1.0f);

        // Alerted sign
        if (isAlerted)
        {
            alertedSign.transform.gameObject.SetActive(true);
        }
        else
        {
            alertedSign.transform.gameObject.SetActive(false);
        }
        alertedSign.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y, transform.position.z + figure.bounds.extents.y / 2.0f));
    }

    public virtual void Update()
    {

    }

    /*public virtual void OnTriggerStay(Collider collision)
    {

    }*/

    public virtual void OnTriggerExit(Collider collision)
    {

    }

    public virtual void OnTriggerEnter(Collider collision)
    {

    }

    public virtual void OnCollisionStay(Collision collision)
    {

    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        
    }

    public virtual void OnCollisionExit(Collision collision)
    {

    }

    public virtual void takeDamage(float amount, Elements type)
    {

    }

    public virtual void setAlerted(bool val)
    {
        isAlerted = val;
    }
}
