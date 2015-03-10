using UnityEngine;
using System.Collections;
using Includes;

public class ElementalyModifiable : MonoBehaviour {

    protected float damage;

	// Use this for initialization
	void Start () {
	
	}

    protected virtual void OnTriggerStay(Collider other)
    {
        Debug.LogWarning("EnteredTrigger");
        if (other.gameObject.tag.CompareTo("Enemy") == 0)
        {
            StatusEffect burn = new BurningStatusEffect(damage);
            other.gameObject.GetComponent<EnemyScript>().applyStatusEffect(burn);
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
