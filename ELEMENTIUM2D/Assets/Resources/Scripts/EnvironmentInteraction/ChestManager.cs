using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using Includes;

public class ChestManager{

    private enum RandomChestsQuality { treasureChestWeakest, treasureChestModerate, treasureChestGood, treasureChestBest } 

	private static ChestManager _instance = null;

    private static int maxChests;

    private static List<TreasureChest> _beingUsedChests;

    private static List<TreasureChest> _spawnedChests;

    private static List<Sprite> _chestQualitySprites;

    private GameObject _randomChest;

    #region Initialization

    protected ChestManager() { }
    // Singleton pattern implementation
    public static ChestManager Instance { get { if (_instance == null) { _instance = new ChestManager(); init(); } return _instance; } }

    private static void init()
    {
        _beingUsedChests = new List<TreasureChest>();
        _spawnedChests = new List<TreasureChest>();
        _chestQualitySprites = new List<Sprite>();

        string chestSpritePath = "Sprites/Environment/Chests/";
        string[] chestNames = Enum.GetNames(typeof(RandomChestsQuality));
        foreach(string name in chestNames)
        {
            Sprite sp = Resources.Load<Sprite>(chestSpritePath + name);
            _chestQualitySprites.Add(sp);
        }  
    }

    public void sceneInit()
    {
        _beingUsedChests = new List<TreasureChest>();
        _spawnedChests = new List<TreasureChest>();

        _randomChest = Resources.Load("Prefabs/Environment/Treasures/RandomTreasure") as GameObject;
        maxChests = 30;

        //For the permanent chests in the rooms we need to use the factory to create them and then to store them
        //Then on the creation of the rooms we need to set these chest as child objects of the room

        //List<GameObject> objs = PrefabFactory.createPrefabs(_chest, maxChests);
        //foreach (GameObject o in objs)
        //{
        //    TreasureChest chest = o.GetComponent<TreasureChest>();
        //    o.SetActive(false);
        //    _spawnedChests.Add(chest);
        //}
    }
 
	#endregion

    public void addStaticChest(Transform transf, string message, Color color)
    {
        TreasureChest chest;
        if(_spawnedChests.Count > 0)
        {
            chest = _spawnedChests[0];
            _spawnedChests.RemoveAt(0);
            _beingUsedChests.Add(chest);
        }
        else
        {
            chest = _beingUsedChests[0];
            _beingUsedChests.RemoveAt(0);
            _beingUsedChests.Add(chest);
        }

        chest.gameObject.transform.position = transf.position;
        chest.gameObject.SetActive(true);
    }

    public GameObject randomChest()
    {
        GameObject chest = PrefabFactory.createPrefab(_randomChest);
        int index = PlayerStats.multiplierLevelIndex();

        chest.GetComponentInChildren<TreasureChest>().updateClosedSprite(_chestQualitySprites[index]);

        return chest;
    }
}
