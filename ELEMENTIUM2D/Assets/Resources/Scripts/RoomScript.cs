using UnityEngine;
using System.Collections;
using Includes;

public class RoomScript : MonoBehaviour {

    public bool ALLOW_NORTH;
    public bool ALLOW_SOUTH;
    public bool ALLOW_WEST;
    public bool ALLOW_EAST;
    public float probability_percentage;

    private GameObject NORTH_DOOR;
    private GameObject SOUTH_DOOR;
    private GameObject WEST_DOOR;
    private GameObject EAST_DOOR;

    void Awake()
    {
        // Gets doors.
        if (ALLOW_NORTH)
        {
            NORTH_DOOR = transform.FindChild("NORTH").gameObject;
            NORTH_DOOR.SetActive(ALLOW_NORTH);
        }
            
        if (ALLOW_SOUTH)
        {
            SOUTH_DOOR = transform.FindChild("SOUTH").gameObject;
            SOUTH_DOOR.SetActive(ALLOW_SOUTH);
        }
            
        if (ALLOW_WEST)
        {
            WEST_DOOR = transform.FindChild("WEST").gameObject;
            WEST_DOOR.SetActive(ALLOW_WEST);
        }
            
        if (ALLOW_EAST)
        {
            EAST_DOOR = transform.FindChild("EAST").gameObject;
            EAST_DOOR.SetActive(ALLOW_EAST);
        }   
    }

	// Use this for initialization
	void Start () {

	}

    public bool isDoorActivated(Includes.Door door)
    {
        if (door == Includes.Door.NORTH)
        {
            return NORTH_DOOR.gameObject.activeSelf;
        }
        else if (door == Includes.Door.SOUTH)
        {
            return SOUTH_DOOR.gameObject.activeSelf;
        }
        else if (door == Includes.Door.WEST)
        {
            return WEST_DOOR.gameObject.activeSelf;
        }
        else if (door == Includes.Door.EAST)
        {
            return EAST_DOOR.gameObject.activeSelf;
        }
        return false;
    }

    public void enableDoor(Includes.Door door, bool enable)
    {
        if (door == Includes.Door.NORTH)
        {
            NORTH_DOOR.SetActive(enable);
            ALLOW_NORTH = !enable;
        }
        else if (door == Includes.Door.SOUTH)
        {
            SOUTH_DOOR.SetActive(enable);
            ALLOW_SOUTH = !enable;
        }
        else if (door == Includes.Door.WEST)
        {
            WEST_DOOR.SetActive(enable);
            ALLOW_WEST = !enable;
        }
        else if (door == Includes.Door.EAST)
        {
            EAST_DOOR.SetActive(enable);
            ALLOW_EAST = !enable;
        }
    }
}
