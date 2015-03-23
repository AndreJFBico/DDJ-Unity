using UnityEngine;
using System.Collections;

public class GatherAllUis : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject[] uiElements = GameObject.FindGameObjectsWithTag("Ui");

        foreach(GameObject obj in uiElements)
        {
            obj.transform.parent = transform;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
