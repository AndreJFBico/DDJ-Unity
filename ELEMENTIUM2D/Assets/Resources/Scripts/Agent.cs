using UnityEngine;
using System.Collections;
using Includes;

public class Agent : MonoBehaviour {

    protected float maxHealth;
    protected float health;
    protected float damage;
    protected float defence;
    protected float waterResist;
    protected float earthResist;
    protected float fireResist;

    public virtual void takeDamage(float amount, Elements type)
    {

    }
}
