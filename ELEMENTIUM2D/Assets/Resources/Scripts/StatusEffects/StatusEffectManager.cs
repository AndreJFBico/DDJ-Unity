using UnityEngine;
using System.Collections;

public class StatusEffectManager{

	private static StatusEffectManager _instance = null;

    #region Initialization

    protected StatusEffectManager() { }
    // Singleton pattern implementation
    public static StatusEffectManager Instance { get { if (_instance == null) { _instance = new StatusEffectManager(); init(); } return _instance; } }

    private static void init()
    {
    }

    public void sceneInit()
    {
    }
 
	#endregion

    public void applyBurning(GameObject agent, float intensity, float durability)
    {
        if (agent.GetComponent<WetStatusEffect>() != null)
        {
            GodScript.destroyComponent(agent.GetComponent<WetStatusEffect>());
        }
        if (agent.GetComponent<BurningStatusEffect>() == null)
        {
            StatusEffect burn = agent.AddComponent<BurningStatusEffect>();
            burn.setIntensity(intensity);
            burn.setDuration(durability);
            agent.GetComponent<Agent>().applyStatusEffect(burn);
        }
        else
        {
            //Need an extra check to see if damage > this damage
            StatusEffect burn = agent.GetComponent<BurningStatusEffect>();
            burn.resetDuration(durability);
        }
    }

    public void applySlow(GameObject agent, float intensity, float durability)
    {
        float duration = 1;
        if (agent.GetComponent<SlowStatusEffect>() == null)
        {
            StatusEffect slow = agent.AddComponent<SlowStatusEffect>();
            slow.setIntensity(intensity);
            slow.setDuration(duration);
            agent.GetComponent<Agent>().applyStatusEffect(slow);
        }
        else
        {
            //Need an extra check to see if damage > this damage
            StatusEffect slow = agent.GetComponent<SlowStatusEffect>();
            slow.resetDuration(duration);
        }
    }

    public void applyWet(GameObject agent, float intensity, float durability)
    {
        if (agent.GetComponent<BurningStatusEffect>() != null)
        {
            GodScript.destroyComponent(agent.GetComponent<BurningStatusEffect>());
        }
        if (agent.GetComponent<WetStatusEffect>() == null)
        {
            StatusEffect wet = agent.AddComponent<WetStatusEffect>();
            wet.setDuration(durability);
            agent.GetComponent<Agent>().applyStatusEffect(wet);
        }
        else
        {
            //Need an extra check to see if damage > this damage
            StatusEffect wet = agent.GetComponent<WetStatusEffect>();
            wet.resetDuration(durability);
        }
    }

    public void applyStun(GameObject agent, float intensity, float durability)
    {
        StunnedStatusEffect sse = agent.AddComponent<StunnedStatusEffect>();
        sse.setDuration(durability);
        sse.applyStatusEffect(agent.GetComponent<EnemyScript>());
    }

    public void applyFrozen(GameObject agent, float intensity, float durability)
    {
        FrozenStatusEffect sse = agent.AddComponent<FrozenStatusEffect>();
        sse.setDuration(durability);
        sse.applyStatusEffect(agent.GetComponent<EnemyScript>());
    }

}

