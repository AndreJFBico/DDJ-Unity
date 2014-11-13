using UnityEngine;
using System.Collections;

public class LookAtPlayer : MonoBehaviour {

    private Transform player;
    public int yDist = 25;
    public int zDist = 35;

	// Use this for initialization
	void Start () {
        player = (GameObject.FindWithTag("Player") as GameObject).transform; 
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(player);
        transform.position = player.position + new Vector3(0, yDist, -zDist);
	}
}
