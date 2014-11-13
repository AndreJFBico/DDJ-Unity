using UnityEngine;
using System.Collections;

public class DestroyTimeout : MonoBehaviour {

    public float timeOut = 1;

	// Use this for initialization
	void Start () {
        Invoke("DestroyNow", timeOut);
	}
	
    private void DestroyNow()
    {
        GameObject.Destroy(gameObject);
    }
}
