using UnityEngine;
using System.Collections;
using Includes;

public class Breakable : MonoBehaviour {

    protected float health;
    protected float maxHealth;
	// Use this for initialization
	void Start () {
	
	}

    public virtual void dealWithProjectile(Elements type, float damage)
    { }

	// Update is called once per frame
	void Update () {
	
	}
}
