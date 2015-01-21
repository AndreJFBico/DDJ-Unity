using UnityEngine;
using System.Collections;

public class CollisionBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Unhitable"))
        {

        }
        else Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
