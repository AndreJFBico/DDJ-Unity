using UnityEngine;
using System.Collections;
using Pathfinding;

public class PathAgent : MonoBehaviour
{
    public Transform target;
    private PathManager manager;
    private Seeker seeker;
    private bool on_a_Path;

    public void Start()
    {
        on_a_Path = false;
        seeker = GetComponent<Seeker>();
        manager = GameObject.Find("Navmeshmanager").GetComponent<PathManager>();
    }

    public void Update()
    {
        if (manager.initialized && !on_a_Path)
        {           
            //Start a new path to the targetPosition, return the result to the OnPathComplete function
            seeker.StartPath(transform.position, target.position, OnPathComplete);
            on_a_Path = true;
        }
    }

    public void OnPathComplete(Path p)
    {
       on_a_Path = false;
       // Debug.Log("Yay, we got a path back. Did it have an error? " + p.error);
    }
}
