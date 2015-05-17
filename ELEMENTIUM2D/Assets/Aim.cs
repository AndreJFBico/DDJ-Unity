using UnityEngine;
using System.Collections;

public class Aim : MonoBehaviour {

    LineRenderer lr;

	// Use this for initialization
	void Start () {
        lr = GetComponent<LineRenderer>();
        lr.SetVertexCount(3);
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("AimAssist"))
        {
            lr.enabled = lr.enabled ? false : true;
        }
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, transform.position + transform.up);
        lr.SetPosition(2, transform.position + transform.up*5);
	}
}
