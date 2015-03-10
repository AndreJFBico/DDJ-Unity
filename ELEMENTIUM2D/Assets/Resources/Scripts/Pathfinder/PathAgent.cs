using UnityEngine;
using System.Collections;
using Includes;

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
        target = null;
        agent = transform.parent.GetComponent<NavMeshAgent>();
        roamRadius = Constants.enemyRoamRadius;
        InvokeRepeating("checkMovement", 0, 0.5f);
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.CompareTo("Player") == 0)
        {
            target = collision.transform;
        }
    }

    void checkMovement()
    {
        if (agent.hasPath)
        {
            if(AtEndOfPath())
            {
                if(target != null)
                {
                    agent.SetDestination(target.position);
                }
                else
                {
                    FreeRoam();
                }
            }
            else
            {
                //  To implement time limit to the follow of the agents make target be null
                if (target != null)
                {
                    agent.SetDestination(target.position);
                }
            }
        }
        else
        {
            if(target != null)
            {
                agent.SetDestination(target.position);
            }
            else
            {
                FreeRoam();
            }
        }
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
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += startPosition;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, roamRadius, 1);
        Vector3 finalPosition = hit.position;
        agent.destination = finalPosition;
    }

    public void Update()
    {
        //checkMovement();
        /*if (manager.initialized && !on_a_Path)
        {           
            //Start a new path to the targetPosition, return the result to the OnPathComplete function
            seeker.StartPath(transform.position, target.position, OnPathComplete);
            on_a_Path = true;
        }*/
    }
}
