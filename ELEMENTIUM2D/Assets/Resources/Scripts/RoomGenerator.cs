using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Includes;

public class RoomGenerator : MonoBehaviour {

    // NOTES:
    //  the startRoom always have 4 possible connections.
    //  the startRoom defines the maximum Room size, all other rooms must be minor or equal the startRoom size.
    public GameObject startRoom;
    public List<GameObject> rooms = new List<GameObject>();
    public int toInstanciate = 10;
    public int deadEndPercentage = 20;

    private List<RoomNode> instanced = new List<RoomNode>();
	// Use this for initialization
    void OnEnable() 
    {
        // Creates the first room.
        GameObject roomToCheck = Instantiate(startRoom.gameObject, transform.position, transform.rotation) as GameObject;

        

        Vector3 currentPos = transform.position;
        List<RoomNode> newRooms = new List<RoomNode>();
        RoomNode currentNode = new RoomNode(currentPos, roomToCheck.gameObject);
        instanced.Add(currentNode);
        for (int i = 0; i < toInstanciate; i++ )
        {
            List<RoomNode> createdList = instantiateAdjacentRooms(currentNode);
            if(createdList.Count > 0)
            {
                newRooms.AddRange(createdList);// = mergeWithoutDuplicates(createdList, newRooms);
                instanced.AddRange(createdList); //= mergeWithoutDuplicates(instanced, createdList);
            }
            if (newRooms.Count > 0)
            {
                currentNode = newRooms[0];
                newRooms.RemoveAt(0);
            }
        }
        Debug.Log(newRooms.Count); 

        // Takes the last currentNode and searchs the tree created to fix doors.
        // Reusing this list.
        /*newRooms.Clear();
        newRooms.Add(instanced[0]);

        while (newRooms.Count > 0)
        {
            newRooms = processRoomNode(newRooms);
        }*/
	}

    //  Untested!!
    List<RoomNode> processRoomNode(List<RoomNode> list)
    {
        Debug.Log(list.Count);

        RoomNode node = list[0];

        if(!node.isRoomAllowed(Door.NORTH))
        {
            node.room.GetComponent<RoomScript>().enableDoor(Door.NORTH, false);
            node.northRoom.room.GetComponent<RoomScript>().enableDoor(Door.SOUTH, false);
            list.Add(node.northRoom);
        }
        else
        {
            node.room.GetComponent<RoomScript>().enableDoor(Door.NORTH, true);
            node.northRoom.room.GetComponent<RoomScript>().enableDoor(Door.SOUTH, true);
        }

        if (!node.isRoomAllowed(Door.SOUTH))
        {
            node.room.GetComponent<RoomScript>().enableDoor(Door.SOUTH, false);
            node.northRoom.room.GetComponent<RoomScript>().enableDoor(Door.NORTH, false);
            list.Add(node.southRoom);
        }
        else
        {
            node.room.GetComponent<RoomScript>().enableDoor(Door.SOUTH, true);
            node.northRoom.room.GetComponent<RoomScript>().enableDoor(Door.NORTH, true);
        }

        if (!node.isRoomAllowed(Door.WEST))
        {
            node.room.GetComponent<RoomScript>().enableDoor(Door.WEST, false);
            node.northRoom.room.GetComponent<RoomScript>().enableDoor(Door.EAST, false);
            list.Add(node.westRoom);
        }
        else
        {
            node.room.GetComponent<RoomScript>().enableDoor(Door.WEST, true);
            node.northRoom.room.GetComponent<RoomScript>().enableDoor(Door.EAST, true);
        }

        if (!node.isRoomAllowed(Door.EAST))
        {
            node.room.GetComponent<RoomScript>().enableDoor(Door.EAST, false);
            node.northRoom.room.GetComponent<RoomScript>().enableDoor(Door.WEST, false);
            list.Add(node.eastRoom);
        }
        else
        {
            node.room.GetComponent<RoomScript>().enableDoor(Door.EAST, true);
            node.northRoom.room.GetComponent<RoomScript>().enableDoor(Door.WEST, true);
        }

        list.Remove(node);
        return list;
    }

    // Calculates the bounds of the startRoom.
    Bounds calcBounds(Vector3 position, Transform room)
    {
        Bounds combinedBounds = new Bounds(position, new Vector3(0.0f, 0.0f, 0.0f));
        var colliders = room.GetComponentsInChildren<BoxCollider>();
        foreach (Transform child in room) 
        {
            BoxCollider collider = child.GetComponent<BoxCollider>();
            //if (combinedBounds.extents == Vector3.zero)
           //     combinedBounds = collider.bounds;
            combinedBounds.Encapsulate(collider.bounds);
            //Debug.Log("ENCAPSULATING");
        }
        return combinedBounds;
    }

    //  No longer Used!!
    List<RoomNode> mergeWithoutDuplicates(List<RoomNode> first, List<RoomNode> second)
    {
        //Debug.Log("D:");
        List<RoomNode> newList = new List<RoomNode>();
        foreach (RoomNode obj1 in first)
        {
            newList.Add(obj1);
            foreach (RoomNode obj2 in second)
            {
                //Debug.Log("WTF" + obj1.position.x + "  " + obj2.position.x);
                if (compareTwoPositions(obj1.position, obj2.position))
                {
                    break;
                }
                newList.Add(obj2);
            }
        }
        return first;
    }

    bool compareTwoPositions(Vector3 pos0, Vector3 pos1)
    {
        float error = 0.002f;
        if(Mathf.Abs(pos0.x - pos1.x) <= error)
        {
            if(Mathf.Abs(pos0.y - pos1.y) <= error)
            {
                if(Mathf.Abs(pos0.z - pos1.z) <= error)
                {
                    return true;
                }
            }
        }
        return false;
    }

    bool roomPositionOcupied(Vector3 position)
    {
        foreach (RoomNode obj in instanced)
        {
            if (compareTwoPositions(obj.position, position))
                return true;
        }
        return false;
    }

    List<RoomNode> instantiateAdjacentRooms(RoomNode currentNode)
    {
        Vector3 position = currentNode.position;
        Transform room = currentNode.room.transform;

        // Calculates the bounds of the first room.
        Bounds bounds = calcBounds(position, room);

        float size_x = bounds.max.x - bounds.min.x;
        float size_z = bounds.max.z - bounds.min.z;
        //Debug.Log(bounds.max.x);
        //Debug.Log(bounds.min.x);

        RoomScript room_script = room.GetComponent<RoomScript>();

        List<RoomNode> createdRooms = new List<RoomNode>();

        Vector3 pos0 = new Vector3(position.x, position.y, position.z + size_z);
        if (!roomPositionOcupied(pos0))
        {
            if (room_script.ALLOW_NORTH && Random.Range(0, 99) < deadEndPercentage)
            {
                int r = Random.Range(0, rooms.Count);
                Debug.Log(rooms.Count - 1);
                GameObject new_room = rooms[r];
                if (!new_room.GetComponent<RoomScript>().ALLOW_SOUTH)
                    new_room = rooms[1];

                var obj = Instantiate(new_room, pos0, new_room.transform.rotation) as GameObject;
                RoomNode n = new RoomNode(pos0, obj);
                currentNode.northRoom = n;
                n.southRoom = currentNode;
                createdRooms.Add(n);
                toInstanciate--;
                room_script.enableDoor(Includes.Door.NORTH, false);
                obj.GetComponent<RoomScript>().enableDoor(Includes.Door.SOUTH, false);
            }
            else
            {
                //room_script.enableDoor(Includes.Door.NORTH, true);
            }
        }

        Vector3 pos1 = new Vector3(position.x, position.y, position.z - size_z);
        if (!roomPositionOcupied(pos1))
        {
            if (room_script.ALLOW_SOUTH && Random.Range(0, 99) < deadEndPercentage)
            {
                int r = Random.Range(0, rooms.Count);
                //Debug.Log(r);
                GameObject new_room = rooms[r];
                if (!new_room.GetComponent<RoomScript>().ALLOW_NORTH)
                    new_room = rooms[1];

                var obj = Instantiate(new_room, pos1, new_room.transform.rotation) as GameObject;
                RoomNode n = new RoomNode(pos1, obj);
                createdRooms.Add(n);
                currentNode.southRoom = n;
                n.northRoom = currentNode;
                toInstanciate--;
                room_script.enableDoor(Includes.Door.SOUTH, false);
                obj.GetComponent<RoomScript>().enableDoor(Includes.Door.NORTH, false);
            }
            else
            {
                //room_script.enableDoor(Includes.Door.SOUTH, true);
            }
        }

        Vector3 pos2 = new Vector3(position.x - size_x, position.y, position.z);
        if (!roomPositionOcupied(pos2))
        {
            if (room_script.ALLOW_WEST && Random.Range(0, 99) < deadEndPercentage)
            {
                int r = Random.Range(0, rooms.Count);
                //Debug.Log(r);
                GameObject new_room = rooms[r];
                if (!new_room.GetComponent<RoomScript>().ALLOW_EAST)
                    new_room = rooms[1];

                var obj = Instantiate(new_room, pos2, new_room.transform.rotation) as GameObject;
                RoomNode n = new RoomNode(pos2, obj);
                createdRooms.Add(n);
                currentNode.westRoom = n;
                n.eastRoom = currentNode;
                toInstanciate--;
                room_script.enableDoor(Includes.Door.WEST, false);
                obj.GetComponent<RoomScript>().enableDoor(Includes.Door.EAST, false);
            }
            else
            {
                //room_script.enableDoor(Includes.Door.WEST, true);
            }
        }

        Vector3 pos3 = new Vector3(position.x + size_x, position.y, position.z);
        if (!roomPositionOcupied(pos3))
        {
            if (room_script.ALLOW_EAST && Random.Range(0, 99) < deadEndPercentage)
            {
                int r = Random.Range(0, rooms.Count);
                //Debug.Log(r);
                GameObject new_room = rooms[r];
                if (!new_room.GetComponent<RoomScript>().ALLOW_WEST)
                    new_room = rooms[1];

                var obj = Instantiate(new_room, pos3, new_room.transform.rotation) as GameObject;
                RoomNode n = new RoomNode(pos3, obj);
                createdRooms.Add(n);
                currentNode.eastRoom = n;
                n.westRoom = currentNode;
                toInstanciate--;
                room_script.enableDoor(Includes.Door.EAST, false);
                obj.GetComponent<RoomScript>().enableDoor(Includes.Door.WEST, false);
            }
            else
            {
                //room_script.enableDoor(Includes.Door.EAST, true);
            }
        }
        //Debug.Log(createdRooms.Count);
        return createdRooms;
    }

}
