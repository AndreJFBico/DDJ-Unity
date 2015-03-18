using UnityEngine;
using System.Collections;
using Includes;

public class OilPuddle : ElementalyModifiable {

    private float damage;
    private float slow;
    public GameObject onFire;
    private bool canBurn = false;

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
            onFire.SetActive(true);
            canBurn = true;
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
