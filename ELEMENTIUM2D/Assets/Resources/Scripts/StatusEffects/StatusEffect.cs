using UnityEngine;
using System.Collections;

public class StatusEffect : MonoBehaviour {

    protected float intensity;
    protected float duration;

	// Use this for initialization
	protected virtual void Start () {
	
	}

    public virtual void setIntensity(float dmg){
        intensity = dmg;
    }

    public virtual void setDuration(float dur)
    {
        duration = dur;
    }

    public virtual void resetDuration(float dur){}

    public virtual void applyStatusEffect(Agent script){}
	
}
