using UnityEngine;
using System.Collections;
using Includes;

public class OilPuddle : ElementalyModifiable {

    private float damage;
    private float slow;
    public GameObject onFire;
    public bool canBurn = false;

    public OilPuddle previous;
    public OilPuddle next;


	// Use this for initialization
	void Start () {
        damage = 5;
        slow = 0.5f;
        durability = 1;
	}

    protected void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.CompareTo("Enemy") == 0)
        {
            if (canBurn)
                applyStatus(other, StatusEffects.BURNING, damage);
            else
            {
                applyStatus(other, StatusEffects.SLOW, slow);
            }
        }
    }

    public override void dealWithProjectile(Elements type)
    {
        if(type == Elements.FIRE)
        {
            burnPuddle();
        }
    }

    private void propagateFire()
    {
        if(previous != null)
        {
            previous.burnPuddle();
        }
        if(next != null)
        {
            next.burnPuddle();
        }
    }

    public void burnPuddle()
    {
        if (!canBurn)
        {
            onFire.SetActive(true);
            canBurn = true;
            propagateFire();
        }
        if((previous!= null && !previous.canBurn) || (next != null && !next.canBurn))
        {
            propagateFire();
        }
    }

    public virtual void dealWithEnemy(EnemyScript scrpt)
    {
        //scrpt.applyBurningStatus(damage);
    }

    public virtual void dealWithPlayer(Interactions scrpt)
    {
        //scrpt.applyBurningStatus(damage);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
