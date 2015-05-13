using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourcesPool {

    public Dictionary<string, UnityEngine.Object> objectsPool = new Dictionary<string, UnityEngine.Object>();

    public ResourcesPool(){

    }

    public UnityEngine.Object getObject(string path) {
        if (objectsPool.ContainsKey(path))
            return objectsPool[path];
        else {
            UnityEngine.Object obj = Resources.Load("Map/Rooms/" + path);
            objectsPool.Add(path, obj);
            return obj;
        }
    }

	public void preloadObject(string path){
		if (!objectsPool.ContainsKey(path)){
			UnityEngine.Object obj = Resources.Load("Map/Rooms/" + path);
			objectsPool.Add(path, obj);
		}
	}
}
