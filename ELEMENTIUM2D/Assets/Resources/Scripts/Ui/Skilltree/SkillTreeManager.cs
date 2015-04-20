using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Includes;

public class SkillTreeManager : MonoBehaviour
{
    private Transform availablePoints;

    private Transform infobox;

    private Transform predictBox;

    [SerializeField]
    private List<SkillTreeNode> allNodeList = new List<SkillTreeNode>();

    // Node being pointed at.
    Transform current;

    // StartNode
    Transform startNode;

    // Current levels on the folowing stats in the skill tree
    public float cur_lim_moveSpeed = 0;
    public float cur_lim_moveInContactWithEnemy = 0;
    public float cur_lim_maxHealth = 0;
    public float cur_lim_damage = 0;
    public float cur_lim_defence = 0;
    public float cur_lim_waterResist = 0;
    public float cur_lim_earthResist = 0;
    public float cur_lim_fireResist = 0;
    public float cur_lim_damageTimer = 0;

    public float cur_lim_primary_neutral_level = 1;
    public float cur_lim_secondary_neutral_level = 0;
    public float cur_lim_terciary_neutral_level = 0;

    public float cur_lim_primary_earth_level = 0;
    public float cur_lim_secondary_earth_level = 0;
    public float cur_lim_terciary_earth_level = 0;

    public float cur_lim_primary_fire_level = 0;
    public float cur_lim_secondary_fire_level = 0;
    public float cur_lim_terciary_fire_level = 0;

    public float cur_lim_primary_water_level = 0;
    public float cur_lim_secondary_water_level = 0;
    public float cur_lim_terciary_water_level = 0;

    public float cur_lim_points;

    // Use this for initialization
    void Start()
    {
        availablePoints = GameObject.Find("Points").transform;
        cur_lim_points = GameManager.Instance.Stats.lim_points;
        availablePoints.GetComponent<Text>().text = "Available Points: " + cur_lim_points;

        infobox = GameObject.Find("InfoBox").transform;

        predictBox = GameObject.Find("PredictBox").transform;
        startNode = GameObject.Find("Parent").transform;

        GameManager.Instance.resetPlayerStats();
        checkDepth();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("UI_ENTER"))
        {
            UpdatePlayerStats();
            Application.LoadLevel("defaultScene");
        }
    }

    bool checkIfHasOneEnabled(SkillTreeNode node, bool checkForUnknown)
    {
        foreach (SkillTreeNode n in node.sucessors)
        {
            if(checkForUnknown)
            {
                if (n.gameObject.activeSelf && !n.isUknown())
                    return true;
            }
            else
            {
                if (n.gameObject.activeSelf)
                    return true;
            }

        }
        return false;
    }

    bool checkIfHasOneSelected(SkillTreeNode node)
    {
        foreach (SkillTreeNode n in node.sucessors)
        {
            if (n.selected)
                return true;
        }
        return false;
    }

    void iterateAndAddUnknowns(List<SkillTreeNode> nodeList)
    {
        List<SkillTreeNode> nextNodeList = new List<SkillTreeNode>();
        foreach (SkillTreeNode n in nodeList)
        {
            if (!n.startNode )
            {
                if (!n.isSearched())
                {
                    if (!checkIfHasOneEnabled(n, true))
                    {
                        n.transform.gameObject.SetActive(false);
                    }
                    else
                    {
                        if(!n.transform.gameObject.activeSelf)
                        {
                            n.transform.gameObject.SetActive(true);
                            n.setUnknown();
                        }
                    }
                    n.setSearched();
                    foreach (SkillTreeNode ns in n.sucessors)
                    {
                        if (!ns.isSearched())
                        {
                            nextNodeList.Add(ns);
                        }
                    }
                }
            }
        }
        if (nextNodeList.Count > 0)
            iterateAndAddUnknowns(nextNodeList);
    }

    void iterateAndCheckDepth(int depth, List<SkillTreeNode> nodeList)
    {
        bool depthIncrease = false;
        List<SkillTreeNode> nextNodeList = new List<SkillTreeNode>();
        foreach (SkillTreeNode n in nodeList)
        {
            if(!n.startNode)
            {
                // Checks if the node is at the correct depth if its limit is ok and if its father is disabled
                if (depth > GameManager.Instance.Stats.depth || !n.checkIndividualLimit())
                {
                    n.transform.gameObject.SetActive(false);
                }
                if (!n.isSearched())
                {
                    n.setSearched();
                    foreach(SkillTreeNode ns in n.sucessors)
                    {
                        if (!ns.isSearched())
                        {
                            nextNodeList.Add(ns);
                            depthIncrease = true;
                        }
                    }
                }  
            }    
        }
        if (depthIncrease)
            depth++;
        if (nextNodeList.Count > 0 )
            iterateAndCheckDepth(depth, nextNodeList);
    }

    void checkDepth()
    {
        if (startNode == null)
        {
            startNode = GameObject.Find("Parent").transform;
        }
        SkillTreeNode node = startNode.GetComponent<SkillTreeNode>();
        iterateAndCheckDepth(1, node.sucessors);
        setAllUnsearched(false);
        iterateAndAddUnknowns(node.sucessors);
        setAllUnsearched(false);
    }

    void iterateAndSet(List<SkillTreeNode> nodeList)
    {
        List<SkillTreeNode> nextNodeList = new List<SkillTreeNode>();
        foreach (SkillTreeNode n in nodeList)
        {
            if (n.father != null)
                n.transform.SetParent(n.father.transform);
            if(!n.isSearched())
            {
                nextNodeList.AddRange(n.sucessors);
                n.setSearched();
            }
        }
        if (nextNodeList.Count > 0)
            iterateAndSet(nextNodeList);
    }

    public void setupHiearchyBasedOfCode()
    {
        if (startNode == null)
        {
            startNode = GameObject.Find("Parent").transform;
        }
        SkillTreeNode node = startNode.GetComponent<SkillTreeNode>();
        iterateAndSet(node.sucessors);
        setAllUnsearched(false);
    }

    bool findNode(List<SkillTreeNode> nodeList, int instanceToIgnore, int instanceToFind)
    {
        bool found = false;
        List<SkillTreeNode> nextNodeList = new List<SkillTreeNode>();
        foreach (SkillTreeNode n in nodeList)
        {
            if(!n.isSearched())
            {
                if (n.transform.GetInstanceID() == instanceToFind)
                    found = true;
                if (n.transform.GetInstanceID() != instanceToIgnore)
                {
                    if (n.selected)
                    {
                        foreach (SkillTreeNode sn in n.sucessors)
                        {
                            if (!sn.startNode && !sn.isSearched() && sn.isSearchable())
                                nextNodeList.Add(sn);
                        }
                    }
                }
            }
            n.setSearched();
        }
        if (found)
            return true;
        if (nextNodeList.Count > 0)
            return findNode(nextNodeList, instanceToIgnore, instanceToFind);
        return false;
    }

    public bool checkPath(SkillTreeNode currentNode, SkillTreeNode nodeToCheck)
    {
        setAllUnsearched(false);
        startNode = GameObject.Find("Parent").transform;
        bool retVal = findNode(startNode.GetComponent<SkillTreeNode>().sucessors, currentNode.transform.GetInstanceID(), nodeToCheck.transform.GetInstanceID());
        setAllUnsearched(false);
        return retVal;
    }

    void iterate(List<SkillTreeNode> nodeList)
    {
        List<SkillTreeNode> nextNodeList = new List<SkillTreeNode>();
        foreach (SkillTreeNode n in nodeList)
        {
            if (n.isSearchable())
            {
                GameManager.Instance.changeStatVariable(n.getVariableBeingChanged(), n.getValueBeingChanged(), n.operation);
                n.setSearched();
            }
            foreach (SkillTreeNode sn in n.sucessors)
            {
                if (sn.isSearchable())
                    nextNodeList.AddRange(n.sucessors);
            }
        }
        if (nextNodeList.Count > 0)
            iterate(nextNodeList);
    }

    void setAllUnsearched(bool unparent)
    {
        foreach(SkillTreeNode n in allNodeList)
        {
            if (unparent)
                n.transform.SetParent(startNode);
            n.setUnsearched();
        }
    }

    void iterateChildren(List<SkillTreeNode> nodeList)
    {
        List<SkillTreeNode> nextNodeList = new List<SkillTreeNode>();
        foreach (SkillTreeNode n in nodeList)
        {
            if(!n.startNode)
            {
                n.setupHiearchy();
                allNodeList.Add(n);
                //n.transform.parent = startNode;
                if (!n.isSearched())
                {
                    nextNodeList.AddRange(n.sucessors);
                    n.setSearched();
                }
            }
        }  
        if (nextNodeList.Count > 0)
            iterateChildren(nextNodeList);
    }

    public void setupHiearchy()
    {
        allNodeList.Clear();
        startNode = GameObject.Find("Parent").transform;
        iterateChildren(startNode.GetComponent<SkillTreeNode>().sucessors);
        setAllUnsearched(true);
    }

    public void updatePointsText()
    {
        availablePoints.GetComponent<Text>().text = "Available Points: " + cur_lim_points;
    }

    public void displayInfo(string info)
    {

        infobox.GetComponent<Text>().text = "Info:" + "\n" + info;
    }

    //ATTENTION CURRENTLY ITS ONLY A DUMP OF THE NUMBER OF POINTS USED
    // AND USE REFLECTION NEXT TIME TO ITERATE ALL VARIABLES
    public void predictChanges()
    {
        predictBox.GetComponent<Text>().text = "Changes: " + "\n\n"
            + "MoveSpeed: " + cur_lim_moveSpeed + "\n\n"
            + "MoveInContactWithEnemy: " + cur_lim_moveInContactWithEnemy + "\n\n"
            + "MaxHealth: " + cur_lim_maxHealth + "\n\n"
            + "Damage: " + cur_lim_damage + "\n\n"
            + "Defence: " + cur_lim_defence + "\n\n"
            + "WaterResist: " + cur_lim_waterResist +  "\n\n"
            + "EarthResist: " + cur_lim_earthResist +  "\n\n"
            + "FireResist: " + cur_lim_fireResist +  "\n\n"
            + "DamageTimer: " + cur_lim_damageTimer + "\n\n"

            + "Neutral level: " + cur_lim_primary_neutral_level + "/" + GameManager.Instance.Stats.lim_primary_neutral_level + "\n\n"
            + "Secondary Neutral level: " + cur_lim_secondary_neutral_level + "/" + GameManager.Instance.Stats.lim_secondary_neutral_level + "\n\n"
            + "Terciary Neutral level: " + cur_lim_terciary_neutral_level + "/" + GameManager.Instance.Stats.lim_terciary_neutral_level + "\n\n"

            + "Primary Earth level: " + cur_lim_primary_earth_level + "/" + GameManager.Instance.Stats.lim_primary_earth_level + "\n\n"
            + "Secondary Earth level: " + cur_lim_secondary_earth_level + "/" + GameManager.Instance.Stats.lim_secondary_earth_level + "\n\n"
            + "Terciary Earth level: " + cur_lim_terciary_earth_level + "/" + GameManager.Instance.Stats.lim_terciary_earth_level + "\n\n"

            + "Primary Fire level: " + cur_lim_primary_fire_level + "/" + GameManager.Instance.Stats.lim_primary_fire_level + "\n\n"
            + "Secondary Fire level: " + cur_lim_secondary_fire_level + "/" + GameManager.Instance.Stats.lim_secondary_fire_level + "\n\n"
            + "Terciary Fire level: " + cur_lim_terciary_fire_level + "/" + GameManager.Instance.Stats.lim_terciary_fire_level + "\n\n"

            + "Primary Water level: " + cur_lim_primary_water_level + "/" + GameManager.Instance.Stats.lim_primary_water_level + "\n\n"
            + "Secondary Water level: " + cur_lim_secondary_water_level + "/" + GameManager.Instance.Stats.lim_secondary_water_level + "\n\n"
            + "Terciary Water level: " + cur_lim_terciary_water_level + "/" + GameManager.Instance.Stats.lim_terciary_water_level + "\n\n";
    }

    public void UpdatePlayerStats()
    {
        SkillTreeNode node = startNode.GetComponent<SkillTreeNode>();
        iterate(node.sucessors);
        Debug.Log("Finished updating game variables");
    }
}
