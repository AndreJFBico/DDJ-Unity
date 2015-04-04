using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System;

public class RoomManager : MonoBehaviour
{

    public static int SPHERE_RADIUS = 50;
    public static int MAX_BRANCH_TRIES = 1;
    public static int MAX_TRIES = 1;

   /* public int timeLimit = 300;

    public string mapType;
    public Dictionary<int, string> rooms = new Dictionary<int, string>();
    public Dictionary<string, List<int>> groups = new Dictionary<string, List<int>>();
    public DungeonNode dungeonHead = null;
    public DungeonRoom dungeonRoomsHead = null;
    public int minRandom = 0;
    public int maxRandom = 0;

    public ResourcesPool pool;

    public bool failed = false;
    public bool processing = true;
    public bool merged = false;

    private PathManager pathManager;

    // Use this for initialization
    void Start()
    {
        pathManager = GameObject.Find("PathManager").GetComponent<PathManager>();
        Debug.Log("started");
        pool = new ResourcesPool();
        loadMapType();
        //for (int i = 0; i < 4; i++)
        //{
            try
            {
                generateMap();
            }
            catch (Exception)
            {
                processing = false;
                failed = true;
            }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if(failed && !processing)
        {
            timeLimit += 100;
            cleanMap();
            try
            {
                generateMap();  
            }
            catch(Exception)
            {
                processing = false;
                return;
            }
        } else if (!merged)
        {
            pathManager.genGraph();
            merged = true;
        }
    }

    private void loadMapType()
    {

        string line;
        StreamReader theReader = new StreamReader(Application.dataPath + "/Maps/" + mapType, Encoding.Default);

        //parse rooms
        line = theReader.ReadLine();
        while (true)
        {
            line = theReader.ReadLine();
            if (line == "::")
                break;
            string[] content = line.Split(' ');
            rooms.Add(int.Parse(content[0]), content[1]);
        }
        //dumpDictionary(rooms);

        //parse groups
        line = theReader.ReadLine();
        while (true)
        {
            line = theReader.ReadLine();
            if (line == "::")
                break;
            string[] content = line.Split(' ');
            List<int> list = new List<int>();
            for (int i = content.Length; i > 1; i--)
            {
                list.Add(int.Parse(content[i - 1]));
            }
            groups.Add(content[0], list);
        }
        //dumpDictionary(groups);

        //parse dungeon
        Stack<DungeonNode> stack = new Stack<DungeonNode>();
        line = theReader.ReadLine();

        line = theReader.ReadLine();
        string[] random = line.Split(' ');
        minRandom = int.Parse(random[1]);
        maxRandom = int.Parse(random[2]);

        DungeonNode lastNode = null;
        int idcounter = 0;
        while (true)
        {
            line = theReader.ReadLine();
            line = line.Replace("\t", "");
            if (line == "::")
                break;
            if (line == "alt")
            {
                stack.Push(lastNode);
                continue;
            }
            if (line == "altend")
            {
                lastNode = stack.Pop();
                continue;
            }

            string[] content = line.Split(' ');
            DungeonNode currentNode = new DungeonNode(content[0], lastNode, ++idcounter);
            if (lastNode != null)
                lastNode.addChild(currentNode);

            if (content[0] == "S")
            {
                currentNode.room = int.Parse(content[1]);
                currentNode.randomgroup = content[2];
            }
            else if (content[0] == "R")
            {
                currentNode.group = content[1];
                currentNode.min = int.Parse(content[2]);
                currentNode.max = int.Parse(content[3]);
                currentNode.randomgroup = content[4];
            }

            if (dungeonHead == null)
            {
                dungeonHead = currentNode;
            }
            lastNode = currentNode;
        }
        //dumpDungeonNodes();

        theReader.Close();
        return;
    }

    System.Diagnostics.Stopwatch watch;


    private void gen()
    {
        generateMap();
    }

    private void generateMap()
    {
        processing = true;
        watch = System.Diagnostics.Stopwatch.StartNew();
        //for (int i = 0; i <= 3;i++ )
        //{

        bool result = generateNode(dungeonHead, null);

            //Debug.Break();
       // }
        generateBranches();
        watch.Stop();
        long elapsedMs = watch.ElapsedMilliseconds;
        Debug.Log("map generated in " + elapsedMs + "ms");
        failed = !result;
        processing = false;
    }

    private void cleanMap()
    {
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
        dungeonRoomsHead = null;
        watch = System.Diagnostics.Stopwatch.StartNew();
        watch.Reset();
        watch.Start();
        //Debug.Log("Tempo inicial" + " " + watch.ElapsedMilliseconds);
        ///watch = System.Diagnostics.Stopwatch.StartNew();
        
    }

    private bool generateNode(DungeonNode node, DungeonRoom lastRoom) 
    {
        if (watch.ElapsedMilliseconds > timeLimit)
        {
            Debug.Log("demorou muito tempo" + watch.ElapsedMilliseconds + "ms");
            throw new Exception("TEST");
        }
        if (node.type == DungeonNode.DungeonNodeType.Random)
        {
            for (int i = 0; i < MAX_TRIES; i++)
            {
                bool result = generateRandomNode(node, lastRoom);
                if (result)
                    return true;
            }
            return false;
        }
        else if (node.type == DungeonNode.DungeonNodeType.Static)
        {
            for (int i = 0; i < MAX_TRIES; i++)
            {
                int result = generateStaticNode(node, lastRoom);
                if (result == 1)
                    //return false;
                    continue;
                else if (result == 2)
                    continue;
                else if (result == 0)
                    return true;
            }
            return false;
        }
        return true;
    }

    private bool generateRandomNode(DungeonNode node, DungeonRoom lastRoom)
    {
        if (watch.ElapsedMilliseconds > timeLimit)
        {
            Debug.Log("demorou muito tempo" + watch.ElapsedMilliseconds + "ms");
            throw new Exception("TEST");
        }
        List<int> group = groups[node.group];
        List<DungeonNode> targets = node.children;
        if (targets.Count == 0)
            targets.Add(null);
        foreach (DungeonNode child in targets)
        {
            bool sucessfullPath = false;
            for (int i = 0; i < MAX_TRIES; i++)
            {
                int depth = RandomGenerator.Next(node.max - node.min) + node.min;
                int response = generateRandomRoom(lastRoom, child, group, depth, node);
                if (response == 0)
                {
                    sucessfullPath = true;
                    break;
                }
            }
            if (!sucessfullPath)
            {
                cleanAllChildrenRooms(lastRoom, node.id);
                return false;
            }
        }
        return true;
    }

    private int generateRandomRoom(DungeonRoom lastRoom, DungeonNode child, List<int> group, int depth, DungeonNode currentNode)
    {
        if (watch.ElapsedMilliseconds > timeLimit)
        {
            Debug.Log("demorou muito tempo" + watch.ElapsedMilliseconds + "ms");
            throw new Exception("TEST");
        }
        if (depth == 0)
        {
            if (child == null)
                return 0;
            if (!generateNode(child, lastRoom))
                return 1;
            else
                return 0;
        }

        for (int i = 0; i < MAX_TRIES; i++)
        {
            GameObject go = Instantiate(pool.getObject(rooms[group[RandomGenerator.Next(group.Count)]])) as GameObject;
            go.transform.parent = transform;
            DungeonRoom room = go.GetComponent<DungeonRoom>();

            List<MapDoor> prevdoors = lastRoom.doors;
            prevdoors.Shuffle();
            foreach (MapDoor door in prevdoors)
            {
                if (!door.goesBackward)
                {
                    if (!door.used)
                    {
                        if (positionRandomRoom(room, lastRoom, door, currentNode))
                        {
                            int r = generateRandomRoom(room, child, group, depth - 1, currentNode);
                            if (r == 1)
                            {
                                door.used = false;
                                continue;
                            }
                            return 0;
                        }
                        else
                            door.used = false;
                    }
                    else if (door.leadsTo.nodeId == currentNode.id)
                    {
                        int r = generateRandomRoom(door.leadsTo, child, group, depth - 1, currentNode);
                        if (r == 1) //if a room bellow failed, we continue trying
                            continue;
                        Destroy(go);
                        return 0;
                    }
                }
            }
            Destroy(go);
        }
        return 1;
    }

    private bool positionRandomRoom(DungeonRoom room, DungeonRoom lastRoom, MapDoor door, DungeonNode currentNode)
    {

        List<MapDoor> newdoors = room.doors;
        newdoors.Shuffle();

        //its a new room, so it cant be going anywhere yet.
        //used to fix a bug
        foreach (MapDoor newdoor in newdoors)
        {
            newdoor.used = false;
            newdoor.goesBackward = false;
            newdoor.leadsTo = null;
        }
        foreach (MapDoor newdoor in newdoors)
        {
            if (door.doorWidth == newdoor.doorWidth)
            {
                door.normalTries++;
                if (positionRoom(door, newdoor, room, lastRoom, currentNode))
                {
                    return true;
                }
            }
        }
        return false;
    }

    //returns
    //0 - ok
    //1 - room cannot be positioned
    //2 - child cannot be positioned
    private int generateStaticNode(DungeonNode node, DungeonRoom lastRoom)
    {
        GameObject go = Instantiate(pool.getObject(rooms[node.room])) as GameObject;
        go.transform.parent = transform;
        DungeonRoom room = go.GetComponent<DungeonRoom>();
        if (dungeonRoomsHead == null)
            dungeonRoomsHead = room;
        if (lastRoom != null)
        {
            if (!positionStaticRoom(room, lastRoom, node))
            {
                Destroy(go);
                return 1;
            }
        }
        else
        {
            room.randomGroup = node.randomgroup;
            room.nodeId = node.id;
        }
        foreach (DungeonNode child in node.children)
        {
            if (!generateNode(child, room))
            {
                //children failed but room was positioned, we have to remove the references to it
                if (lastRoom != null)
                {
                    foreach (MapDoor door in lastRoom.doors)
                    {
                        if (door.used && door.leadsTo == room)
                        {
                            door.used = false;
                            door.leadsTo = null;
                            break;
                        }
                    }
                }
                Destroy(go);
                return 2;
            }
        }
        return 0;
    }

    private bool positionStaticRoom(DungeonRoom room, DungeonRoom lastRoom, DungeonNode currentNode)
    {
        List<MapDoor> prevdoors = lastRoom.doors;
        prevdoors.Shuffle();
        foreach (MapDoor door in prevdoors)
        {
            if (!door.used)
            {
                List<MapDoor> newdoors = room.doors;
                newdoors.Shuffle();
                foreach (MapDoor newdoor in newdoors)
                {
                    if (!newdoor.used && (door.doorWidth == newdoor.doorWidth))
                    {
                        door.normalTries++;
                        if (positionRoom(door, newdoor, room, lastRoom, currentNode))
                            return true;
                    }
                }
            }
        }
        return false;
    }

    //returns if these two rooms can be connected by these doors without collisions
    private bool positionRoom(MapDoor door, MapDoor newdoor, DungeonRoom room, DungeonRoom lastRoom, DungeonNode currentNode)
    {

        //position the piece correctly
        float angle = 180.0f - Vector3.Angle(door.transform.forward, newdoor.transform.forward);
        room.transform.RotateAround(room.transform.position, Vector3.up, angle);

        //if that wasnt correct rotate the other way... TODO: change to something cleaner
        float newangle = Vector3.Angle(door.transform.forward, newdoor.transform.forward);
        if (newangle != 180)
            room.transform.RotateAround(room.transform.position, Vector3.up, -2 * angle);

        room.transform.position = door.transform.position + room.transform.position - newdoor.transform.position;

        //check for collisions
        bool failcollision = false;
        Transform colliders_object = room.transform.Find("Colliders");
        foreach (Transform child in colliders_object)
        {
            BoxCollider collider = child.GetComponent<BoxCollider>();
            Bounds bounds = collider.bounds;
            int layerMask = 0 | (1 << LayerMask.NameToLayer("RoomColliders"));
            Collider[] otherColliders = Physics.OverlapSphere(child.position, SPHERE_RADIUS, layerMask);
            foreach (Collider c in otherColliders)
            {
                if (c.transform.GetInstanceID() == collider.transform.GetInstanceID())
                {
                    continue;
                }
                else if (bounds.Intersects(c.bounds))
                {
                    //Debug.Log("detected colission");
                    failcollision = true;
                    break;
                }
            }
            //if one of the boxes collided with another room, no point cheking the other boxes
            if (failcollision)
                break;
        }

        //the room collides, so lets try another one
        if (failcollision)
        {
            return false;
        }
        else
        {
            //doesnt collide on the correct position, so it can be used
            door.used = true;
            door.activateNavmesh();
            door.leadsTo = room;
            if (currentNode == null)
            {
                room.nodeId = -1;
                room.randomGroup = lastRoom.randomGroup;
            }
            else
            {
                room.nodeId = currentNode.id;
                room.randomGroup = currentNode.randomgroup;
            }
            newdoor.used = true;
            newdoor.activateNavmesh();
            newdoor.leadsTo = lastRoom;
            newdoor.goesBackward = true;
            return true;
        }
    }

    private void cleanAllChildrenRooms(DungeonRoom room, int nodeId)
    {
        //Debug.Log("cleaning rooms");
        foreach (MapDoor door in room.doors)
        {
            if (!door.goesBackward && door.leadsTo != null && door.leadsTo.nodeId == nodeId)
            {
                if (door.used)
                {
                    cleanAllChildRoomsRecur(door.leadsTo, nodeId);
                }
                door.used = false;
                door.leadsTo = null;
            }
        }
        return;
    }

    private void cleanAllChildRoomsRecur(DungeonRoom room, int nodeId)
    {
        foreach (MapDoor door in room.doors)
        {
            if (!door.goesBackward && door.leadsTo != null && door.leadsTo.nodeId == nodeId)
            {
                cleanAllChildRoomsRecur(door.leadsTo, nodeId);
            }
        }
        Destroy(room.gameObject);
        return;
    }

    public HashSet<MapDoor> emptydoors = new HashSet<MapDoor>();

    private void generateBranches()
    {
        exploreRoomForDoors(dungeonRoomsHead);
        int quantity = RandomGenerator.Next(maxRandom - minRandom) + minRandom;
        for (; quantity > 0; quantity--)
        {
            bool roomDone = false;
            while (emptydoors.Count != 0)
            {
                //int randomDoorIndex = RandomGenerator.Next(emptydoors.Count);
                MapDoor door = emptydoors.ElementAt(RandomGenerator.Next(emptydoors.Count));
                if (door.branchTries >= MAX_BRANCH_TRIES)
                {
                    emptydoors.Remove(door);
                    continue;
                }
                door.branchTries++;

                List<int> group = groups[door.belongsTo.randomGroup];
                GameObject go = Instantiate(pool.getObject(rooms[group[RandomGenerator.Next(group.Count)]])) as GameObject;
                go.transform.parent = transform;
                DungeonRoom room = go.GetComponent<DungeonRoom>();

                List<MapDoor> newdoors = room.doors;
                newdoors.Shuffle();
                foreach (MapDoor newdoor in newdoors)
                {
                    if (!newdoor.used && (door.doorWidth == newdoor.doorWidth))
                    {
                        if (positionRoom(door, newdoor, room, door.belongsTo, null))
                        {
                            emptydoors.Remove(door);
                            foreach (MapDoor idoor in room.doors)
                            {
                                if (!idoor.used)
                                {
                                    emptydoors.Add(idoor);
                                }
                            }
                            roomDone = true;
                            break;
                        }
                    }
                }
                if (!roomDone)
                    Destroy(go);
                else
                    break;
            }
        }
    }

    private void exploreRoomForDoors(DungeonRoom exploringRoom)
    {
        foreach (MapDoor door in exploringRoom.doors)
        {
            if (!door.used)
            {
                //if (door.normalTries < 3) {
                    if (exploringRoom.randomGroup != "0")
                        emptydoors.Add(door);
                //}
            }
            else if (!door.goesBackward)
            {
                exploreRoomForDoors(door.leadsTo);
            }
        }
    }

    private void dumpDungeonNodes()
    {
        dumpNode(dungeonHead, 0);
    }

    private void dumpNode(DungeonNode node, int depth)
    {
        foreach (DungeonNode n in node.children)
        {
            string text = depth + " ";
            if (n.type == DungeonNode.DungeonNodeType.Random)
            {
                text += "R ";
                text += n.group + " " + n.min + " " + n.max;
            }
            else if (n.type == DungeonNode.DungeonNodeType.Static)
            {
                text += "S ";
                text += n.room;
            }
            Debug.Log(text);
            dumpNode(n, depth + 1);
        }
    }

    private static void dumpDictionary(Dictionary<int, string> dict)
    {
        foreach (KeyValuePair<int, string> kvp in dict)
        {
            Debug.Log(kvp.Key + " " + kvp.Value);
        }
    }

    private static void dumpDictionary(Dictionary<string, List<int>> dict)
    {
        foreach (KeyValuePair<string, List<int>> kvp in dict)
        {
            string result = kvp.Key;
            List<int> list = kvp.Value;
            foreach (int i in list)
            {
                result += i;
            }
            Debug.Log(result);
        }
    }*/
}
