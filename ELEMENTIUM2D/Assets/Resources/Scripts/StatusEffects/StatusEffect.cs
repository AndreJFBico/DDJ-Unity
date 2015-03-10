using UnityEngine;
using System.Collections;

public class StatusEffect : MonoBehaviour {

    protected float intensity;

	// Use this for initialization
	void Start () {
	
	}

    public StatusEffect(float dmg){
        intensity = dmg;
    }
    
    public virtual void applyStatusEffect(EnemyScript script)
    {

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
