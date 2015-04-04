using UnityEngine;
using System.Collections;

public class RoomCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.GetComponentInParent<Player>() != null){
			RoomManagerV2 manager = transform.parent.parent.parent.GetComponent<RoomManagerV2>();
			if(manager.generationDone){
				Debug.Log("player entered room");
				manager.toggleRooms(transform.parent.parent.GetComponent<DungeonRoom>(), false, false);
			}
		}
	}
}
