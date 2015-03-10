using UnityEngine;
using System.Collections;
using Includes;

public class OilPuddle : ElementalyModifiable {

    
    public GameObject onFire;
    private bool canBurn = false;

	// Use this for initialization
	void Start () {
	    damage = 5;
	}

    protected override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
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
