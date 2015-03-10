using UnityEngine;
using System.Collections;
using Includes;

public class BurningStatusEffect : StatusEffect
{
    private float burningTimer = 10;
    private float burningDamage = 0;
    private bool isBurning = false;


	// Use this for initialization
	void Start () {
	
	}

    public BurningStatusEffect(float dmg) : base(dmg)
    {
    }

    public override void applyStatusEffect(EnemyScript script)
    {
        applyBurningStatus(script);
    }

    private IEnumerator burning(EnemyScript script)
    {
        while (burningTimer > 0)
        {
            script.takeDamage(intensity, Elements.FIRE);
            burningTimer -= Time.deltaTime;
            yield return new WaitForSeconds(1);
        }
        isBurning = false;
        burningDamage = 0;
    }

    public void applyBurningStatus(EnemyScript script)
    {
        Debug.LogWarning("Taking Damage");
        if (isBurning)
        {
            if (intensity > burningDamage)
            {
                StopCoroutine("burning");
                StartCoroutine("burning", script);
                burningDamage = intensity;
            }
            burningTimer = 10;
        }
        else
        {
            isBurning = true;
            burningTimer = 10;
            burningDamage = intensity;
            StartCoroutine("burning", script);
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
