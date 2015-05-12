using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Includes;

public class Pushable : BreakableProp
{
    public float pushStrength = 0.2f;
    private bool attached = false;
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
                Pushable p = hit.collider.gameObject.GetComponent<Pushable>();
                if(p)
                {
                    if (latestCollision && !attachedColls.Contains(hit.collider) && !p.attached)
                    {
                        p.setCol(latestCollision);
                        attachedColls.Add(hit.collider);
                        p.attached = true;
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
                    Pushable p = gameObject.GetComponent<Pushable>();
                    if (latestCollision && !attachedColls.Contains(col) && !p.attached)
                    {
                        p.setCol(latestCollision);
                        attachedColls.Add(col);
                        p.attached = true;
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
        List<Pushable> pushables = new List<Pushable>();
        foreach(Collider c in attachedColls)
        {
            c.GetComponent<Pushable>().setCol(null);
            pushables.Add(c.GetComponent<Pushable>());
        }
        attachedColls.Clear();
        foreach(Pushable p in pushables)
        {
            p.clearAttached();
        }
        attached = false;
    }

    void OnTriggerExit(Collider col)
    {
        if (LayerMask.NameToLayer("Player") == col.gameObject.layer)
        {
            latestCollision = null;
            clearAttached();
        }
        else if(LayerMask.NameToLayer("Breakable") == col.gameObject.layer)
        {
            if (col.GetComponent<Pushable>())
            {
                if (latestCollision)
                {
                    clearAttached();
                }
            }
        }
    }
}
