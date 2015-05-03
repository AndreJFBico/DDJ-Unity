using UnityEngine;
using System.Collections;
using Includes;

public class PathAgent : MonoBehaviour
{
    //Spawning position or startposition
    public Vector3 startPosition;
    public float roamRadius;
    public Transform target;
    public float pathEndThreshold = 0.1f;
    //private PathManager manager;
    //private Seeker seeker;
    //private bool on_a_Path;
    private NavMeshAgent agent;
    private Agent agentScrpt;
    private float previousSpeed;
    private Transform previousTarget;
    private bool imobilized = false;

    private bool inSight = false;

    public void Awake() 
    {
        target = null;
        agent = transform.parent.GetComponent<NavMeshAgent>();
        roamRadius = Constants.enemyRoamRadius;
        //InvokeRepeating("checkMovement", 0, 0.5f);
        agentScrpt = transform.parent.gameObject.GetComponent<Agent>();
        previousSpeed = agent.speed;   
    } 

    public void playerSighted(Transform player)
    {
        inSight = true;
        agentScrpt.setAlerted(true);
        target = player;
        if (imobilized)
            agent.speed = 0;
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.CompareTo("Player") == 0)
        {
            playerSighted(collision.transform);
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag.CompareTo("Player") == 0)
        {
            stopChasing();
        }
    }

    public void stop()
    {
        agent.speed = 0.0f;
        inSight = false;
        previousTarget = target;
        target = null;
        imobilized = true;
    }

    public void restart(bool chase)
    {
        imobilized = false;
        agent.speed = previousSpeed;
        if(chase)
        {
            if(previousTarget != null)
            {
                inSight = true;
                target = previousTarget;
            }
        }
    }

    public void instantStopChase()
    {
        inSight = false;
        Invoke("stopChase", 0);
    }

    public void stopChasing()
    {
        inSight = false;
        Invoke("stopChase", 1);
    }

    private void stopChase()
    {
        if (!inSight)
            target = null;
    }

    public bool hasTarget()
    {
        return target != null;
    }

    void OnEnable()
    {
        StartCoroutine("checkMovement");
    }

    IEnumerator checkMovement()
    {
        yield return new WaitForSeconds(.1f);
        for ( ; ; )
        {
            if (agent.hasPath)
            {
                if (AtEndOfPath())
                {
                    if (hasTarget())
                    {
                        agent.SetDestination(target.position);
                    }
                    else if(agent.isOnNavMesh)
                    {
                        FreeRoam();
                    }
                }
                else
                {
                    //  To implement time limit to the follow of the agents make target be null
                    if (hasTarget())
                    {
                        agent.SetDestination(target.position);
                    }
                }
            }
            else
            {
                if (hasTarget())
                {
                    agent.SetDestination(target.position);
                }
                else if (agent.isOnNavMesh)
                {
                    FreeRoam();
                }
            }
            yield return new WaitForSeconds(.2f);
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
        agentScrpt.setAlerted(false);
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += startPosition;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, roamRadius, 1);
        Vector3 finalPosition = hit.position;
        agent.destination = finalPosition;
    }

    public void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
        //checkMovement();
        /*if (manager.initialized && !on_a_Path)
        {           
            //Start a new path to the targetPosition, return the result to the OnPathComplete function
            seeker.StartPath(transform.position, target.position, OnPathComplete);
            on_a_Path = true;
        }*/
    }
}
