using UnityEngine;
using System.Collections;
using Includes;

public class Agent : ElementiumMonoBehaviour
{

    public Transform healthbar_background;
    public Transform healthbar;
    protected SpriteRenderer figure;
    protected bool centerHealthBar = false;

    protected virtual void Awake()
    {
        // Finds the sprite associated to the agent
        figure = transform.gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    public virtual bool isHurt() { return false; }

    public virtual void OnTriggerExit(Collider collision) { }

    public virtual void OnTriggerEnter(Collider collision) { }

    public virtual void OnCollisionStay(Collision collision) { }

    public virtual void OnCollisionEnter(Collision collision) { }

    public virtual void OnCollisionExit(Collision collision) { }

    public virtual void takeDamage(float amount, Elements type, bool goTroughBlink, string source) { }

    public virtual void healSelf(float amount, Elements type) { }

    public virtual void setAlerted(bool val) { }

    public virtual bool getAlerted() { return false; }

    public virtual void applyStatusEffect(StatusEffect scrpt) { }

    public virtual void slowSelf(float intensity) { }

    public virtual void restoreMoveSpeed(float intensity) { }

    public virtual void createFloatingText(string message) { }
    public virtual void createFloatingText(string message, float time) { }

    public virtual void playerSighted() {}
}
