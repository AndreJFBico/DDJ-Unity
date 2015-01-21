using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class ChaseTarget : MonoBehaviour {

    private NavMeshAgent agent;
    private GameObject player;

	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(player.transform.position);
	}
}
