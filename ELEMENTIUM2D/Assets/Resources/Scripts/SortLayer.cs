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
    public Transform parentSprite;

    private bool inited;

	// Use this for initialization
	void Start () {
        if (parentSprite)
        {
            GetComponent<Renderer>().sortingOrder = parentSprite.GetComponent<SortLayer>().getCalculatedSortLayer() + 1;
        }
        else
        {
            GetComponent<Renderer>().sortingOrder = Mathf.RoundToInt(-(transform.position.z + 250) * 100);
        }
        int index = 1;
        if (list != null && list.Capacity > 0)
        {
            foreach (SpriteRenderer sr in list)
            {
                if (position == Position.FRONT)
                    sr.sortingOrder = GetComponent<Renderer>().sortingOrder + index;

                if (position == Position.BACK)
                    sr.sortingOrder = GetComponent<Renderer>().sortingOrder - index;
                index++;
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
            int index = 1;
            if (type != Objects.STATIC)
            {
                if (parentSprite)
                {
                    GetComponent<Renderer>().sortingOrder = parentSprite.GetComponent<SortLayer>().getCalculatedSortLayer() + 1;
                }
                else
                {
                    GetComponent<Renderer>().sortingOrder = Mathf.RoundToInt(-(transform.position.z + 250) * 100);
                }
                if (list != null && list.Capacity > 0)
                {
                    foreach (SpriteRenderer sr in list)
                    {
                        if (position == Position.FRONT)
                            sr.sortingOrder = GetComponent<Renderer>().sortingOrder + index;

                        if (position == Position.BACK)
                            sr.sortingOrder = GetComponent<Renderer>().sortingOrder - index;
                        index++;
                    }
                }
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    public int getCalculatedSortLayer()
    {
        return GetComponent<Renderer>().sortingOrder = Mathf.RoundToInt(-(transform.position.z + 250) * 100);
    }
}
