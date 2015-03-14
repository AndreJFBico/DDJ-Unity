using UnityEngine;
using System.Collections;
using Includes;

public class ElementalyModifiable : MonoBehaviour {

    protected float damage;
    protected float duration;

	// Use this for initialization
	void Start () {
	
	}

    protected virtual void OnTriggerStay(Collider other)
    {
//        if()
        if (other.gameObject.tag.CompareTo("Enemy") == 0)
        {
            if (other.gameObject.GetComponent<BurningStatusEffect>() == null)
            {
                StatusEffect burn = other.gameObject.AddComponent<BurningStatusEffect>();
                burn.setIntensity(damage);
                burn.setDuration(duration);
                other.gameObject.GetComponent<EnemyScript>().applyStatusEffect(burn);
            }
            else
            {

            }
        }
    }

    public virtual void dealWithProjectile(Elements type)
    {

    }

    public virtual void dealWithEnemy(EnemyScript scrpt)
    {
        
    }

    public virtual void dealWithPlayer(Interactions scrpt)
    {

    }

	// Update is called once per frame
	void Update () {
	
	}
}
