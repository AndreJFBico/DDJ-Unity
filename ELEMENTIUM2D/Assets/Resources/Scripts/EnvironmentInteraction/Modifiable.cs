using UnityEngine;
using System.Collections;
using Includes;

public class Modifiable : MonoBehaviour {

    protected float durability; //For Breakables it's like health and for "puddles" it's duration
    protected float maxDurability;

    protected virtual void Awake() { }
    public virtual void dealWithProjectile(Elements type, float damage){ }
    public virtual void dealWithProjectile(Elements type) { }
}
