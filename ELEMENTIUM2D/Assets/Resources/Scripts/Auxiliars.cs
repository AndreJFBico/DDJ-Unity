using UnityEngine;
using System.Collections;

namespace Includes
{
    public enum Elements { NEUTRAL, FIRE, EARTH, FROST };
    public enum Door { NORTH, SOUTH, WEST, EAST };

    public class RoomNode
    {
        public Vector3 position;
        public GameObject room;

        // If room is not allowed then there is not RoomNode associated, therefore it is equal to null.
        public RoomNode northRoom;
        public RoomNode southRoom;
        public RoomNode westRoom;
        public RoomNode eastRoom;

        public RoomNode(Vector3 p, GameObject r)
        {
            position = p;
            room = r;
            northRoom = null;
            southRoom = null;
            westRoom = null;
            eastRoom = null;
        }

        public bool isRoomAllowed(Door door)
        {
            if (door == Door.NORTH)
            {
                return room.GetComponent<RoomScript>().ALLOW_NORTH;
            }
            else if (door == Door.SOUTH)
            {
                return room.GetComponent<RoomScript>().ALLOW_SOUTH;
            }
            else if (door == Door.EAST)
            {
                return room.GetComponent<RoomScript>().ALLOW_EAST;
            }
            else if (door == Door.WEST)
            {
                return room.GetComponent<RoomScript>().ALLOW_WEST;
            }
            return false;
        }
    }
}
