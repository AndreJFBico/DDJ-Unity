using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System;

public class RoomManagerV2 : MonoBehaviour {

	public static int SPHERE_RADIUS = 5000;
	public static int MAX_BRANCH_TRIES = 3;
	public static int MAX_TRIES = 2;

	public string mapType;
	System.Diagnostics.Stopwatch watch;

	public Dictionary<int, string> rooms = new Dictionary<int, string>();
	public Dictionary<string, string> doors = new Dictionary<string, string>();
	public Dictionary<string, List<int>> groups = new Dictionary<string, List<int>>();
	public Dictionary<int, DungeonPart> parts = new Dictionary<int, DungeonPart>();
	public Dictionary<string, string> doorsUsage = new Dictionary<string, string>();
	public ResourcesPool pool;
	public DungeonRoom dungeonRoomsHead = null;
	public DungeonPart firstPart = null;
	public int searchId = 1;
	public int roomId = 0;
	public int minRandom = 0;
	public int maxRandom = 0;

	public int timeLimit = 5000;

	private Vector3 cemiteryPosition = new Vector3(10000, 1000, 1000);
	public int state = 0;

	public bool generationDone = false;

	void Start () {
		pool = new ResourcesPool();
		loadMapType();
	}
		
	void Update () {
		if(state == 0){
			if(generateMap ())
				state = 1;
		}
		else if(state == 1){
			posGeneration();
			state = 2;
		}
	}

	private void loadMapType()
	{
        TextAsset textAsset = Resources.Load("Map/Maps/type3") as TextAsset;
        //File.Create("Map.txt");

        //StreamWriter file2 = new StreamWriter("/Map.txt", true);
        //file2.WriteLine(textAsset.text);
        //file2.Close();

        //string line;
        //StreamReader theReader = new StreamReader("/Map.txt", Encoding.Default);

        byte[] byteArray = Encoding.UTF8.GetBytes(textAsset.text);
        MemoryStream stream = new MemoryStream(byteArray);

        StreamReader theReader = new StreamReader(stream);
        string line;

        //string line;
        //StreamReader theReader = new StreamReader(Application.dataPath + "/Resources/Map/Maps/" + mapType, Encoding.Default);
		

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

		//parse doors
		line = theReader.ReadLine();
		while (true)
		{
			line = theReader.ReadLine();
			if (line == "::")
				break;
			string[] content = line.Split(' ');
			doors.Add(content[0], content[1]);
		}
		
		//parse dungeon
		line = theReader.ReadLine(); //::dungeon

		line = theReader.ReadLine(); //random
		string[] random = line.Split(' ');
		minRandom = int.Parse(random[1]);
		maxRandom = int.Parse(random[2]);
		line = theReader.ReadLine(); //blank

		Dictionary<int, List<KeyValuePair<int,KeyValuePair<bool, string>>>> gotos = new Dictionary<int, List<KeyValuePair<int, KeyValuePair<bool, string>>>>();
		
		int idcounter = 0;
		while (true){
			line = theReader.ReadLine();
			line = line.Replace("\t", "");
			if (line == "::")
				break;
			if (line == "")
				continue;

			string[] part = line.Split(' ');
			int partId = int.Parse(part[1]);
			DungeonPart currentPart = new DungeonPart(partId);
			gotos[partId] = new List<KeyValuePair<int,KeyValuePair<bool, string>>>();
			if(firstPart == null)
				firstPart = currentPart;
			while(true){
				line = theReader.ReadLine();
				string[] content = line.Split(' ');

				if(content[0] == ":goto")
					break;

				DungeonNode currentNode = new DungeonNode(content[0], ++idcounter);

				if (content[0] == "S"){
					currentNode.room = int.Parse(content[1]);
					currentNode.randomgroup = content[2];
				}
				else if (content[0] == "R"){

					currentNode.group = content[1];
					currentNode.min = int.Parse(content[2]);
					currentNode.max = int.Parse(content[3]);
					currentNode.randomgroup = content[4];
				}
				currentPart.addChild(currentNode);
			}
			string[] gotoline = line.Split(' ');
			for(int i = 1; i < gotoline.Length; i++){
				String[] target = gotoline[i].Split('|');
				int key = int.Parse(target[0]);
				bool value = false;
				if(target[1] == "Y")
					value = true;
				string d = "";
				if(target.Count() > 2)
					d = target[2];
				gotos[partId].Add(new KeyValuePair<int, KeyValuePair<bool, string>>(key, new KeyValuePair<bool, string>(value, d)));
			}
			parts[partId] = currentPart;
		}
		foreach(KeyValuePair<int, List<KeyValuePair<int, KeyValuePair<bool, string>>>> pair in gotos){
			foreach(KeyValuePair<int, KeyValuePair<bool, string>> kvp in pair.Value){
				int i = kvp.Key;
				bool type = kvp.Value.Key;
				string door = kvp.Value.Value;
				doorsUsage[pair.Key + "+" + i] = door;
				parts[pair.Key].addAdjacentEnd(parts[i], type);
				parts[i].addAdjacentStart(parts[pair.Key]);
			}
		}
		dumpParts();

		theReader.Close();
		Debug.Log(doorsUsage);
		return;
	}
	
	private bool generateMap(){
		watch = System.Diagnostics.Stopwatch.StartNew();
		bool result;

		result = generatePart (firstPart, null);
		
		parkAllRooms(dungeonRoomsHead);
		watch.Stop();
		long elapsedMs = watch.ElapsedMilliseconds;
		Debug.Log("map generated in " + elapsedMs + "ms");
		generationDone = true;
		return true;
	}


	private void posGeneration(){
		watch = System.Diagnostics.Stopwatch.StartNew();
		
		//bool result = generateNode(dungeonHead, null);
		generateBranches();
		activateDoorsNavmesh();
		toggleRooms(dungeonRoomsHead, false, false);
		watch.Stop();
		long elapsedMs = watch.ElapsedMilliseconds;
		Debug.Log("map generated in " + elapsedMs + "ms");
		generationDone = true;
	}

	private bool generatePart(DungeonPart part, DungeonRoom previousRoom){
		bool result = false;
		if(previousRoom == null){ //first part
			result = generateNode(part.getFirstNode(), null, part);
		}
		else{ //other parts
			result = generateNode(part.getFirstNode(), previousRoom, part);
		}
		return result;
	}

	public void toggleRooms(DungeonRoom room, bool fullSearch, bool generation){
		if(room == null)
			return;

		bool localFullSearch = fullSearch; //possible bugs, lighter
		//bool localFullSearch = true; //best results, heavier

		if(localFullSearch)
			parkAllRooms(room);

		searchId++;

		KeyValuePair<DungeonRoom, KeyValuePair<MapDoor, int>> currentPair;
		DungeonRoom currentRoom = room;

		Queue<KeyValuePair<DungeonRoom, KeyValuePair<MapDoor, int>>> queue = new Queue<KeyValuePair<DungeonRoom, KeyValuePair<MapDoor, int>>>();
		queue.Enqueue( new KeyValuePair<DungeonRoom, KeyValuePair<MapDoor, int>>(currentRoom, new KeyValuePair<MapDoor, int>(null, 0)));
		currentRoom.lastSearch = searchId;

		int maxDepth = 0;
		if(generation)
			maxDepth = 3;
		else
			maxDepth = 2;

		while(queue.Count != 0){
			currentPair = queue.Dequeue();
			currentRoom = currentPair.Key;
			int currentDepth = currentPair.Value.Value;
			MapDoor currentDoor = currentPair.Value.Key;

			currentRoom.lastSearch = searchId;
			
			if(currentDepth <= maxDepth){
				if(currentRoom.parked){				

					if(currentRoom == room){
						//position doesnt really matter, means nothing is turned on
						if(generation)
							currentRoom.transform.position = new Vector3(100,0,100);
						else
							currentRoom.transform.position = new Vector3(0,0,0);
						currentRoom.parked = false;
					}else{

						//find the door that leads back
						MapDoor door = null;
						foreach(MapDoor d in currentRoom.doors){
							if(d.leadsTo != null && d.leadsTo == currentDoor.belongsTo){
								door = d;
								break;
							}
						}

						//unpark room
						//Debug.Log("calledroom: " + room.id + " room: " + currentRoom.id + " depth: " + currentDepth);
						currentRoom.transform.position = currentDoor.transform.position + currentRoom.transform.position - door.transform.position;
						currentRoom.parked = false;
					}

                    if (!generation){
                        Transform scenery = currentRoom.transform.FindChild("Scenery/Spawners");//.gameObject.SetActive(true);
                        if(scenery != null){
							if(scenery.GetComponent<SpawnerManager>() != null)
                            	scenery.GetComponent<SpawnerManager>().generateSpawners();
						}
					}
				}
			}else{
				if(!currentRoom.parked){
					//park room
					parkRoom(currentRoom, generation);
					if(generation)
                        continue;
				}
			}

			foreach(MapDoor door in currentRoom.doors){
				if(door.used){
					if(door.leadsTo.lastSearch < searchId){
						queue.Enqueue( new KeyValuePair<DungeonRoom, KeyValuePair<MapDoor, int>>(door.leadsTo, new KeyValuePair<MapDoor, int>(door, currentDepth + 1)));
					}
				}
			}
		}
	}

	public void parkAllRooms(DungeonRoom room){
		if(room == null)
			return;
		
		searchId++;
		
		KeyValuePair<DungeonRoom, KeyValuePair<MapDoor, int>> currentPair;
		DungeonRoom currentRoom = room;
		
		Queue<KeyValuePair<DungeonRoom, KeyValuePair<MapDoor, int>>> queue = new Queue<KeyValuePair<DungeonRoom, KeyValuePair<MapDoor, int>>>();
		queue.Enqueue( new KeyValuePair<DungeonRoom, KeyValuePair<MapDoor, int>>(currentRoom, new KeyValuePair<MapDoor, int>(null, 0)));
		currentRoom.lastSearch = searchId;
		
		while(queue.Count != 0){
			currentPair = queue.Dequeue();
			currentRoom = currentPair.Key;
			int currentDepth = currentPair.Value.Value;
			MapDoor currentDoor = currentPair.Value.Key;
			
			currentRoom.lastSearch = searchId;

			if(!currentRoom.parked && currentRoom != room){
				//park room
				parkRoom (currentRoom, true);
			}
			
			foreach(MapDoor door in currentRoom.doors){
				if(door.used){
					if(door.leadsTo.lastSearch < searchId){
						queue.Enqueue( new KeyValuePair<DungeonRoom, KeyValuePair<MapDoor, int>>(door.leadsTo, new KeyValuePair<MapDoor, int>(door, currentDepth + 1)));
					}
				}
			}
		}
	}

	private void activateDoorsNavmesh(){
		searchId++;
		KeyValuePair<DungeonRoom, KeyValuePair<MapDoor, int>> currentPair;
		DungeonRoom currentRoom = dungeonRoomsHead;
		
		Queue<KeyValuePair<DungeonRoom, KeyValuePair<MapDoor, int>>> queue = new Queue<KeyValuePair<DungeonRoom, KeyValuePair<MapDoor, int>>>();
		queue.Enqueue( new KeyValuePair<DungeonRoom, KeyValuePair<MapDoor, int>>(currentRoom, new KeyValuePair<MapDoor, int>(null, 0)));
		currentRoom.lastSearch = searchId;
		
		while(queue.Count != 0){
			currentPair = queue.Dequeue();
			currentRoom = currentPair.Key;
			int currentDepth = currentPair.Value.Value;
			MapDoor currentDoor = currentPair.Value.Key;
			
			currentRoom.lastSearch = searchId;

			if(!currentRoom.parked){ //also park all the rooms, fixes a bug
				//park room
				parkRoom(currentRoom, true);
			}

			foreach(MapDoor door in currentRoom.doors){
				if(door.used){
					door.activateNavmesh();

					if(door.staticDoor != ""){
						GameObject go = Instantiate(Resources.Load("Prefabs/Environment/BreakableWalls/" + doors[door.staticDoor])) as GameObject;
						go.transform.parent = door.transform;
						go.transform.position = door.transform.position;
					}

					if(door.leadsTo.lastSearch < searchId){
						queue.Enqueue( new KeyValuePair<DungeonRoom, KeyValuePair<MapDoor, int>>(door.leadsTo, new KeyValuePair<MapDoor, int>(door, currentDepth + 1)));
					}
				}
			}
		}
	}

	private void parkRoom(DungeonRoom room, bool generation){
        if(!generation)
        {
            Transform other = room.transform.FindChild("Other");
            List<EnemyScript> enemies = new List<EnemyScript>();
            foreach (Transform c in other)
            {
                if (c.GetComponent<EnemyScript>() != null)
                {
                    enemies.Add(c.GetComponent<EnemyScript>());
                }
                else
                {
                    Destroy(c.gameObject);
                }
            }

            foreach(EnemyScript scrpt in enemies)
            {
                scrpt.Eliminate();
            }

            Transform scenery = room.transform.FindChild("Scenery");
            foreach (Transform c in scenery)
            {
                if (String.Compare(c.name, "Spawners") == 0)
                {
                    if(c.GetComponent<SpawnerManager>() != null){
						c.GetComponent<SpawnerManager>().disableSpawners();
					}
                    continue;
                }
                foreach (Transform k in c)
                {
                    ElementiumMonoBehaviour[] emlist = k.GetComponents<ElementiumMonoBehaviour>();
                    foreach (ElementiumMonoBehaviour elem in emlist)
                    {
                        elem.Disable();
                    }
                }
            }
        }
		room.transform.position = new Vector3(-1000, 1000, 1000);
		room.parked = true;
	}

	/*******/

	private bool generateNode(DungeonNode node, DungeonRoom lastRoom, DungeonPart currentPart) 
	{
		if (watch.ElapsedMilliseconds > timeLimit)
		{
			Debug.Log("demorou muito tempo" + watch.ElapsedMilliseconds + "ms");
			throw new Exception("TEST");
		}
		//Debug.Log("generating node " + node.id);
		if (node.type == DungeonNode.DungeonNodeType.Random)
		{
			for (int i = 0; i < MAX_TRIES; i++)
			{
				//Debug.Log ("random node try");
				toggleRooms(lastRoom, false, true);//
				bool result = generateRandomNode(node, lastRoom, currentPart);
				if (result)
					return true;
			}
			return false;
		}
		else if (node.type == DungeonNode.DungeonNodeType.Static)
		{
			for (int i = 0; i < MAX_TRIES; i++)
			{
				toggleRooms(lastRoom, false, true);//
				int result = generateStaticNode(node, lastRoom, currentPart);
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
	
	private bool generateRandomNode(DungeonNode node, DungeonRoom lastRoom, DungeonPart currentPart)
	{
		if (watch.ElapsedMilliseconds > timeLimit)
		{
			Debug.Log("demorou muito tempo" + watch.ElapsedMilliseconds + "ms");
			throw new Exception("TEST");
		}
		List<int> group = groups[node.group];
		DungeonNode child = currentPart.getNextNode(node);
		//if(child != null){
			bool sucessfullPath = false;
			for (int i = 0; i < MAX_TRIES; i++)
			{
				toggleRooms(lastRoom, false, true);//
				int depth = RandomGenerator.Next(node.max - node.min) + node.min;
				int response = generateRandomRoom(lastRoom, child, group, depth, node, currentPart);
				if (response == 0)
				{
					sucessfullPath = true;
					break;
				}
			}
			if (!sucessfullPath)
			{
				//Debug.Log("cleaning children");
				List<MapDoor> prevdoors = lastRoom.doors;
				foreach (MapDoor door in prevdoors){
					if(door.used && door.leadsTo.nodeId == node.id){
						cleanAllChildrenRooms(door.leadsTo);
					}
				}
				return false;
			}
		//}
		return true;
	}
	
	private int generateRandomRoom(DungeonRoom lastRoom, DungeonNode child, List<int> group, int depth, DungeonNode currentNode, DungeonPart currentPart)
	{
		if (watch.ElapsedMilliseconds > timeLimit)
		{
			Debug.Log("demorou muito tempo" + watch.ElapsedMilliseconds + "ms");
			throw new Exception("TEST");
		}
		if (depth == 0)
		{
			if (child == null){
				foreach(KeyValuePair<DungeonPart, bool> kvp in currentPart.adjacentEnd){
					DungeonPart p = kvp.Key;
					toggleRooms(lastRoom, false, true);//

					if(p.getNumberOfRooms() == 0){
						if(!generatePart (p, lastRoom)){
							cleanAllChildrenRooms(lastRoom);
							return 1;
						}
					}else{
						DungeonRoom roomToJumpTo = p.getLowestRoom();
						if(!tryToConnectRooms(lastRoom, roomToJumpTo, kvp.Value)){
							
							//Debug.Log ("connect failed");
							cleanAllChildrenRooms(lastRoom);
							return 1;
						}
					}
				}

				return 0;

			}
			if (!generateNode(child, lastRoom, currentPart))
				return 1;
			else
				return 0;
		}
		
		for (int i = 0; i < MAX_TRIES; i++){

			toggleRooms(lastRoom, false, true);//

			GameObject go = Instantiate(pool.getObject(rooms[group[RandomGenerator.Next(group.Count)]])) as GameObject;
			go.transform.parent = transform;
			DungeonRoom room = go.GetComponent<DungeonRoom>();
			room.id = ++roomId;
			room.part = currentPart;
			room.partId = currentPart.id;
			currentPart.addRoom(room);

			List<MapDoor> prevdoors = lastRoom.doors;
			prevdoors.Shuffle();
			foreach (MapDoor door in prevdoors){

				//Debug.Log("trying a random door");
				toggleRooms(room, false, true);//
				if (!door.goesBackward){
					if (!door.used){
						if (positionRandomRoom(room, lastRoom, door, currentNode)){
							int r = generateRandomRoom(room, child, group, depth - 1, currentNode, currentPart);
							if (r == 1){
								cleanAllChildrenRooms(room);
								disconnectRooms(room, lastRoom);
								door.resetDoor();
								continue;
							}
							return 0;
						}
						else{
							door.resetDoor();
						}
					}
				}
			}
			room.part.removeRoom(room.id);
			room.transform.position = cemiteryPosition;
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
	private int generateStaticNode(DungeonNode node, DungeonRoom lastRoom, DungeonPart currentPart)
	{
		GameObject go = Instantiate(pool.getObject(rooms[node.room])) as GameObject;
		go.transform.parent = transform;
		DungeonRoom room = go.GetComponent<DungeonRoom>();
		room.id = ++roomId;
		room.part = currentPart;
		room.partId = currentPart.id;
		currentPart.addRoom(room);
		toggleRooms(lastRoom, false, true);//TODO - desnecessario?

		if (dungeonRoomsHead == null)
			dungeonRoomsHead = room;
		if (lastRoom != null)
		{
			if (!positionStaticRoom(room, lastRoom, node))
			{
				room.part.removeRoom(room.id);	
				room.transform.position = cemiteryPosition;
				Destroy(go);
				return 1;
			}
		}
		else
		{
			room.randomGroup = node.randomgroup;
			room.nodeId = node.id;
		}

		DungeonNode child = currentPart.getNextNode(node);
		if(child != null){
			if (!generateNode(child, room, currentPart))
			{
				//children failed but room was positioned, we have to remove the references to it
				if (lastRoom != null){
					disconnectRooms(room, lastRoom);
				}
				room.part.removeRoom(room.id);
				room.transform.position = cemiteryPosition;
				Destroy(go);
				return 2;
			}
		}else{
			foreach(KeyValuePair<DungeonPart, bool> kvp in currentPart.adjacentEnd){
				DungeonPart p = kvp.Key;
				toggleRooms(room, false, true);//
				if(p.getNumberOfRooms() == 0){
					if(!generatePart (p, room)){

						//children failed but room was positioned, we have to remove the references to it
						cleanAllChildrenRooms(room);
						if (lastRoom != null){
							disconnectRooms(room, lastRoom);
						}
						room.part.removeRoom(room.id);
						room.transform.position = cemiteryPosition;
						Destroy(go);
						return 2;
					}
				}else{
					DungeonRoom roomToJumpTo = p.getLowestRoom();
					if(!tryToConnectRooms(room, roomToJumpTo, kvp.Value)){

						//children failed but room was positioned, we have to remove the references to it
						cleanAllChildrenRooms(room);
						if (lastRoom != null){
							disconnectRooms(room, lastRoom);
						}
						room.part.removeRoom(room.id);
						room.transform.position = cemiteryPosition;
						Destroy(go);
						return 2;
					}
				}
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

        toggleRooms(lastRoom, false, true);

		//position the piece correctly
		float angle = 180.0f - Vector3.Angle(door.transform.forward, newdoor.transform.forward);
		room.transform.RotateAround(room.transform.position, Vector3.up, angle);
		
		//if that wasnt correct rotate the other way... TODO: change to something cleaner
		float newangle = Vector3.Angle(door.transform.forward, newdoor.transform.forward);
		if (newangle != 180)
			room.transform.RotateAround(room.transform.position, Vector3.up, -2 * angle);
		
		room.transform.position = door.transform.position + room.transform.position - newdoor.transform.position;
		
		//check if the room doesnt colide with anything in this position
		if (!checkColisionsRoom(room))
		{
			return false;
		}
		else
		{
			Transform scenery = room.transform.FindChild("Scenery");
            rotateToCamera(scenery);

			//doesnt collide on the correct position, so it can be used
			door.used = true;
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
			newdoor.leadsTo = lastRoom;
			newdoor.goesBackward = true;

			if(lastRoom != null && lastRoom.partId != room.partId && lastRoom.partId != 0 && room.partId != 0){
				door.staticDoor = doorsUsage[lastRoom.partId + "+" + room.partId];
			}
			return true;
		}
	}

    private void rotateToCamera(Transform scenery)
    {
        foreach (Transform child in scenery)
        {
            if (scenery.childCount == 0)
                child.forward = new Vector3(0, 0, 1);
            else
            {
                foreach (Transform children in child)
                    children.forward = new Vector3(0, 0, 1);
            }
        }
    }

	private bool tryToConnectRooms(DungeonRoom room, DungeonRoom roomToConnectTo, bool allowsRandomTarget){

		DungeonRoom targetRoom = roomToConnectTo;

		while(true){

			//try for each pair of doors
			List<MapDoor> prevdoors = room.doors;
			prevdoors.Shuffle();
			foreach (MapDoor door in prevdoors){
				if (!door.used){

					List<MapDoor> newdoors = targetRoom.doors;
					newdoors.Shuffle();
					foreach (MapDoor newdoor in newdoors){
						if (!newdoor.used && (door.doorWidth == newdoor.doorWidth)){

							//check if they point to oposing directions
							if(Vector3.Dot (door.transform.forward, newdoor.transform.forward) == -1){

								door.backTries++;
								newdoor.backTries++;

								//they are compatible, connect them and now check the physics
								door.leadsTo = newdoor.belongsTo;
								door.backJump = true;
								door.used = true;
								newdoor.leadsTo = door.belongsTo;
								newdoor.frontJump = true;
								newdoor.used = true;

								toggleRooms(room, true, true);
								if(!checkColisionsState(room)){

									//it failed, try next door
									door.resetDoor();
									newdoor.resetDoor();
									continue;
								}

								toggleRooms(targetRoom, true, true);
								if(!checkColisionsState(targetRoom)){

									//it failed, try next door
									door.resetDoor();
									newdoor.resetDoor();
									continue;
								}

								door.staticDoor = doorsUsage[room.partId + "+" + roomToConnectTo.partId];

								//back to the original so it doesnt cause problems
								toggleRooms(room, true, true);

								return true;
							}
						}
					}
				}
			}
			if(!allowsRandomTarget)
				break;

			bool hasNewRoom = false;
			foreach(MapDoor d in targetRoom.doors){
				int partid = targetRoom.partId;
				if(!d.goesBackward && d.used && !d.backJump && !d.frontJump && d.leadsTo.partId == partid){
					targetRoom = d.leadsTo;
					hasNewRoom = true;
					break;
				}
			}

			if(hasNewRoom)
				continue;
			break;
		}

		return false;
	}

	private bool checkColisionsState(DungeonRoom room){
		searchId++;
		KeyValuePair<DungeonRoom, KeyValuePair<MapDoor, int>> currentPair;
		DungeonRoom currentRoom = room;
		
		Queue<KeyValuePair<DungeonRoom, KeyValuePair<MapDoor, int>>> queue = new Queue<KeyValuePair<DungeonRoom, KeyValuePair<MapDoor, int>>>();
		queue.Enqueue( new KeyValuePair<DungeonRoom, KeyValuePair<MapDoor, int>>(currentRoom, new KeyValuePair<MapDoor, int>(null, 0)));
		currentRoom.lastSearch = searchId;
		
		while(queue.Count != 0){
			currentPair = queue.Dequeue();
			currentRoom = currentPair.Key;
			int currentDepth = currentPair.Value.Value;
			MapDoor currentDoor = currentPair.Value.Key;
			
			currentRoom.lastSearch = searchId;
	
			if(!checkColisionsRoom(currentRoom)){
				return false;
			}
			
			foreach(MapDoor door in currentRoom.doors){
				if(door.used && !door.leadsTo.parked){
					if(door.leadsTo.lastSearch < searchId){
						queue.Enqueue( new KeyValuePair<DungeonRoom, KeyValuePair<MapDoor, int>>(door.leadsTo, new KeyValuePair<MapDoor, int>(door, currentDepth + 1)));
					}
				}
			}
		}
		return true;
	}

	private bool checkColisionsRoom(DungeonRoom room){

		Transform colliders_object = room.transform.Find("Colliders");
		foreach (Transform child in colliders_object){

			BoxCollider collider = child.GetComponent<BoxCollider>();
			Bounds bounds = collider.bounds;
			int layerMask = 0 | (1 << LayerMask.NameToLayer("RoomColliders"));
			Collider[] otherColliders = Physics.OverlapSphere(child.position, SPHERE_RADIUS, layerMask);

			foreach (Collider c in otherColliders){
				if(c.transform.name == "roomcheck")
					continue;

				if (c.transform.parent.parent == collider.transform.parent.parent){
					continue;
				}

				else if (bounds.Intersects(c.bounds)){
					return false;
				}
			}
		}
		return true;
	}

	private bool disconnectDoor(MapDoor door){
		foreach (MapDoor d in door.leadsTo.doors){
			if (d.used && d.leadsTo == door.belongsTo){
				d.resetDoor();
				break;
			}
		}
		
		door.resetDoor();
		return true;
	}

	private bool disconnectRooms(DungeonRoom room, DungeonRoom otherRoom){
		//Debug.Log("Disconecting Doors");
		foreach (MapDoor d in room.doors){
			if (d.leadsTo == otherRoom){
				d.resetDoor();
				//Debug.Log ("side A");
				break;
			}
		}

		foreach (MapDoor d in otherRoom.doors){
			if (d.leadsTo == room){
				d.resetDoor();
				//Debug.Log ("side B");
				break;
			}
		}
		return true;
	}

	private void cleanAllChildrenRooms(DungeonRoom room)
	{
		//Debug.Log("cleaning rooms---------------------");
		foreach (MapDoor door in room.doors)
		{
			if (!door.goesBackward)
			{
				if(door.leadsTo == null){
					door.resetDoor();
				}
				else if(door.backJump || door.frontJump){
					disconnectDoor(door);
				}
				else {
					cleanAllChildRoomsRecur(door.leadsTo);
					door.resetDoor();
				}
			}
		}
		return;
	}
	
	private void cleanAllChildRoomsRecur(DungeonRoom room)
	{
		foreach (MapDoor door in room.doors)
		{
			if(door.backJump || door.frontJump){
				disconnectDoor(door);
			}
			else if (!door.goesBackward && door.leadsTo != null)
			{
				cleanAllChildRoomsRecur(door.leadsTo);
				door.resetDoor();
			}else if(door.leadsTo == null){
				door.resetDoor();
			}
		}
		room.part.removeRoom(room.id);
		room.transform.position = cemiteryPosition;
		Destroy(room.gameObject);
		return;
	}

	/**********/

	public HashSet<MapDoor> emptydoors = new HashSet<MapDoor>();

	private void generateBranches(){
		exploreRoomForDoors(dungeonRoomsHead);

		int quantity = RandomGenerator.Next(maxRandom - minRandom) + minRandom;
		for (; quantity > 0; quantity--){

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

				toggleRooms(door.belongsTo, true, true);

				List<int> group = groups[door.belongsTo.randomGroup];
				GameObject go = Instantiate(pool.getObject(rooms[group[RandomGenerator.Next(group.Count)]])) as GameObject;
				go.transform.parent = transform;
				DungeonRoom room = go.GetComponent<DungeonRoom>();
				room.id = ++roomId;

				List<MapDoor> newdoors = room.doors;
				newdoors.Shuffle();
				foreach (MapDoor newdoor in newdoors){

					if (!newdoor.used && (door.doorWidth == newdoor.doorWidth)){
						if (positionRoom(door, newdoor, room, door.belongsTo, null)){

							emptydoors.Remove(door);
							foreach (MapDoor idoor in room.doors){

								if (!idoor.used){

									emptydoors.Add(idoor);
								}
							}
							roomDone = true;
							break;
						}
					}
				}
				if (!roomDone){
					go.transform.position = cemiteryPosition;
					Destroy(go);
				}
				else
					break;
			}
		}
	}
	
	private void exploreRoomForDoors(DungeonRoom exploringRoom){
		
		foreach (MapDoor door in exploringRoom.doors){
			
			if (!door.used){
				if (exploringRoom.randomGroup != "0"){
					emptydoors.Add(door);
				}
			}
			else if (!door.goesBackward && !door.backJump && !door.frontJump){
				exploreRoomForDoors(door.leadsTo);
			}
		}
	}
	
	/**********/
	
	private void dumpParts(){
		foreach (KeyValuePair<int, DungeonPart> kvp in parts){
			kvp.Value.dump();
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
	}

	private static void dumpDictionary(Dictionary<int, DungeonRoom> dict){
		String result = "";
		foreach(KeyValuePair<int, DungeonRoom> kvp in dict){
			result += kvp.Key;
			result += " ";
		}
		Debug.Log (result);
	}
}
