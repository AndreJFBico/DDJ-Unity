using UnityEngine;
using System.Collections;
using Includes;

public class Modifiable : ElementiumMonoBehaviour {

    protected float durability; //For Breakables it's like health and for "puddles" it's duration
    protected float maxDurability;

    public virtual void dealWithProjectile(Elements type, float damage)
    { }
    public virtual void dealWithProjectile(Elements type)
    { }
}
