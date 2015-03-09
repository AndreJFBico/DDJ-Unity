using UnityEngine;
using System.Collections;

public class PathAgent : MonoBehaviour
{
    public Vector3 startPosition;
    public float roamRadius;
    public Transform target;
    public float pathEndThreshold = 0.1f;
    //private PathManager manager;
    //private Seeker seeker;
    //private bool on_a_Path;
    private NavMeshAgent agent;

    public void Start()
    {
       // on_a_Path = false;
        //seeker = GetComponent<Seeker>();
        agent = GetComponent<NavMeshAgent>();
        FreeRoam();
        InvokeRepeating("checkRoaming", 1, 1);
        //manager = GameObject.Find("PathManager").GetComponent<PathManager>();
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.CompareTo("Player") == 0)
        {
            target = collision.transform;
        }
    }

    void checkRoaming()
    {
        if (AtEndOfPath())
            FreeRoam();
    }

    bool AtEndOfPath()
    {
        if (agent.remainingDistance <= agent.stoppingDistance + pathEndThreshold )
        {
            return true;
        }
        return false;
    }

    void FreeRoam()
    {
        {
            Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
            randomDirection += startPosition;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, roamRadius, 1);
            Vector3 finalPosition = hit.position;
            agent.destination = finalPosition;
        }
    }

    public void Update()
    {
        if(target != null)
        agent.SetDestination(target.position);
        /*if (manager.initialized && !on_a_Path)
        {           
            //Start a new path to the targetPosition, return the result to the OnPathComplete function
            seeker.StartPath(transform.position, target.position, OnPathComplete);
            on_a_Path = true;
        }*/
    }
}
