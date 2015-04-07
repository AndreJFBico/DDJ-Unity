using UnityEngine;
using System.Collections;

public class RoomCollider : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.GetComponentInParent<Player>() != null){
			RoomManagerV2 manager = transform.parent.parent.parent.GetComponent<RoomManagerV2>();
			if(manager.generationDone){
				manager.toggleRooms(transform.parent.parent.GetComponent<DungeonRoom>(), false, false);
			}
		}
        else if (other.gameObject.GetComponentInParent<EnemyScript>() != null)
        {
            other.transform.parent.parent = transform.parent.parent.FindChild("Other");
        }
	}
}
