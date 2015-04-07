using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnScript : ElementiumMonoBehaviour {

/*    public Transform enemy;
    public int amount_to_spawn = 10;
    public int spawn_timer = 1;

    private List<Transform> toSpawn;
    private List<Transform> spawned;

    private Transform position;

    public override void Disable()
    {
        
    }

    public override void Enable()
    {
        StartCoroutine("SpawnEnemy");
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
            // deactivates the object
            // Debug.Log(obj);
            obj.GetComponent<EnemyScript>().retrieveGuiFromCanvas();
            obj.SetActive(false);
            toSpawn.Add(obj.transform);
        }
        Enable();
    }

    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            if (toSpawn.Count > 0)
            {
                Transform obj = toSpawn[0];
                obj.GetComponent<EnemyScript>().sendGuiToCanvas();
                obj.transform.position = transform.FindChild("SpawnPosition").position;
                obj.gameObject.SetActive(true);
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
        //obj.transform.position = transform.position;
        //obj.transform.parent = transform.parent.parent.FindChild("Other");
        //obj.transform.rotation = transform.rotation;
        spawned.Remove(obj);
        toSpawn.Add(obj);       
    }*/
}
