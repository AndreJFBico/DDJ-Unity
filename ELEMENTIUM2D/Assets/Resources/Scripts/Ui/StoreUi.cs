using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoreUi : MonoBehaviour {

    private List<GameObject> instantiatedPrefabs;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GenerateUi(int hE, int vE)
    {
        Reset();
        
        GameObject Vitem = Resources.Load("Prefabs/GUI/Vitem") as GameObject;
        GameObject Hitem = Resources.Load("Prefabs/GUI/Hitem") as GameObject;

        Vector3 horizontalIncrement = new Vector3(Vitem.GetComponent<RectTransform>().rect.size.x / 2.0f, 0.0f, 0.0f);
        Vector3 verticalIncrement = new Vector3(0.0f, Vitem.GetComponent<RectTransform>().rect.size.y / 2.0f, 0.0f);

        Vector3 verticalPosition = transform.position;
        Vector3 horizontalPosition = transform.position + horizontalIncrement;

        for(int i = 0; i < vE; i++)
        {
            GameObject vitem = Instantiate(Vitem, verticalPosition, Quaternion.identity) as GameObject;
            vitem.transform.SetParent(transform.parent);
            instantiatedPrefabs.Add(vitem);
            for(int z = 0; z < hE; z++)
            {
                GameObject hitem = Instantiate(Hitem, horizontalPosition, Quaternion.identity) as GameObject;
                hitem.transform.SetParent(transform.parent);
                horizontalPosition += horizontalIncrement;
                instantiatedPrefabs.Add(hitem);
            }

            verticalPosition = verticalPosition - verticalIncrement;
            horizontalPosition = verticalPosition + horizontalIncrement;
        }
    }

    public void Reset()
    {
        if (instantiatedPrefabs != null)
        {
            foreach (GameObject obj in instantiatedPrefabs)
            {
                DestroyImmediate(obj);
            }
        }
        instantiatedPrefabs = new List<GameObject>();
    }
}
