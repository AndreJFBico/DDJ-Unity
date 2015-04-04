using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnScript : MonoBehaviour {

    public Transform enemy;
    public int amount_to_spawn = 10;
    public int spawn_timer = 1;

    private List<Transform> toSpawn;
    private List<Transform> spawned;

    private Transform position;

	// Use this for initialization
	void Start () 
    {
        position = transform.FindChild("SpawnPosition");
	    spawned = new List<Transform>();
        toSpawn = new List<Transform>();
        for (int i = 0; i < amount_to_spawn; i++)
        {
            GameObject obj = Instantiate(enemy.gameObject, position.transform.position, position.transform.rotation) as GameObject;
            obj.transform.parent = transform;
            PathAgent pa = obj.GetComponentInChildren<PathAgent>();
            pa.startPosition = transform.position;
            obj.GetComponent<EnemyScript>().setSpawner(this);
            // deactivates the object
           // Debug.Log(obj);
            obj.GetComponent<EnemyScript>().retrieveGuiFromCanvas();
            obj.SetActive(false);
            toSpawn.Add(obj.transform);
        }
        InvokeRepeating("SpawnEnemy", spawn_timer, spawn_timer);
	}

    void SpawnEnemy()
    {
        if(toSpawn.Count > 0)
        {
            Transform obj = toSpawn[0];
            obj.GetComponent<EnemyScript>().sendGuiToCanvas();   
            obj.gameObject.SetActive(true);
            obj.GetComponentInChildren<PathAgent>().startCheckingMovement();
            toSpawn.Remove(obj);
            spawned.Add(obj);
        }
    }

    public void despawn(Transform obj)
    {
        obj.GetComponentInChildren<PathAgent>().target = null;
        obj.GetComponent<EnemyScript>().retrieveGuiFromCanvas();
        StatusEffect[] effects = obj.GetComponents<StatusEffect>();
        foreach (StatusEffect item in effects)
	    {
            Destroy(item);
	    }
        obj.gameObject.SetActive(false);
        obj.transform.position = transform.position;
        //obj.transform.rotation = transform.rotation;
        spawned.Remove(obj);
        toSpawn.Add(obj);       
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
