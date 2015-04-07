using UnityEngine;
using System.Collections;
using Includes;

public class ElementalyModifiable : Modifiable {
    
    //#############################################################
    //######################## MAIN FUNCTION ######################
    //#############################################################
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
        if (status == StatusEffects.WET)
        {
            applyWet(other, intensity);
        }
    }

    //#############################################################
    //################### AUXILIAR FUNCTIONS ######################
    //#############################################################
    private void applyBurning(Collider other, float intensity)
    {
        if (other.gameObject.GetComponent<WetStatusEffect>() != null)
        {
            Destroy(other.gameObject.GetComponent<WetStatusEffect>());
        }
        if (other.gameObject.GetComponent<BurningStatusEffect>() == null)
        {
            StatusEffect burn = other.gameObject.AddComponent<BurningStatusEffect>();
            burn.setIntensity(intensity);
            burn.setDuration(durability);
            other.gameObject.GetComponent<EnemyScript>().applyStatusEffect(burn);
        }
        else
        {
            //Need an extra check to see if damage > this damage
            StatusEffect burn = other.gameObject.GetComponent<BurningStatusEffect>();
            burn.resetDuration(durability);
        }
    }

    private void applySlow(Collider other, float intensity)
    {
        if (other.gameObject.GetComponent<SlowStatusEffect>() == null)
        {
            StatusEffect slow = other.gameObject.AddComponent<SlowStatusEffect>();
            slow.setIntensity(intensity);
            slow.setDuration(durability);
            other.gameObject.GetComponent<EnemyScript>().applyStatusEffect(slow);
        }
        else
        {
            //Need an extra check to see if damage > this damage
            StatusEffect slow = other.gameObject.GetComponent<SlowStatusEffect>();
            slow.resetDuration(durability);
        }
    }

    private void applyWet(Collider other, float intensity)
    {
        if (other.gameObject.GetComponent<BurningStatusEffect>() != null)
        {
            Destroy(other.gameObject.GetComponent<BurningStatusEffect>());
        }
        if (other.gameObject.GetComponent<WetStatusEffect>() == null)
        {
            StatusEffect wet = other.gameObject.AddComponent<WetStatusEffect>();
            wet.setDuration(durability);
            other.gameObject.GetComponent<EnemyScript>().applyStatusEffect(wet);
        }
        else
        {
            //Need an extra check to see if damage > this damage
            StatusEffect wet = other.gameObject.GetComponent<WetStatusEffect>();
            wet.resetDuration(durability);
        }
    }

    public virtual void dealWithEnemy(EnemyScript scrpt){}

    public virtual void dealWithPlayer(Interactions scrpt){}

}
