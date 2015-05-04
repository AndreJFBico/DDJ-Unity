using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonPart {
	
	public LinkedList<DungeonNode> children = new LinkedList<DungeonNode>();
	public List<DungeonPart> adjacentStart = new List<DungeonPart>();
	public List<KeyValuePair<DungeonPart, bool>> adjacentEnd = new List<KeyValuePair<DungeonPart, bool>>();

	private SortedDictionary<int, DungeonRoom> partRooms = new SortedDictionary<int, DungeonRoom>();

	public int id = 0;

	public string dropGroup = "0";

	public DungeonPart(int id_a){
		id = id_a;
	}
	
	public void addChild(DungeonNode newChild){
		children.AddLast(newChild);
	}

	public void addAdjacentStart(DungeonPart newChild){
		adjacentStart.Add(newChild);
	}

	public void addAdjacentEnd(DungeonPart newChild, bool randomTarget){
		adjacentEnd.Add(new KeyValuePair<DungeonPart, bool>(newChild, randomTarget));
	}

	public void addRoom(DungeonRoom room){
		partRooms.Add(room.id, room);
	}

	public void removeRoom(int id){
		partRooms.Remove(id);
	}

	public DungeonNode getFirstNode(){
		return children.First.Value;
	}

	public DungeonNode getNextNode(DungeonNode currentNode){
		LinkedListNode<DungeonNode> v = children.Find(currentNode).Next;
		if(v != null)
			return v.Value;
		return null;
	}

	public int getNumberOfRooms(){
		return partRooms.Count;
	}

	public DungeonRoom getLowestRoom(){
		foreach(KeyValuePair<int, DungeonRoom> kvp in partRooms)
			return kvp.Value;
		return null;
	}

	public void dump(){
		string value = "" + id;
		value += " start: ";
		foreach(DungeonPart dn in adjacentStart){
			value += dn.id;
			value += " ";
		}

		value += "end: ";
		foreach(KeyValuePair<DungeonPart, bool> dn in adjacentEnd){
			value += dn.Key.id;
			value += "|";
			value += dn.Value;
			value += " ";
		}

		value += "nodes: ";
		foreach(DungeonNode dn in children){
			value += "n";
			value += " ";
		}
		value += "rooms: " + getNumberOfRooms();
		Debug.Log(value);
	}
}
