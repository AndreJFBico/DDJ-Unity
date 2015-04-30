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
        StatusEffectManager.Instance.applyBurning(other.gameObject, intensity, durability);
    }

    private void applySlow(Collider other, float intensity)
    {
        StatusEffectManager.Instance.applySlow(other.gameObject, intensity, durability);
    }

    private void applyWet(Collider other, float intensity)
    {
        StatusEffectManager.Instance.applyWet(other.gameObject, intensity, durability);
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Agent>() != null)
        {
            dealWithAgent(other);
        }
    }

    public virtual void dealWithEnemy(EnemyScript scrpt){}

    protected virtual void dealWithAgent(Collider other) { }

}
