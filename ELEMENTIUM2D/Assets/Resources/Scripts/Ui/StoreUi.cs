using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Includes;

public class StoreUi : MonoBehaviour
{
    public List<StoreUiElement> values;

    [SerializeField]
    private List<GameObject> instantiatedPrefabs;

    [SerializeField]
    private GameObject pointer;
    [SerializeField]
    private int currentLine = 0;
    [SerializeField]
    private int points = 4;

    [SerializeField]
    GameObject UIVitem;
    [SerializeField]
    GameObject UIHitem;
    [SerializeField]
    GameObject UICount;
    [SerializeField]
    GameObject UIPoints;

    [SerializeField]
    Vector3 horizontalIncrement;
    [SerializeField]
    Vector3 verticalIncrement;

    [SerializeField]
    Vector3 startingHorizontalPosition;

    [SerializeField]
    Vector3 verticalPosition;
    [SerializeField]
    Vector3 horizontalPosition;

    // Use this for initialization
    void Start()
    {
        verticalIncrement = new Vector3(0.0f, instantiatedPrefabs[0].GetComponent<RectTransform>().sizeDelta.y * instantiatedPrefabs[0].GetComponent<RectTransform>().localScale.y, 0.0f);
        //verticalIncrement = new Vector3(0.0f, 54, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        StoreUiElement elem;
        // Controls pointer movement up and down
        if (Input.GetButtonDown("UI_UP") && currentLine > 0)
        {
            pointer.GetComponent<RectTransform>().anchoredPosition3D = pointer.GetComponent<RectTransform>().anchoredPosition3D + verticalIncrement;
            currentLine--;
        }
        if (Input.GetButtonDown("UI_DOWN") && currentLine < values.Count -1)
        {
            pointer.GetComponent<RectTransform>().anchoredPosition3D = pointer.GetComponent<RectTransform>().anchoredPosition3D - verticalIncrement;
            currentLine++;
        }
        if (Input.GetButtonDown("UI_RIGHT") && points > 0)
        {
            elem = values[currentLine];
            if(elem.incrementPointer())
            {
                elem.updateText();
                points--;
                updateAvailablePoints();
            }
        }
        if (Input.GetButtonDown("UI_LEFT"))
        {
            elem = values[currentLine];
            if(elem.decrementPointer())
            {
                elem.updateText();
                points++;
                updateAvailablePoints();
            }
        }
        if(Input.GetButtonDown("UI_ENTER"))
        {
            UpdatePlayerStats();
            Application.LoadLevel("defaultScene");
        }
    }

    int findStatAndReturnCount(string stat)
    {
        foreach(StoreUiElement elem in values)
        {
            if(elem.name.CompareTo(stat) == 0)
            {
                return elem.getNumcheck();
            }
        }
        return 0;
    }

    void UpdatePlayerStats()
    {
        PlayerStats.damage = PlayerStats.def_damage + findStatAndReturnCount("Attack") * PlayerStats.inc_damage_level;
        PlayerStats.defence = findStatAndReturnCount("Defence");
        PlayerStats.earth_level = findStatAndReturnCount("Earth");
        PlayerStats.water_level = findStatAndReturnCount("Water");
        PlayerStats.fire_level = findStatAndReturnCount("Fire");
        PlayerStats.maxHealth = PlayerStats.def_health + findStatAndReturnCount("Health") * PlayerStats.def_inc_health;
        //PlayerStats.defence = ;
        //PlayerStats.maxHealth = ;
    }

    // CALCULATES VERTICAL AND HORIZONTAL OFFSET VALUES
    void CalculateAndLoadValues()
    {
        pointer = GameObject.Find("Pointer");
        UIVitem = Resources.Load("Prefabs/GUI/Vitem") as GameObject;
        UIHitem = Resources.Load("Prefabs/GUI/Hitem") as GameObject;
        UICount = Resources.Load("Prefabs/GUI/Count") as GameObject;
        UIPoints = Resources.Load("Prefabs/GUI/Points") as GameObject;

        horizontalIncrement = new Vector3(UIHitem.GetComponent<RectTransform>().rect.width  * UIHitem.GetComponent<RectTransform>().localScale.x, 0.0f, 0.0f);
        verticalIncrement = new Vector3(0.0f, UIVitem.GetComponent<RectTransform>().rect.height * UIVitem.GetComponent<RectTransform>().localScale.y, 0.0f);

        startingHorizontalPosition = new Vector3(UIVitem.GetComponent<RectTransform>().rect.width * UIHitem.GetComponent<RectTransform>().localScale.x, 0.0f, 0.0f);

        verticalPosition = transform.position;
        horizontalPosition = transform.position + startingHorizontalPosition;
    }

    void updateAvailablePoints()
    {
        UIPoints.GetComponent<Text>().text = "Available Points: " + points;
    }

    // FUNCTION INVOKED ON THE EDITOR TO GENERATE THE UI
    public void GenerateUi()
    {
        Reset();
        GameObject uiPoints = Instantiate(UIPoints, verticalPosition, Quaternion.identity) as GameObject;
        uiPoints.transform.SetParent(transform.parent);
        instantiatedPrefabs.Add(uiPoints);

        // TO AVOID AN EXTRA VARIABLE -> (FIXME)
        UIPoints = uiPoints;

        // VERTICAL POSITION ADJUSTEMENT
        verticalPosition = verticalPosition - verticalIncrement;
        horizontalPosition = verticalPosition + startingHorizontalPosition;

        

        // VERTICAL LINE
        for (int i = 0; i < values.Count; i++)
        {
            StoreUiElement suie = values[i];
            suie.loadCheckedSprites();
            GameObject vitem = Instantiate(UIVitem, verticalPosition, Quaternion.identity) as GameObject;
            Text text = vitem.GetComponentInChildren<Text>();
            text.text = suie.name;
            vitem.transform.SetParent(transform.parent);
            instantiatedPrefabs.Add(vitem);

            // UPDATE POINTER POSITION
            pointer.GetComponent<RectTransform>().anchoredPosition =
                vitem.GetComponent<RectTransform>().anchoredPosition -
                new Vector2(pointer.GetComponent<RectTransform>().rect.width * pointer.GetComponent<RectTransform>().localScale.x,
                            0.0f) +
                new Vector2(0.0f,
                            vitem.GetComponent<RectTransform>().rect.height /2.0f * vitem.GetComponent<RectTransform>().localScale.y);


            // HORIZONTAL LINE
            for (int z = 0; z < suie.maxHorizontalItems; z++)
            {
                GameObject hitem = Instantiate(UIHitem, horizontalPosition, Quaternion.identity) as GameObject;
                hitem.transform.SetParent(transform.parent);
                horizontalPosition += horizontalIncrement;
                instantiatedPrefabs.Add(hitem);
                suie.addBox(hitem.GetComponent<Image>());
            }

            // END OF HORIZONTAL LINE
            GameObject count = Instantiate(UICount, horizontalPosition, Quaternion.identity) as GameObject;
            count.transform.SetParent(transform.parent);
            instantiatedPrefabs.Add(count);
            suie.setPoints(count.GetComponent<Text>());

            // VERTICAL POSITION ADJUSTEMENT
            verticalPosition = verticalPosition - verticalIncrement;
            horizontalPosition = verticalPosition + startingHorizontalPosition;
            currentLine = i;
        }
        updateAvailablePoints();
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
        CalculateAndLoadValues();

        if(values != null)
        {
            foreach (StoreUiElement uiElem in values)
            {
                uiElem.reset();
            }
        }
    }
}
