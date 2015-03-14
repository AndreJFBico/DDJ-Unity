using UnityEngine;
using System.Collections;
using Includes;

public class Agent : MonoBehaviour {

    public RectTransform healthbar_background;
    public RectTransform healthbar;
    protected float maxHealth;
    protected float health;
    protected float damage;
    protected float defence;
    protected float waterResist;
    protected float earthResist;
    protected float fireResist;
    protected bool centerHealthBar = false;

    // Health bar
    protected virtual void OnGUI()
    {
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
    }

    public virtual void Update()
    {

    }

    public virtual void OnTriggerStay(Collider collision)
    {

    }

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
}
