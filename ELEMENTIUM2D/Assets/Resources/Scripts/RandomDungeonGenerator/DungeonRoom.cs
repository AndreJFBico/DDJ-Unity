using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonRoom : MonoBehaviour {

	public List<MapDoor> doors = new List<MapDoor>();

	public int nodeId = 0;

	public DungeonPart part;

	public int partId;

	public int id = 0;

	public int lastSearch = 0;

	public bool parked = false;

	public string randomGroup = "";

    private bool cleared = false;

    private MinimapComponent minimapClearedSprite;

	void Awake () {
		Transform doors_object = transform.Find("Doors");
		foreach(Transform child in doors_object){
			MapDoor door = child.GetComponent<MapDoor>();
			doors.Add(door);
			door.belongsTo = this;
		}
        minimapClearedSprite = transform.FindChild("Sprites").transform.FindChild("minimap").transform.GetComponent<MinimapComponent>();
	}

    public bool isCleared()
    {
        return cleared;
    }

    public void setCleared()
    {
        cleared = true;
        minimapClearedSprite.type = MinimapComponent.Type.CLEARED;
        minimapClearedSprite.resetSpriteRenderer();
    }
}
