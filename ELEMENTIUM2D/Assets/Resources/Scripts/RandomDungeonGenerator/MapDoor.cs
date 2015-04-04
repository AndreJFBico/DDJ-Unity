using UnityEngine;
using System.Collections;

public class MapDoor : MonoBehaviour {

	public float doorWidth = 1.0f;

	public bool used = false;

	public bool goesBackward = false;

	public DungeonRoom leadsTo = null;
	public DungeonRoom belongsTo = null;
	
	public bool frontJump = false;
	public bool backJump = false;

	public int branchTries = 0;
	public int normalTries = 0;
	public int backTries = 0;

    public Transform doorNavmesh;

	void Start () 
    {

	}
	
	void Update () 
    {

	}

	public void resetDoor(){
		used = false;
		leadsTo = null;
		backJump = false;
		frontJump = false;
		goesBackward = false;
	}

	void OnDrawGizmos () 
    {
		float size = 0.5f;
		if(backJump || frontJump)
			Gizmos.color = new Color (0.0f,1.0f,0.0f,0.5f);
		else if(used && branchTries > 0){
			Gizmos.color = new Color (1.0f,1.0f,0.0f,1.0f);
			size = 0.6f;
		}
		else if(used)
			Gizmos.color = new Color (0.0f,0.0f,1.0f,0.5f);
		else if(branchTries >= RoomManager.MAX_BRANCH_TRIES || normalTries >= 1)
			Gizmos.color = new Color (0.0f,0.0f,0.0f,0.5f);
		else
			Gizmos.color = new Color (1.0f,0.0f,0.0f,0.5f);
		Gizmos.DrawCube (transform.position, new Vector3 (size, size, size));

		Gizmos.color = new Color (0.0f, 1.0f, 0.0f, 0.5f);
		Gizmos.DrawCube (transform.position + (0.3f * transform.forward), new Vector3 (0.1f, 0.1f, 0.1f));
	}

    public void activateNavmesh()
    {
        //doorNavmesh.gameObject.SetActive(true);
        Transform[] children = transform.GetComponentsInChildren<Transform>(true);
        foreach(Transform t in children)
        {
            t.gameObject.SetActive(false);
        }
		gameObject.SetActive(true);
        //Destroy(this);
    }
}
