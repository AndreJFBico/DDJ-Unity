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

    void iterateAndCheckDepth(int depth, List<SkillTreeNode> nodeList)
    {
        List<SkillTreeNode> nextNodeList = new List<SkillTreeNode>();
        foreach (SkillTreeNode n in nodeList)
        {
            // Checks if the node is at the correct depth if its limit is ok and if its father is disabled
            if (depth > GameManager.Instance.Stats.depth || !n.checkIndividualLimit() || !n.fatherNode.gameObject.activeSelf)
            {
                if (n.fatherNode.gameObject.activeSelf && !n.fatherNode.isUknown())
                {
                    n.setUnknown();
                    //n.transform.gameObject.SetActive(false);
                }          
                else
                {
                    n.transform.gameObject.SetActive(false);
                }
            }
            nextNodeList.AddRange(n.sucessors);                 
        }
        if (nextNodeList.Count > 0 )
            iterateAndCheckDepth(depth + 1, nextNodeList);
    }

    void checkDepth()
    {
        if (startNode == null)
        {
            startNode = GameObject.Find("Parent").transform;
        }
        SkillTreeNode node = startNode.GetComponent<SkillTreeNode>();
        iterateAndCheckDepth(1, node.sucessors);
    }

    void iterateAndSet(List<SkillTreeNode> nodeList)
    {
        List<SkillTreeNode> nextNodeList = new List<SkillTreeNode>();
        foreach (SkillTreeNode n in nodeList)
        {
            n.transform.parent = n.fatherNode.transform;
            nextNodeList.AddRange(n.sucessors);
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

    void iterateChildren(List<SkillTreeNode> nodeList)
    {
        List<SkillTreeNode> nextNodeList = new List<SkillTreeNode>();
        foreach (SkillTreeNode n in nodeList)
        {
            n.setupHiearchy();
            n.transform.parent = startNode;
            nextNodeList.AddRange(n.sucessors);
        }
        if (nextNodeList.Count > 0)
            iterateChildren(nextNodeList);
    }

    public void setupHiearchy()
    {
        startNode = GameObject.Find("Parent").transform;
        iterateChildren(startNode.GetComponent<SkillTreeNode>().sucessors);
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
