using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrefabFactory : MonoBehaviour {

    private static GameObject thisObject;

    void Awake()
    {
        thisObject = gameObject;
    }

    public static List<GameObject> createPrefabs(GameObject prefab, int number)
    {
        List<GameObject> obj = new List<GameObject>();
        for (int i = 0; i < number; i++)
        {
            GameObject created = Instantiate(prefab) as GameObject;
            created.transform.parent = thisObject.transform;
            obj.Add(created);
        }
        return obj;
    }
}
