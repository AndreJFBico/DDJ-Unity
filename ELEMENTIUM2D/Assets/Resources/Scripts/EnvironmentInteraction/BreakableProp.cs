using UnityEngine;
using System.Collections;
using Includes;

public class BreakableProp : Breakable {

	// Use this for initialization
	void Start () {
	
	}

    public override void dealWithProjectile(Elements type, float damage)
    {
        health -= 1;

        if(health <= 0)
        {
            dealWithDeath();
        }
    }

    protected virtual void dealWithDeath()
    {

    }

	// Update is called once per frame
	void Update () {
	
	}
}
