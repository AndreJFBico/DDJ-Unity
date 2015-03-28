using UnityEngine;
using System.Collections;
using Includes;

public class EarthShield : AbilityBehaviour {

    public override void initiate(GameObject startingObject)
    {
        var center = startingObject.transform.position;
        transform.parent = startingObject.transform;
        //ConstantForce constantForce = gameObject.AddComponent<ConstantForce>();
        //constantForce.relativeForce = new Vector3(0.0f, 0.0f, AbilityStats.Earth.ability2.movementForce);
    }
}
