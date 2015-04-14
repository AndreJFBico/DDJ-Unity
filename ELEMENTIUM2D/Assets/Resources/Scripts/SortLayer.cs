using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SortLayer : MonoBehaviour {

    public enum Objects { STATIC, DYNAMIC };
    public enum Position { FRONT, BACK };
    private GameObject player;
    private IEnumerator sLayerRoutine;
    public Objects type;
    public Position position;
    public List<SpriteRenderer> list;

    private bool inited;

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
        inited = true;
        sLayerRoutine = sortLayer();      
        StartCoroutine(sLayerRoutine);
	}

    /*void OnBecameVisible()
    {
        #if UNITY_EDITOR
        if (Camera.current.name == "SceneCamera")
                    return;
        #endif

        //Debug.Log(Camera.current.name);
        if ( Camera.current.name.CompareTo(Camera.main.name) == 0)
        {
            if(!inited)
            {
                inited = true;
                StartCoroutine(sLayerRoutine);              
            }    
        }
    }

    void OnBecameInvisible()
    {
        #if UNITY_EDITOR
                if (Camera.current != null && Camera.current.name == "SceneCamera")
                    return;
        #endif
        Debug.Log("OnBecameInvisible");
        inited = false;
        StopCoroutine(sLayerRoutine);
    }*/
	
	// Update is called once per frame
    IEnumerator sortLayer()
    {
        while (true)
        {
            if (type != Objects.STATIC)
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
            yield return new WaitForSeconds(0.017f);
        }
    }
}
