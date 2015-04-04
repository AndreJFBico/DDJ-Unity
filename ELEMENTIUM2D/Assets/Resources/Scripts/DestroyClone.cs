using UnityEngine;
using System.Collections;

public class DestroyClone : MonoBehaviour {

    public float destroytimer = 1.5f;
	// Use this for initialization
	void Start () {
        Invoke("destroyClone", destroytimer);
	}
	
    private void destroyClone()
    {
        Destroy(gameObject);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
