using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SortLayer : MonoBehaviour {

    public enum Objects { STATIC, DYNAMIC };
    public enum Position { FRONT, BACK };
    private GameObject player;
    public Objects type;
    public Position position;
    public List<SpriteRenderer> list;

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().sortingOrder = Mathf.RoundToInt(-(transform.position.z + 250)*100);
        if (list != null && list.Capacity > 0)
        {
            foreach (SpriteRenderer sr in list)
            {
                if (position == Position.FRONT)
                    sr.sortingOrder = GetComponent<Renderer>().sortingOrder + 1;

                if (position == Position.BACK)
                    sr.sortingOrder = GetComponent<Renderer>().sortingOrder - 1;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if(type != Objects.STATIC)
        {
            GetComponent<Renderer>().sortingOrder = Mathf.RoundToInt(-(transform.position.z + 250) * 100);
            if (list != null && list.Capacity > 0)
            {
                foreach (SpriteRenderer sr in list)
                {
                    if (position == Position.FRONT)
                        sr.sortingOrder = GetComponent<Renderer>().sortingOrder + 1;

                    if (position == Position.BACK)
                        sr.sortingOrder = GetComponent<Renderer>().sortingOrder - 1;
                }
            }
        }
    }
}
