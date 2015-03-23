using UnityEngine;
using System.Collections;

public class SortLayer : MonoBehaviour {

    public enum Objects { STATIC, DYNAMIC };
    private GameObject player;
    public Objects type; 

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().sortingOrder = Mathf.RoundToInt(-(transform.position.z + 250)*100);
	}
	
	// Update is called once per frame
	void Update () {
	    if(type != Objects.STATIC)
        {
            GetComponent<Renderer>().sortingOrder = Mathf.RoundToInt(-(transform.position.z + 250) * 100);
        }
    }
}
