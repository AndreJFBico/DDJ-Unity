using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Shoot))]
[RequireComponent(typeof(LookAtMouse))]
[RequireComponent(typeof(CharacterMove))]

public class PlayerStats : MonoBehaviour {

    public float hp;
    public float attack;
    public float defence;
    public float projectileRange;

    public float fireElementLevel;
    public float earthElementLevel;
    public float gold;

    void Awake()
    {
        hp = 5;
        attack = 1;
        defence = 0;
        projectileRange = 1;

        fireElementLevel = 0;
        earthElementLevel = 0;
        gold = 0;
    }

	// Use this for initialization
	void Start () {
	
	}
	
    void OnGUI()
    {
        int bWidth = 80;
        GUI.Box(new Rect(0, 0, bWidth, 40), "HP: " + hp);
        GUI.Box(new Rect(bWidth, 0, bWidth, 40), "Attack: " + attack);
        GUI.Box(new Rect(bWidth * 2, 0, bWidth, 40), "Defence: " + defence);
        GUI.Box(new Rect(bWidth * 3, 0, bWidth, 40), "Range: " + projectileRange);
        GUI.Box(new Rect(bWidth * 4, 0, bWidth, 40), "Fire: " + fireElementLevel);
        GUI.Box(new Rect(bWidth * 5, 0, bWidth, 40), "Earth: " + earthElementLevel);
        GUI.Box(new Rect(bWidth * 6, 0, bWidth, 40), "Gold: " + gold);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
