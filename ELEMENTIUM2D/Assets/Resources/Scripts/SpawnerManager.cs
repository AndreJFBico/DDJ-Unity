using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Includes;

public class SpawnerManager : MonoBehaviour {

    public int _minSpawners;
    public int _maxSpawners;
    public GameObject _treasure;

    private List<Transform> _spawningPositions;
    private GameObject _spawnerPrefab;
    private List<EnemySpawner> _spawners;
    private DungeonRoom room;
    private GameObject _HPPotion;

    private bool generated;


	// Use this for initialization
    void Awake()
    {
        room = transform.parent.parent.GetComponent<DungeonRoom>();
        _spawningPositions = new List<Transform>();
        _spawners = new List<EnemySpawner>();
        foreach (Transform t in transform)
        {
            _spawningPositions.Add(t);
        }
        generated = false;
        _HPPotion = (GameObject)Resources.Load("Prefabs/Environment/Pickups/HealthPot");
	}

    public void generateSpawners()
    {
        if (!generated)
        {
            int random = Random.Range(_minSpawners, _maxSpawners);
            _spawningPositions.Shuffle();
            for (int i = 0; i < random; i++)
            {
                EnemySpawner sp = _spawningPositions[i].GetComponent<EnemySpawner>();
                sp.tryInitialize();
                sp.SpawnerManager = this;
                _spawners.Add(sp);
            }
            int k = _spawningPositions.Count;
            for (int i = random; i < k; i++)
            {
                GameObject o = _spawningPositions[_spawningPositions.Count - 1].gameObject;
                _spawningPositions.RemoveAt(_spawningPositions.Count-1);
                Destroy(o);
            }
            generated = true;
        }
        else
        {
            enableSpawners();
        }
    }

    public void disableSpawners()
    {
        foreach(EnemySpawner sp in _spawners)
        {
            sp.Disable();
        }
    }

    private void enableSpawners()
    {
        foreach (EnemySpawner sp in _spawners)
        {
            sp.Enable();
        }
    }

    public void destroySpawner(EnemySpawner spawner)
    {
        _spawners.Remove(spawner);
        if(_spawners.Count < 1)
        {
            spawnChest(spawner);
            dropHealthOrb(spawner);
            room.cleared = true;
        }
        Destroy(spawner.gameObject);
    }

    private void spawnChest(EnemySpawner spawner)
    {
        GameObject treasure = ChestManager.Instance.randomChest(room);//(GameObject)Instantiate(_treasure, spawner.transform.position, spawner.transform.rotation);
        treasure.transform.parent = gameObject.transform.parent;
        treasure.transform.position = spawner.transform.position;
        treasure.transform.rotation = spawner.transform.rotation;
        GameManager.Instance.Player.createFloatingText("Treasure Appeared!", 1);
    }

    private void dropHealthOrb(EnemySpawner spawner)
    {
        // OU DROPA 1 POTE QD DROPA O CHEST
        // OU VARIAS QUE NO MAXIMO DAO ESTE VALOR 
        // SPAWNERS -  %HP GAIN
        //    1     -    2-5
        //    2     -    5-10
        //    3     -    7-20
        //    4     -    15-30
        //    5     -    20-40
        //    6     -    25-55
        //    7     -    30-65
        //    8     -    40-75

        int numSpawners = _spawningPositions.Count;
        int index = PlayerStats.multiplierLevelIndex();

        int[] minIntensity = new int[numSpawners];
        int[] maxIntensity = new int[numSpawners];

        minMaxHPPotIntensity(ref minIntensity, ref maxIntensity);

        int minMaxdiff = maxIntensity[numSpawners-1] - minIntensity[numSpawners-1];

        int finalPercentage = minIntensity[numSpawners-1] + (minMaxdiff * index / (GameManager.Instance.Stats.multiplierLevels.Length-1));

        int numHPPots = (int)System.Math.Round(finalPercentage / 7.5f, 0);

        if (numHPPots < 1)
            numHPPots = 1;

        for (int i = 0; i < numHPPots; i++)
        {
            float rotation = (360 / numHPPots) * i;
            Vector3 dir = new Vector3(Mathf.Cos(rotation), 0, Mathf.Sin(rotation));
            GameObject potion = Instantiate(_HPPotion, spawner.transform.position, spawner.transform.rotation) as GameObject;
            potion.GetComponent<HealthPot>().init((float)System.Math.Round(GameManager.Instance.Stats.maxHealth * (finalPercentage/numHPPots / 100.0f), System.MidpointRounding.AwayFromZero));
            potion.GetComponent<Rigidbody>().AddForce(dir*30);

            potion.transform.parent = gameObject.transform.parent;
        }
    }

    private void minMaxHPPotIntensity(ref int[] minIntensity, ref int[] maxIntensity)
    {
        for (int i = 0; i < _spawningPositions.Count; i++)
        {
            switch (i)
            {
                case 0:
                    minIntensity[i] = 2;
                    maxIntensity[i] = 5;
                    break;
                case 1:
                    minIntensity[i] = 5;
                    maxIntensity[i] = 10;
                    break;
                case 2:
                    minIntensity[i] = 7;
                    maxIntensity[i] = 20;
                    break;
                case 3:
                    minIntensity[i] = 15;
                    maxIntensity[i] = 30;
                    break;
                case 4:
                    minIntensity[i] = 20;
                    maxIntensity[i] = 40;
                    break;
                case 5:
                    minIntensity[i] = 25;
                    maxIntensity[i] = 55;
                    break;
                case 6:
                    minIntensity[i] = 30;
                    maxIntensity[i] = 65;
                    break;
                case 7:
                    minIntensity[i] = 40;
                    maxIntensity[i] = 75;
                    break;
                default:
                    minIntensity[i] = 0;
                    maxIntensity[i] = 1;
                    Debug.Log("SOMETHING WENT WRONT IN HEALTHPOTS DROP");
                    break;
            }
        }
    }
}
