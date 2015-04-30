using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Includes;

public class OilPuddle : ElementalyModifiable {

    private float damage;
    private float slow;
    public GameObject onFire;
    public bool canBurn = false;

    public List<OilPuddle> _connected;

    protected override void Awake()
    {
        _connected = new List<OilPuddle>();
    }

	// Use this for initialization
	void Start () {
        damage = 10;
        slow = 0.5f;
        durability = 20;
        StartCoroutine("reduceSize", 10);
        Invoke("destroySelf", durability);
	}

    protected override void dealWithAgent(Collider other)
    {
        if (canBurn)
            applyStatus(other, StatusEffects.BURNING, damage);
        else
        {
            applyStatus(other, StatusEffects.SLOW, slow);
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
        foreach (OilPuddle item in _connected)
	    {
            item.burnPuddle();
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
    }

    public void checkIfShouldBurn()
    {
        foreach (OilPuddle item in _connected)
        {
            if (item.canBurn)
                burnPuddle();
        }
    }

    private IEnumerator reduceSize(int steps)
    {
        float step = durability / steps;
        float decrease = 1-(1.0f / steps);
        while(true)
        {
            yield return new WaitForSeconds(step);
            transform.localScale *= (decrease);
        }
    }

    private void destroySelf()
    {
        OilPuddleManager.Instance.removeOilPuddle(this);
    }
}
