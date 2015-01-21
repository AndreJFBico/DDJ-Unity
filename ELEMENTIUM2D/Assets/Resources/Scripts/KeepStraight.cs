using UnityEngine;
using System.Collections;

public class KeepStraight : MonoBehaviour {

    private Quaternion rotation;
	// Use this for initialization

    void Awake()
    {
        rotation = transform.rotation;
    }

    void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.rotation = rotation;
	}
}
