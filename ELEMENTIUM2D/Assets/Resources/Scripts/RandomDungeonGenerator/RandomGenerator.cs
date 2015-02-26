using UnityEngine;
using System.Collections;

public class RandomGenerator {

	private static System.Random rng = new System.Random(); 

	public static int Next(int value){
		return rng.Next(value);
	}
}
