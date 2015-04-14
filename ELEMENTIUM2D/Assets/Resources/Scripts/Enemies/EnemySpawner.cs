using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Includes;

public class EnemySpawner : EnemyScript
{
    public Transform enemy;
    public int amount_to_spawn = 10;
    public int spawn_timer = 1;

    private List<Transform> toSpawn;
    private List<Transform> spawned;

    private Transform position;
    private SpawnerManager _spawnerManager;


    private IEnumerator spawnEnemy;

	// Use this for initialization
	protected override void Awake()
    {
        type = Elements.NEUTRAL;
        maxHealth = EnemyStats.BasicNeutral.maxHealth;
        health = maxHealth;
        damage = EnemyStats.BasicNeutral.damage;
        defence = EnemyStats.BasicNeutral.defence;
        waterResist = EnemyStats.BasicNeutral.waterResist;
        earthResist = EnemyStats.BasicNeutral.earthResist;
        fireResist = EnemyStats.BasicNeutral.fireResist;
        spawnEnemy = SpawnEnemy();
        base.Awake();
	}

    public override void Disable()
    {
        StopCoroutine(spawnEnemy);
    }

    public override void Enable()
    {
        base.Enable();
        StartCoroutine(spawnEnemy);       
    }

    public override void Init()
    {
        position = transform.FindChild("SpawnPosition");
        spawned = new List<Transform>();
        toSpawn = new List<Transform>();
        for (int i = 0; i < amount_to_spawn; i++)
        {
            GameObject obj = Instantiate(enemy.gameObject, new Vector3(0.0f, 0.1f, 0.0f), position.transform.rotation) as GameObject;
            PathAgent pa = obj.GetComponentInChildren<PathAgent>();
            pa.startPosition = transform.position;
            obj.GetComponent<EnemyScript>().setSpawner(this);
            obj.SetActive(false);
            toSpawn.Add(obj.transform);
        }
        base.Init();
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (toSpawn.Count > 0)
            {
                Transform obj = toSpawn[0];
                obj.transform.position = transform.FindChild("SpawnPosition").position;
                obj.gameObject.SetActive(true);
                PathAgent pa = obj.GetComponentInChildren<PathAgent>();
                pa.startPosition = transform.position;
                //obj.GetComponentInChildren<PathAgent>().startCheckingMovement();
                toSpawn.Remove(obj);
                spawned.Add(obj);
            }
            yield return new WaitForSeconds(spawn_timer);
        }
    }

    public void despawn(Transform obj)
    {
        obj.GetComponentInChildren<PathAgent>().target = null;
        StatusEffect[] effects = obj.GetComponents<StatusEffect>();
        foreach (StatusEffect item in effects)
        {
            Destroy(item);
        }
        obj.gameObject.SetActive(false);
        obj.parent = transform.root;
        //obj.transform.position = transform.position;
        //obj.transform.parent = transform.parent.parent.FindChild("Other");
        //obj.transform.rotation = transform.rotation;
        spawned.Remove(obj);
        toSpawn.Add(obj);
    }

    public override void Eliminate()
    {
        if(_spawnerManager == null)
            Destroy(gameObject);
        else
        {
            _spawnerManager.destroySpawner(this);
        }
    }

    public SpawnerManager SpawnerManager
    {
        set{
            _spawnerManager = value;
        }
    }

}
