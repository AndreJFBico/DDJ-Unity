using UnityEngine;
using System.Collections;
using Includes;

public class ProjectileBehaviour : MonoBehaviour
{

    protected float damage;
    protected Elements type;
    protected GameObject explosion;
    protected virtual void Start() { }

    protected virtual void Awake()
    {

    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        createExplosion();
        Destroy(gameObject);
    }

    protected bool collidedWith(Collision collision, float damage)
    {
        if ((collision.gameObject.tag.CompareTo("Enemy") == 0) || (LayerMask.NameToLayer("Player") == collision.gameObject.layer))
        {
            Agent enemy = collision.gameObject.GetComponent<Agent>();
            enemy.takeDamage(damage, type);
            return true;
        }
        return false;
    }

    protected bool collidedWithBreakable(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.breakable))
        {
            collision.gameObject.GetComponent<Breakable>().dealWithProjectile(type, damage);
            return true;
        }
        return false;
    }

    protected bool enteredBreakableTrigger(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(Constants.elementalyModifiable))
        {
            other.gameObject.GetComponent<ElementalyModifiable>().dealWithProjectile(type);
            return true;
        }
        return false;
    }

    public virtual void createExplosion()
    {
        Instantiate(explosion, transform.position, explosion.transform.rotation);
    }

    // This function applys initial movement to the projectile
    public virtual void applyMovement()
    {

    }
}
