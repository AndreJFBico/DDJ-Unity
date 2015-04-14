using UnityEngine;
using System.Collections;
using Includes;

public class Agent : ElementiumMonoBehaviour
{

    public Transform healthbar_background;
    public Transform healthbar;
    protected SpriteRenderer figure;
    protected float maxHealth;
    protected float health;
    protected float damage;
    protected float defence;
    protected float waterResist;
    protected float earthResist;
    protected float fireResist;
    protected bool centerHealthBar = false;

    protected void Awake()
    {
        // Findes the sprite associated to the agent
        figure = transform.gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    /*protected virtual void OnGUI()
    {
        // Health bar
        Vector2 targetPos = healthbar_background.position;
        if (centerHealthBar)
        {
            targetPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.1f));
        }

        healthbar_background.position = targetPos;
        float percentage = health / maxHealth;
        healthbar.transform.localScale = new Vector3(percentage, 1.0f, 1.0f);
    }*/

    public bool isHurt()
    {
        return health < maxHealth;
    }

    public virtual void OnTriggerExit(Collider collision) { }

    public virtual void OnTriggerEnter(Collider collision) { }

    public virtual void OnCollisionStay(Collision collision) { }

    public virtual void OnCollisionEnter(Collision collision) { }

    public virtual void OnCollisionExit(Collision collision) { }

    public virtual void takeDamage(float amount, Elements type) { }

    public virtual void setAlerted(bool val) { }

    public virtual bool getAlerted() { return false; }

    public virtual void applyStatusEffect(StatusEffect scrpt) { }

    public virtual void slowSelf(float intensity) { }

    public virtual void restoreMoveSpeed(float intensity) { }

    public virtual void createFloatingText(string message) { }
}
