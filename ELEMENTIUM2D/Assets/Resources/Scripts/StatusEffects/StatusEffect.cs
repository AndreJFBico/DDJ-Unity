using UnityEngine;
using System.Collections;

public class StatusEffect : MonoBehaviour {

    protected float intensity;
    protected float duration;

	// Use this for initialization
	protected virtual void Start () {
	
	}

    public float Intensity{
        get
        {
            return intensity;
        }
        set
        {
            intensity = value;
        }
    }

    public float Duration
    {
        get
        {
            return duration;
        }
        set
        {
            duration = value;
        }
    }

    public virtual void initiate(float inten, float dur) { }

    public virtual void resetDuration(float dur){}

    public virtual void applyStatusEffect(Agent script){}
	
}
