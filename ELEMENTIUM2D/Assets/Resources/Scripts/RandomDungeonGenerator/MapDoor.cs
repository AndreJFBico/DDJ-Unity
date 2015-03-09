using UnityEngine;
using System.Collections;

public class MapDoor : MonoBehaviour {

	public float doorWidth = 1.0f;

	public bool used = false;

	public bool goesBackward = false;

	public DungeonRoom leadsTo = null;
	public DungeonRoom belongsTo = null;

	public int branchTries = 0;
	public int normalTries = 0;

    public Transform doorNavmesh;

	void Start () 
    {

	}
	
	void Update () 
    {

	}

	void OnDrawGizmos () 
    {

		if(used)
			Gizmos.color = new Color (0.0f,0.0f,1.0f,0.5f);
		else if(branchTries >= RoomManager.MAX_BRANCH_TRIES || normalTries >= 1)
			Gizmos.color = new Color (0.0f,0.0f,0.0f,0.5f);
		else
			Gizmos.color = new Color (1.0f,0.0f,0.0f,0.5f);
		Gizmos.DrawCube (transform.position, new Vector3 (0.5f,0.5f,0.5f));

		Gizmos.color = new Color (0.0f, 1.0f, 0.0f, 0.5f);
		Gizmos.DrawCube (transform.position + (0.3f * transform.forward), new Vector3 (0.1f,0.1f,0.1f));
	}

    public void activateNavmesh()
    {
        //doorNavmesh.gameObject.SetActive(true);
        Transform[] children = transform.GetComponentsInChildren<Transform>(true);
        foreach(Transform t in children)
        {
            t.gameObject.SetActive(false);
        }
        //Destroy(this);
    }
}
