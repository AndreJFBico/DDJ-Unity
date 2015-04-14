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

    private bool generated;


	// Use this for initialization
	void Awake () {
        _spawningPositions = new List<Transform>();
        _spawners = new List<EnemySpawner>();
        foreach (Transform t in transform)
        {
            _spawningPositions.Add(t);
        }
        generated = false;
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
            for (int i = random; i < _spawningPositions.Count; i++)
            {
                Destroy(_spawningPositions[i].gameObject);
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
            Instantiate(_treasure, spawner.transform.position, spawner.transform.rotation);
            GameManager.Instance.Player.GetComponent<Player>().createFloatingText("Treasure Appeared!");
        }
        Destroy(spawner.gameObject);
    }

}
