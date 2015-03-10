using UnityEngine;
using System.Collections;
using Includes;

public class OilPuddle : ElementalyModifiable {

    private float damage = 5;
    public GameObject onFire;
    private bool canBurn = false;

	// Use this for initialization
	void Start () {
	
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
        scrpt.applyBurningStatus(damage);
    }

    public virtual void dealWithPlayer()
    {
        //Apply Burning
    }

	// Update is called once per frame
	void Update () {
	
	}
}
