using UnityEngine;
using System.Collections;
using Includes;

public class AbilityBehaviour : ElementiumMonoBehaviour
{
    protected Elements type;
    protected GameObject explosion;
    protected float damage;
    protected float startSpeed = 150;

    #region Start and Awake
    protected virtual void Start() { }

    protected virtual void Awake() { } 
    #endregion

    #region OnTrigger and OnCollision Functions
    public void destroyBehaviour()
    {
        createExplosion();
        Destroy(gameObject);
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        destroyBehaviour();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        enteredBreakableTrigger(other.gameObject);
    } 
    #endregion

    #region Collision and trigger behaviours
    protected bool collidedWith(GameObject collidedObj, float damage)
    {
        if (collidedObj.tag.CompareTo("Enemy") == 0)
        {
            Agent enemy = collidedObj.GetComponent<Agent>();
            collidedObj.GetComponent<EnemyScript>().playerSighted();
            enemy.takeDamage(damage, type);
            return true;
        }
        else if (LayerMask.NameToLayer("Player") == collidedObj.layer)
        {
            Agent player = collidedObj.GetComponent<Agent>();
            player.takeDamage(damage, type);
            return true;
        }
        return false;
    }

    protected bool collidedWithBreakable(GameObject collidedObj)
    {
        if (collidedObj.layer == LayerMask.NameToLayer(Constants.breakable))
        {
            collidedObj.GetComponent<Modifiable>().dealWithProjectile(type, damage);
            return true;
        }
        return false;
    }

    protected bool enteredBreakableTrigger(GameObject collidedObj)
    {
        if (collidedObj.layer == LayerMask.NameToLayer(Constants.elementalyModifiable))
        {
            collidedObj.GetComponent<Modifiable>().dealWithProjectile(type);
            return true;
        }
        return false;
    } 
    #endregion

    public virtual void createExplosion()
    {
        Instantiate(explosion, transform.position, explosion.transform.rotation);
    }

    // This function applys initial movement to the projectile
    public virtual void initiate(GameObject startingObject, float damage)
    {
    }
}
