using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonRoom : MonoBehaviour {

	public List<MapDoor> doors = new List<MapDoor>();

	public int nodeId = 0;

	public string randomGroup = "";

	void Awake () {
		Transform doors_object = transform.Find("Doors");
		foreach(Transform child in doors_object){
			MapDoor door = child.GetComponent<MapDoor>();
			doors.Add(door);
			door.belongsTo = this;
		}
	}

	void Update () {
	
	}
}
