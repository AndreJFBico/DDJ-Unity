using UnityEngine;
using System.Collections;

public class SortLayer : MonoBehaviour {

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        renderer.sortingOrder = Mathf.RoundToInt(-transform.position.z*10);
	}
	
	// Update is called once per frame
	void Update () {
	}
}
