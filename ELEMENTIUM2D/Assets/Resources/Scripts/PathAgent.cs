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

    private NavMeshAgent agent;
    private Agent agentScrpt;
    private Transform previousTarget;
    private bool imobilized = false;
    private int numFreeRoam = 0;

    private float previousSpeed;
    private float unalertedSpeed;
    private float alertedSpeed;

    public void Awake() 
    {
        target = null;
        agent = transform.parent.GetComponent<NavMeshAgent>();
        roamRadius = Constants.enemyRoamRadius;
        agentScrpt = transform.parent.gameObject.GetComponent<Agent>();
        previousSpeed = agent.speed;   
    } 

    public float UnalertedSpeed
    {
        get
        {
            return unalertedSpeed;
        }
        set
        {
            unalertedSpeed = value;
        }
    }

    public float AlertedSpeed
    {
        get
        {
            return alertedSpeed;
        }
        set
        {
            alertedSpeed = value;
        }
    }

    public void playerSighted(Transform player)
    {
        agentScrpt.setAlerted(true);
        target = player;
        if (imobilized)
            agent.speed = 0;
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.CompareTo("Player") == 0 && target == null)
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
        previousTarget = target;
        target = null;
        imobilized = true;
    }

    public void setAlerted(bool val)
    {
        if (val)
        {
            agent.speed = alertedSpeed;
            target = GameManager.Instance.Player.GetComponent<Player>().transform;
            agent.SetDestination(target.position);
        }
        else agent.speed = unalertedSpeed;
    }

    public void restart(bool chase)
    {
        imobilized = false;
        agent.speed = previousSpeed;
        if(chase)
        {
            if(previousTarget != null)
            {
                target = previousTarget;
            }
        }
    }

    public void instantStopChase()
    {
        Invoke("stopChase", 0);
    }

    public void stopChasing()
    {
        Invoke("stopChase", 1);
    }

    private void stopChase()
    {
        if(target != null)
        {
            float distance = (target.position - transform.parent.position).magnitude;
            if (distance < GetComponent<CapsuleCollider>().bounds.max.x - GetComponent<CapsuleCollider>().bounds.center.x)
            {
                Invoke("stopChase", 1);
                return;
            }
            target = null;
        }
    }

    public void setStoppingDistance(float val)
    {
        agent.stoppingDistance = val;
    }

    public void resetStoppingDistance()
    {
        agent.stoppingDistance = 0.05f;
    }

    public bool hasTarget()
    {
        return target != null;
    }

    void OnDisable()
    {
        StopCoroutine("checkMovement");
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
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += startPosition;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, roamRadius, 1 << NavMesh.GetNavMeshLayerFromName("Walkable"));
        Vector3 finalPosition = hit.position;
        //agent.destination = finalPosition;

        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(finalPosition, path);
        float distance = 0;
        Vector3 comparingNode = agent.transform.position;
        numFreeRoam++;
        foreach (Vector3 v in path.corners)
        {
            distance += (v - comparingNode).magnitude; 
        }
        if (distance > roamRadius * 2.0f && numFreeRoam <= 5)
        {
            FreeRoam();
        }
        numFreeRoam = 0;
        agent.SetPath(path);
        agentScrpt.setAlerted(false);
    }

    public void slowSelf(float intensity)
    {
        previousSpeed = agent.speed;
        agent.speed *= intensity;
    }

    public void restoreMoveSpeed(float intensity)
    {
        agent.speed = previousSpeed;
    }

    public void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
    }
}
