using UnityEngine;
using System.Collections;
using Includes;

public class PlayerData : MonoBehaviour {

    public int currentElement = (int)Elements.NEUTRAL;
    public float Maxhealth = Player.maxHealth;
    public float currentHealth = Player.maxHealth;
}
