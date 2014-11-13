using UnityEngine;
using System.Collections;

public class PlayerSighted : MonoBehaviour {

    private NavMeshAgent nav;
    private Vector3 dest;

    private  NavMeshHit nmhit;

    private GameObject player;

	// Use this for initialization
	void Start () {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        StartCoroutine("doStuff");
	}
	
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
        }
    }

    private IEnumerator doStuff()
    {
        while (true)
        {
            nav.CalculatePath(dest, nav.path);
            nav.SetPath(nav.path);
            //nav.FindClosestEdge(out nmhit);
            nav.SetDestination(dest);
            nav.Move(nmhit.position); 
            yield return new WaitForSeconds(0.5f);
        }
    }


	// Update is called once per frame
    void Update()
    {
        dest = player.transform.position;
	}
}
