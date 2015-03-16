using UnityEngine;
using System.Collections;
using Includes;

public class ElementalyModifiable : MonoBehaviour {

    protected float duration;

	// Use this for initialization
	void Start () {
	
	}

    protected virtual void applyStatus(Collider other, StatusEffects status, float intensity)
    {
        if (status == StatusEffects.BURNING)
        {
            applyBurning(other, intensity);
        }
        if (status == StatusEffects.SLOW)
        {
            applySlow(other, intensity);
        }
    }

    private void applyBurning(Collider other, float intensity)
    {
        if (other.gameObject.GetComponent<BurningStatusEffect>() == null)
        {
            StatusEffect burn = other.gameObject.AddComponent<BurningStatusEffect>();
            burn.setIntensity(intensity);
            burn.setDuration(duration);
            other.gameObject.GetComponent<EnemyScript>().applyStatusEffect(burn);
        }
        else
        {
            //Need an extra check to see if damage > this damage
            StatusEffect burn = other.gameObject.GetComponent<BurningStatusEffect>();
            burn.resetDuration(duration);
        }
    }

    private void applySlow(Collider other, float intensity)
    {
        if (other.gameObject.GetComponent<SlowStatusEffect>() == null)
        {
            StatusEffect slow = other.gameObject.AddComponent<SlowStatusEffect>();
            slow.setIntensity(intensity);
            slow.setDuration(duration);
            other.gameObject.GetComponent<EnemyScript>().applyStatusEffect(slow);
        }
        else
        {
            //Need an extra check to see if damage > this damage
            StatusEffect slow = other.gameObject.GetComponent<SlowStatusEffect>();
            slow.resetDuration(duration);
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
