using UnityEngine;
using System.Collections;

public class DestroyClone : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("destroyClone", 1.5f);
	}
	
    private void destroyClone()
    {
        Destroy(gameObject);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
