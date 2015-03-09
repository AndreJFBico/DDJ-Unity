using UnityEngine;
using System.Collections;

public class PathAgent : MonoBehaviour
{
    public Transform target;
    //private PathManager manager;
    //private Seeker seeker;
    //private bool on_a_Path;
    private NavMeshAgent agent;

    public void Start()
    {
       // on_a_Path = false;
        //seeker = GetComponent<Seeker>();
        agent = GetComponent<NavMeshAgent>();
        //manager = GameObject.Find("PathManager").GetComponent<PathManager>();
    }

    public void Update()
    {
        agent.SetDestination(target.position);
        /*if (manager.initialized && !on_a_Path)
        {           
            //Start a new path to the targetPosition, return the result to the OnPathComplete function
            seeker.StartPath(transform.position, target.position, OnPathComplete);
            on_a_Path = true;
        }*/
    }
}
