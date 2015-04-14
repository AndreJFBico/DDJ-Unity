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

    public SkillTreeNode fatherNode;

    public MathOperations operation;

    // Checks if the node has been selected by the player
    public bool selected = false;

    bool searched = false;

    public bool lockedForever = false;

    [SerializeField]
    int choiceIndex = 0;

    [SerializeField]
    string variableBeingChanged;

    // Use this for initialization
    void Start()
    {
        manager = (GameObject.Find("SkillTree") as GameObject).GetComponent<SkillTreeManager>();
    }

    public void setupHiearchy()
    {
        if (!startNode)
            fatherNode = transform.parent.GetComponent<SkillTreeNode>();
        sucessors.Clear();
        foreach (Transform t in transform)
        {
            sucessors.Add(t.GetComponent<SkillTreeNode>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!selected)
            gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.20f);
        else gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1.00f);
    }

    void OnGUI()
    {
        if (fatherNode != null)
        {
            GuiHelper.DrawLine(new Vector2(GetComponent<RectTransform>().position.x, Screen.height - GetComponent<RectTransform>().position.y), new Vector2(fatherNode.GetComponent<RectTransform>().position.x, Screen.height - fatherNode.GetComponent<RectTransform>().position.y), Color.white);
            Debug.DrawLine(
                new Vector2(
                    GetComponent<RectTransform>().position.x,
                    GetComponent<RectTransform>().position.y), 
                new Vector2(
                    fatherNode.GetComponent<RectTransform>().position.x, 
                    fatherNode.GetComponent<RectTransform>().position.y), Color.white);
        }
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
        if (fatherNode != null && !lockedForever)
        {
            if (fatherNode.selected && !selected)
            {
                checkIfLimits();
            }
            else if (fatherNode.selected && selected)
            {
                foreach(SkillTreeNode n in sucessors)
                {
                    if(n.selected)
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

    public void setSearched()
    {
        searched = true;
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

    public SkillTreeNode getFatherNode()
    {
        return fatherNode;
    }
}
