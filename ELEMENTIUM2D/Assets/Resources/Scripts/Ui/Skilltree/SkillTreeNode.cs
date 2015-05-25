using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Reflection;
using Includes;

[ExecuteInEditMode]
public class SkillTreeNode : MonoBehaviour
{
    public int pointCost = 1;

    private SkillTreeManager manager;

    public bool startNode;

    public float changeWithValue;

    public List<SkillTreeNode> sucessors = new List<SkillTreeNode>();

    public MathOperations operation;

    [HideInInspector]
    public SkillTreeNode father;

    // Checks if the node has been selected by the player
    public bool selected = false;

    [HideInInspector]
    bool searched = false;

    public bool changeAbilityNode = false;

    public bool lockedForever = false;

    private bool unknown = false;

    [HideInInspector]
    [SerializeField]
    private List<GameObject> linerenderers;

    [SerializeField]
    public string information;

    [HideInInspector]
    [SerializeField]
    int choiceIndex = 0;

    [HideInInspector]
    [SerializeField]
    string variableBeingChanged;

    // Use this for initialization
    void Awake()
    {
        manager = (GameObject.Find("SkillTree") as GameObject).GetComponent<SkillTreeManager>();
    }

    public void clearLineRenderers()
    {
        foreach (GameObject obj in linerenderers)
        {
            DestroyImmediate(obj);
        }
        linerenderers.Clear();
    }

    public void generateLineRenderers()
    {
        foreach(GameObject obj in linerenderers)
        {
            DestroyImmediate(obj);
        }
        linerenderers.Clear();
        foreach (SkillTreeNode n in sucessors)
        {
            GameObject obj = new GameObject();
            obj.name = "UILineRenderer";

            obj.transform.parent = manager.transform.FindChild("Parent");
           
            obj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            obj.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            UILineRenderer lineRenderer = obj.AddComponent<UILineRenderer>();
            lineRenderer.Points = new Vector2[2];
            if(startNode)
                lineRenderer.Points[0] = new Vector2(0.3f, -0.3f);
            else lineRenderer.Points[0] = new Vector2(GetComponent<RectTransform>().localPosition.x, GetComponent<RectTransform>().localPosition.y);//new Vector2(GetComponent<RectTransform>().position.x, GetComponent<RectTransform>().position.y);
            if(n.startNode)
                lineRenderer.Points[1] = new Vector2(0.3f, -0.3f);
            else lineRenderer.Points[1] = new Vector2(n.GetComponent<RectTransform>().localPosition.x, n.GetComponent<RectTransform>().localPosition.y);
            linerenderers.Add(lineRenderer.gameObject);
        }
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
        if (isUknown())
            gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.40f);
        else if (startNode)
            return;
        else if (!selected)
            gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.20f);
        else if (selected)
            gameObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1.00f);     
    }


    //Attention lines will be repeated
    void OnGUI()
    {
        int linerenderer = 0;
        foreach(SkillTreeNode n in sucessors)
        {
            if(n.gameObject.activeSelf && !n.isUknown())
            {
                if (linerenderers != null)
                {
                    if (linerenderer < linerenderers.Count)
                    {
                        if (startNode)
                            linerenderers[linerenderer].GetComponent<UILineRenderer>().Points[0] = new Vector2(0.3f, -0.3f);
                        else linerenderers[linerenderer].GetComponent<UILineRenderer>().Points[0] = new Vector2(GetComponent<RectTransform>().localPosition.x, GetComponent<RectTransform>().localPosition.y);//new Vector2(GetComponent<RectTransform>().position.x, GetComponent<RectTransform>().position.y);
                        if (n.startNode)
                            linerenderers[linerenderer].GetComponent<UILineRenderer>().Points[1] = new Vector2(0.3f, -0.3f);
                        else linerenderers[linerenderer].GetComponent<UILineRenderer>().Points[1] = new Vector2(n.GetComponent<RectTransform>().localPosition.x, n.GetComponent<RectTransform>().localPosition.y);
                        linerenderer++;
                    }
                }

                /*GuiHelper.DrawLine(new Vector2(GetComponent<RectTransform>().position.x, Screen.height - GetComponent<RectTransform>().position.y), new Vector2(n.GetComponent<RectTransform>().position.x, Screen.height - n.GetComponent<RectTransform>().position.y), Color.white);
                Debug.DrawLine(
                    new Vector2(
                        GetComponent<RectTransform>().position.x,
                        GetComponent<RectTransform>().position.y),
                    new Vector2(
                        n.GetComponent<RectTransform>().position.x,
                        n.GetComponent<RectTransform>().position.y), Color.white);*/

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
        if (changeAbilityNode)
            return true;
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

    void checkAbilityNodeLimits()
    {
        if (!selected)
        {
            if (manager.cur_lim_points >= pointCost)
            {
                    selected = true;
                    manager.cur_lim_points -= pointCost;
                    manager.updatePointsText();
                    manager.predictChanges();
                    return;
            }
        }
        else
        {
                selected = false;
                manager.cur_lim_points += pointCost;
                manager.updatePointsText();
                manager.predictChanges();
                return;
            }
    }

    void checkIfLimits()
    {
        var type = typeof(SkillTreeManager); // Get type pointer
        FieldInfo[] fields = type.GetFields(); // Obtain all fields
        if(changeAbilityNode)
        {
            checkAbilityNodeLimits();
            return;
        }
        foreach (var field in fields) // Loop through fields
        {
            // is a float
            if (typeof(float).IsAssignableFrom(field.FieldType))
            {
                if (field.Name.CompareTo("cur_lim_" + variableBeingChanged) == 0 )
                {
                    if(!selected)
                    {
                        if (manager.cur_lim_points >= pointCost)
                        {
                            if (((float)field.GetValue(manager)) < GameManager.Instance.getStatVariable("lim_" + variableBeingChanged))
                            {
                                selected = true;
                                field.SetValue(manager, ((float)field.GetValue(manager)) + 1);
                                manager.cur_lim_points -= pointCost;
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
                            manager.cur_lim_points += pointCost;
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
        /*string info = information;= "Changing: " + variableBeingChanged + "\n"
                    + "With " + operation.ToString() + " " + changeWithValue;*/
        if(isUknown())
        {
            manager.displayInfo("Unknown node", 0, transform);
        }
        else manager.displayInfo(information, pointCost, transform);
    }

    public void PointerExit()
    {
        /*string info = information;= "Changing: " + variableBeingChanged + "\n"
                    + "With " + operation.ToString() + " " + changeWithValue;*/
        manager.hideInfoBox();
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

    public void deactivateLineRenderer()
    {
        foreach (GameObject o in linerenderers)
        {
            o.SetActive(false);
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
