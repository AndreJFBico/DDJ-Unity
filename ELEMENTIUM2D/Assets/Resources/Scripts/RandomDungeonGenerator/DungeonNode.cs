using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonNode {

	public enum DungeonNodeType{Static, Random};

	public DungeonNode parent = null;

	public DungeonNodeType type;

	public int max = 0;
	public int min = 0;
	public string group = "";
	public int room = 1;
	public int id = 0;
	public string randomgroup = "";

	public DungeonNode(string type_a, int id_a){
		if(type_a == "S")
			type = DungeonNodeType.Static;
		else if(type_a == "R")
			type = DungeonNodeType.Random;

		id = id_a;
	}
}
