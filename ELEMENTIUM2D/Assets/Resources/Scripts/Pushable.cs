using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Includes;

public class Pushable : BreakableProp
{
    public float pushStrength = 0.2f;

    private Collider latestCollision;
    private List<Collider> attachedColls;

    // Use this for initialization
    void Start()
    {
        attachedColls = new List<Collider>();
        maxDurability = 10;
        durability = maxDurability;
    }

    bool rayCast(Vector3 position, Vector3 direction, float distance)
    {
        RaycastHit hit;
        if (Physics.Raycast(new Ray(position, direction), out hit, distance, LayerMask.GetMask(Constants.obstacles) | LayerMask.GetMask(Constants.breakable)))
        {
            Debug.DrawRay(position, direction, Color.red);
            if (!hit.collider.isTrigger && hit.transform.GetInstanceID() != transform.GetInstanceID())
            {
                if(hit.collider.gameObject.GetComponent<Pushable>())
                {
                    if(latestCollision)
                    {
                        hit.collider.gameObject.GetComponent<Pushable>().setCol(latestCollision);
                        attachedColls.Add(hit.collider);
                    }             
                }
                return true;
            }
        }
        Debug.DrawRay(position, direction, Color.white);
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if(latestCollision)
        {
            Vector3 inicialDir = transform.position - latestCollision.transform.position;
            Vector3 direction = transform.forward;
            float adjustmentValue = 0;
            if (Mathf.Abs(inicialDir.x) > Mathf.Abs(inicialDir.z))
            {
                direction = new Vector3(inicialDir.x, 0.0f, 0.0f);
            }
            else
            {
                direction = new Vector3(0.0f, 0.0f, inicialDir.z);
                adjustmentValue = 0.0f;
            }
            
            if(!rayCast(transform.position, direction, 0.3f + adjustmentValue ))
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + direction * pushStrength, Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(!col.isTrigger)
        {
            if (LayerMask.NameToLayer("Player") == col.gameObject.layer)
            {
                latestCollision = col;
            }
            else if (LayerMask.NameToLayer("Breakable") == col.gameObject.layer)
            {
                if (col.GetComponent<Pushable>())
                {
                    if (latestCollision)
                    {
                        col.GetComponent<Pushable>().setCol(latestCollision);
                        attachedColls.Add(col);
                    }
                }
            }
        }
    }

    public void setCol(Collider col)
    {
        latestCollision = col;
    }

    public void clearAttached()
    {
        foreach(Collider c in attachedColls)
        {
            c.GetComponent<Pushable>().setCol(null);
        }
        attachedColls.Clear();
    }

    void OnTriggerExit(Collider col)
    {
        if (LayerMask.NameToLayer("Player") == col.gameObject.layer)
        {
            latestCollision = null;
            foreach(Collider c in attachedColls)
            {
                c.GetComponent<Pushable>().setCol(null);
            }
            clearAttached();
        }
        else if(LayerMask.NameToLayer("Breakable") == col.gameObject.layer)
        {
            if (col.GetComponent<Pushable>())
            {
                if (latestCollision)
                {
                    foreach(Collider c in attachedColls)
                    {
                        c.GetComponent<Pushable>().setCol(null);
                        c.GetComponent<Pushable>().clearAttached();
                    }
                }
            }
        }
    }
}
