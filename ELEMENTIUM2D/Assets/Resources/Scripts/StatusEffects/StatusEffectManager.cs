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
        else if (agent.GetComponent<BurningStatusEffect>() == null)
        {
            StatusEffect burn = agent.AddComponent<BurningStatusEffect>();
            burn.initiate(intensity, durability);
            agent.GetComponent<Agent>().applyStatusEffect(burn);
        }
        else
        {
            //Need an extra check to see if damage > this damage
            BurningStatusEffect burn = agent.GetComponent<BurningStatusEffect>();
            if (burn.getRemainderDamage() < intensity)
                burn.initiate(intensity, durability);
        }
    }

    public void applySlow(GameObject agent, float intensity, float durability)
    {
        float duration = 1;
        if (agent.GetComponent<SlowStatusEffect>() == null)
        {
            StatusEffect slow = agent.AddComponent<SlowStatusEffect>();
            slow.initiate(intensity, durability);
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
        else if (agent.GetComponent<WetStatusEffect>() == null)
        {
            StatusEffect wet = agent.AddComponent<WetStatusEffect>();
            wet.initiate(intensity, durability);
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
        sse.initiate(intensity, durability);
        sse.applyStatusEffect(agent.GetComponent<EnemyScript>());
    }

    public void applyFrozen(GameObject agent, float intensity, float durability)
    {
        FrozenStatusEffect sse = agent.AddComponent<FrozenStatusEffect>();
        sse.initiate(intensity, durability);
        sse.applyStatusEffect(agent.GetComponent<EnemyScript>());
    }

}

