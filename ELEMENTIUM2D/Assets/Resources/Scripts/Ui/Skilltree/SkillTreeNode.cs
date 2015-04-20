using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Reflection;
using Includes;

[ExecuteInEditMode]
public class SkillTreeNode : MonoBehaviour
{

    private SkillTreeManager manager;

    public bool startNode;

    public float changeWithValue;

    public List<SkillTreeNode> sucessors = new List<SkillTreeNode>();

    public MathOperations operation;

    public SkillTreeNode father;

    // Checks if the node has been selected by the player
    public bool selected = false;

    bool searched = false;

    public bool lockedForever = false;

    private bool unknown = false;

    [SerializeField]
    int choiceIndex = 0;

    [SerializeField]
    string variableBeingChanged;

    // Use this for initialization
    void Awake()
    {
        manager = (GameObject.Find("SkillTree") as GameObject).GetComponent<SkillTreeManager>();
    }

    public void setupHiearchy()
    {
        sucessors.Clear();
        if (!startNode)
        {
            father = transform.parent.GetComponent<SkillTreeNode>();
            sucessors.Add(transform.parent.GetComponent<SkillTreeNode>());
        }
            
        
        foreach (Transform t in transform)
        {
            sucessors.Add(t.GetComponent<SkillTreeNode>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isUknown())
            gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.40f);
        else if (!selected)
            gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.20f);
        else if(selected)
            gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1.00f);      
    }


    //Attentiont lines will be repeated
    void OnGUI()
    {
        foreach(SkillTreeNode n in sucessors)
        {
            if(n.gameObject.activeSelf && !n.isUknown())
            {
                GuiHelper.DrawLine(new Vector2(GetComponent<RectTransform>().position.x, Screen.height - GetComponent<RectTransform>().position.y), new Vector2(n.GetComponent<RectTransform>().position.x, Screen.height - n.GetComponent<RectTransform>().position.y), Color.white);
                Debug.DrawLine(
                    new Vector2(
                        GetComponent<RectTransform>().position.x,
                        GetComponent<RectTransform>().position.y),
                    new Vector2(
                        n.GetComponent<RectTransform>().position.x,
                        n.GetComponent<RectTransform>().position.y), Color.white);
            }
        }
    }

    public void setUnknown()
    {
        GetComponent<Image>().sprite = GameManager.Instance.UnknownSymbol;
        unknown = true;
    }

    public bool checkIndividualLimit()
    {
        if(selected)
            return true;
        var type = typeof(SkillTreeManager); // Get type pointer
        FieldInfo[] fields = type.GetFields(); // Obtain all fields
        foreach (var field in fields) // Loop through fields
        {
            // is a float
            if (typeof(float).IsAssignableFrom(field.FieldType))
            {
                if (field.Name.CompareTo("cur_lim_" + variableBeingChanged) == 0)
                {
                    if (((float)field.GetValue(manager)) < GameManager.Instance.getStatVariable("lim_" + variableBeingChanged))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    void checkIfLimits()
    {
        var type = typeof(SkillTreeManager); // Get type pointer
        FieldInfo[] fields = type.GetFields(); // Obtain all fields
        foreach (var field in fields) // Loop through fields
        {
            // is a float
            if (typeof(float).IsAssignableFrom(field.FieldType))
            {
                if (field.Name.CompareTo("cur_lim_" + variableBeingChanged) == 0)
                {
                    if(!selected)
                    {
                        if (manager.cur_lim_points > 0)
                        {
                            if (((float)field.GetValue(manager)) < GameManager.Instance.getStatVariable("lim_" + variableBeingChanged))
                            {
                                selected = true;
                                field.SetValue(manager, ((float)field.GetValue(manager)) + 1);
                                manager.cur_lim_points -= 1;
                                manager.updatePointsText();
                                manager.predictChanges();
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (((float)field.GetValue(manager)) > 0)
                        {
                            selected = false;
                            field.SetValue(manager, ((float)field.GetValue(manager)) - 1);
                            manager.cur_lim_points += 1;
                            manager.updatePointsText();
                            manager.predictChanges();
                            return;
                        }
                    }
                }
            }
        }
    }

    public void PointerEnter()
    {
        string info = "Changing: " + variableBeingChanged + "\n"
                    + "With " + operation.ToString() + " " + changeWithValue;
        manager.displayInfo(info);
    }

    public void MouseCallBack()
    {
        if (!lockedForever && !unknown)
        {
            //not selected
            if (!selected)
            {
                foreach (SkillTreeNode node in sucessors)
                {
                    if(node.selected)
                    {
                        checkIfLimits();
                        return;
                    }
                }
            }
            else // selected
            {
                foreach(SkillTreeNode n in sucessors)
                {
                    // Checks if theres a path to the selected path
                    // if there isnt return
                    if(!n.startNode && (n.selected && !manager.checkPath(this, n)))
                    {
                        return;
                    }                        
                }
                checkIfLimits();
            }
        }
    }

    public void setVariableBeingChanged(string var)
    {
        variableBeingChanged = var;
    }

    public string getVariableBeingChanged()
    {
        return variableBeingChanged;
    }

    public float getValueBeingChanged()
    {
        return changeWithValue;
    }

    public void setUnsearched()
    {
        searched = false;
    }

    public void setSearched()
    {
        searched = true;
    }

    public bool isSearched()
    {
        return searched;
    }

    public bool isSearchable()
    {
        return selected && !searched;
    }

    public int getPreviousChoiceIndex()
    {
        return choiceIndex;
    }

    public void setPreviousChoiceIndex(int ci)
    {
        choiceIndex = ci;
    }

    public bool isUknown()
    {
        return unknown;
    }
}
