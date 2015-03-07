using UnityEngine;
using System.Collections;

public class ProjectileBehaviour : MonoBehaviour {

    protected float damage;

    protected virtual void Start() { }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    // This function applys initial movement to the projectile
    public virtual void applyMovement()
    {

    }
}
