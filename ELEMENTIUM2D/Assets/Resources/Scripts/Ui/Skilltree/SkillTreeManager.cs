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
    private Transform neutralBox;
    private Transform earthBox;
    private Transform fireBox;
    private Transform frostBox;

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
    public float cur_lim_attackSpeed = 0;
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
        infobox.gameObject.SetActive(false);

        predictBox = GameObject.Find("PredictBox").transform;
        neutralBox = GameObject.Find("NeutralBox").transform;
        earthBox = GameObject.Find("EarthBox").transform;
        fireBox = GameObject.Find("FireBox").transform;
        frostBox = GameObject.Find("FrostBox").transform;
        startNode = GameObject.Find("Parent").transform;

        GameManager.Instance.resetAbilityStats();
        GameManager.Instance.resetPlayerStats();
        checkDepth();
        predictChanges();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("UI_ENTER"))
        {
            UpdatePlayerStats();
            Application.LoadLevel("defaultScene");
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            cur_lim_points += 1;
        }

    }

    bool checkIfHasOneEnabled(SkillTreeNode node, bool checkForUnknown)
    {
        foreach (SkillTreeNode n in node.sucessors)
        {
            if(checkForUnknown)
            {
                if(n.gameObject.activeSelf)
                {
                    if(!n.isUknown())
                    {
                        return true;
                    }
                }
            }
            else
            {
                if (n.gameObject.activeSelf)
                {
                    return true;
                }    
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
                        n.deactivateLineRenderer();
                        n.transform.gameObject.SetActive(false);
                    }
                    else
                    {
                        if(!n.transform.gameObject.activeSelf)
                        {
                            n.transform.gameObject.SetActive(true);
                            n.deactivateLineRenderer();
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

    public void clearAllLineRenderers()
    {
        foreach(SkillTreeNode n in allNodeList)
        {
            n.clearLineRenderers();
        }
    }

    public void setupHiearchyBasedOfCode()
    {
        if (startNode == null)
        {
            startNode = GameObject.Find("Parent").transform;
        }
        SkillTreeNode node = startNode.GetComponent<SkillTreeNode>();
        iterateAndSet(node.sucessors);

        clearAllLineRenderers();
        setAllUnsearched(false);
    }

    bool checkStraightPath(SkillTreeNode sNode, SkillTreeNode endNode)
    {
        setAllUnsearched(false);
        if (sNode.transform.GetInstanceID() == endNode.transform.GetInstanceID())
            return true;
        bool retVal = findNode(sNode.sucessors, endNode.transform.GetInstanceID());
        setAllUnsearched(false);
        return retVal;
    }

    bool findNode(List<SkillTreeNode> nodeList, int instanceToFind)
    {
        bool found = false;
        List<SkillTreeNode> nextNodeList = new List<SkillTreeNode>();
        foreach (SkillTreeNode n in nodeList)
        {
            if (!n.isSearched())
            {
                if (n.transform.GetInstanceID() == instanceToFind)
                    return true;
                if (n.selected)
                {
                    foreach (SkillTreeNode sn in n.sucessors)
                    {
                        if (!sn.startNode && !sn.isSearched() && sn.isSearchable())
                            nextNodeList.Add(sn);
                    }
                }
            }
            n.setSearched();
        }
        if (nextNodeList.Count > 0)
            return findNode(nextNodeList, instanceToFind);
        return false;
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
                if(n.changeAbilityNode)
                {
                    GameManager.Instance.changeAbilityVariable(n.getVariableBeingChanged(), n.getValueBeingChanged(), n.operation);
                }
                else
                {
                    GameManager.Instance.changeStatVariable(n.getVariableBeingChanged(), n.getValueBeingChanged(), n.operation);
                }
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

    void generateLineRenderers()
    {
        startNode.GetComponent<SkillTreeNode>().generateLineRenderers();
        foreach(SkillTreeNode n in allNodeList)
        {
            n.generateLineRenderers();
        }
    }

    public void setupHiearchy()
    {
        allNodeList.Clear();
        startNode = GameObject.Find("Parent").transform;
        iterateChildren(startNode.GetComponent<SkillTreeNode>().sucessors);
        setAllUnsearched(true);
        generateLineRenderers();
    }

    public void updatePointsText()
    {
        availablePoints.GetComponent<Text>().text = "Available Points: " + cur_lim_points;
    }

    public void hideInfoBox()
    {
        infobox.gameObject.SetActive(false);
    }

    public void displayInfo(string info, int pointCost, Transform node)
    {
        infobox.gameObject.SetActive(true);
        infobox.transform.position = node.transform.position;
        if(pointCost == 0) //Unknown cost
            infobox.GetComponentInChildren<Text>().text = "Info:" + "\n" + info;
        else
            infobox.GetComponentInChildren<Text>().text = "Info:" + "\n" + info + "\n" + "Node cost: " + pointCost;
    }

    //ATTENTION CURRENTLY ITS ONLY A DUMP OF THE NUMBER OF POINTS USED
    // AND USE REFLECTION NEXT TIME TO ITERATE ALL VARIABLES
    public void predictChanges()
    {
        UpdatePlayerStats();
        setAllUnsearched(false);
        Text text= predictBox.GetComponent<Text>(); 
        text.text = "Main player stats: " + "\n\n"
            + "MoveSpeed: " + GameManager.Instance.Stats.moveSpeed + "\n\n"
            + "MaxHealth: " + GameManager.Instance.Stats.maxHealth + "\n\n"
            + "Damage: " + GameManager.Instance.Stats.damage + "\n\n"
            + "Defence: " + GameManager.Instance.Stats.defence + "\n\n"
            + "WaterResist: " + GameManager.Instance.Stats.waterResist + "%" + "\n\n"
            + "EarthResist: " + GameManager.Instance.Stats.earthResist + "%" + "\n\n"
            + "FireResist: " + GameManager.Instance.Stats.fireResist + "%" + "\n\n";

        #region NEUTRAL
        neutralBox.GetComponent<Text>().text = "Neutral Stats\n";
        bool unlockedN = false;
            if (GameManager.Instance.Stats.lim_primary_neutral_level == 1)
            {
                neutralBox.GetComponent<Text>().text += 
                    "Neutral ability 1" + "\n"
                    + "\t" + "Damage:\t" + AbilityStats.Neutral.ability1.Damage + "\n"
                    + "\t" + "AttackSpeed:\t" + AbilityStats.Neutral.ability1.AttackSpeed + "\n";
                unlockedN = true;
            }
            if (GameManager.Instance.Stats.lim_secondary_neutral_level == 1)
            {
                neutralBox.GetComponent<Text>().text +=
                    "Neutral homing missiles" + "\n"
                    + "\t" + "Damage:\t" + AbilityStats.Neutral.ability2.Damage + "\n"
                    + "\t" + "Cooldown:\t" + AbilityStats.Neutral.ability2.AttackSpeed + "\n";
                unlockedN = true;
            }
            if (GameManager.Instance.Stats.lim_terciary_neutral_level == 1)
            {
                neutralBox.GetComponent<Text>().text +=
                    "Neutral splitting projectile" + "\n"
                    + "\t" + "Damage:\t" + AbilityStats.Neutral.ability3.Damage + "\n"
                    + "\t" + "AttackSpeed:\t" + AbilityStats.Neutral.ability3.AttackSpeed + "\n";
                unlockedN = true;
            }
        if(!unlockedN)
            neutralBox.GetComponent<Text>().text +="No abilities have been unlocked";
        #endregion

        #region FIRE
            fireBox.GetComponent<Text>().text = "Fire Stats\n";
            bool unlockedF = false;
            if (GameManager.Instance.Stats.lim_primary_fire_level == 1)
            {
                fireBox.GetComponent<Text>().text +=
                    "Fireball" + "\n"
                    + "\t" + "Damage:\t" + AbilityStats.Fire.ability1.Damage + "\n"
                    + "\t" + "AttackSpeed:\t" + AbilityStats.Fire.ability1.AttackSpeed + "\n";
                unlockedF = true;
            }
            if (GameManager.Instance.Stats.lim_secondary_fire_level == 1)
            {
                fireBox.GetComponent<Text>().text +=
                    "OillPuddle" + "\n"
                    + "\t" + "Cooldown:\t" + AbilityStats.Fire.ability2.AttackSpeed + "\n";
                unlockedF = true;
            }
            if (GameManager.Instance.Stats.lim_terciary_fire_level == 1)
            {
                fireBox.GetComponent<Text>().text +=
                    "Heal spell" + "\n"
                    + "\t" + "Healing:\t" + AbilityStats.Fire.ability3.Damage + "\n"
                    + "\t" + "Cooldown:\t" + AbilityStats.Fire.ability3.AttackSpeed + "\n";
                unlockedF = true;
            }
            if (!unlockedF)
                fireBox.GetComponent<Text>().text += "No abilities have been unlocked";
        #endregion

        #region EARTH
            earthBox.GetComponent<Text>().text = "Earth Stats\n";
            bool unlockedE = false;
            if (GameManager.Instance.Stats.lim_primary_earth_level == 1)
            {
                earthBox.GetComponent<Text>().text +=
                    "Boulder" + "\n"
                    + "\t" + "Damage:\t" + AbilityStats.Earth.ability1.Damage + "\n"
                    + "\t" + "AttackSpeed:\t" + AbilityStats.Earth.ability1.AttackSpeed + "\n";
                unlockedE = true;
            }
            if (GameManager.Instance.Stats.lim_secondary_earth_level == 1)
            {
                earthBox.GetComponent<Text>().text +=
                    "Rock Shield" + "\n"
                    + "\t" + "Duration:\t" + AbilityStats.Earth.ability2.abilityTimer + "\n"
                    + "\t" + "Cooldown:\t" + AbilityStats.Earth.ability2.AttackSpeed + "\n";
                unlockedE = true;
            }
            if (GameManager.Instance.Stats.lim_terciary_earth_level == 1)
            {
                earthBox.GetComponent<Text>().text +=
                    "Stun projectile" + "\n"
                    + "\t" + "Damage:\t" + AbilityStats.Earth.ability3.Damage + "\n"
                    + "\t" + "AttackSpeed:\t" + AbilityStats.Earth.ability3.AttackSpeed + "\n";
                unlockedE = true;
            }
        if(!unlockedE)
            earthBox.GetComponent<Text>().text += "No abilities have been unlocked";
        #endregion

        #region WATER
        frostBox.GetComponent<Text>().text = "Water Stats\n";
        bool unlockedW = false;
            if (GameManager.Instance.Stats.lim_primary_water_level == 1)
            {
                frostBox.GetComponent<Text>().text +=
                    "Frost bouncing projectile" + "\n"
                    + "\t" + "Damage:\t" + AbilityStats.Frost.ability1.Damage + "\n"
                    + "\t" + "AttackSpeed:\t" + AbilityStats.Frost.ability1.AttackSpeed + "\n";
                unlockedW = true;
            }
            if (GameManager.Instance.Stats.lim_secondary_water_level == 1)
            {
                frostBox.GetComponent<Text>().text +=
                    "Water burst" + "\n"
                    + "\t" + "Damage:\t" + AbilityStats.Frost.ability2.Damage + "\n"
                    + "\t" + "AttackSpeed:\t" + AbilityStats.Frost.ability2.AttackSpeed + "\n";
                unlockedW = true;
            }
            if (GameManager.Instance.Stats.lim_terciary_water_level == 1)
            {
                frostBox.GetComponent<Text>().text +=
                    "Ice Nova" + "\n"
                    //+ "\t" + "Damage:" + AbilityStats.Frost.ability3.damage + "\n"
                    + "\t" + "Cooldown:\t" + AbilityStats.Frost.ability3.AttackSpeed + "\n";
                unlockedW = true;
            }
            if (!unlockedW)
                frostBox.GetComponent<Text>().text += "No abilities have been unlocked";
        #endregion



        /*+ "Neutral level: " + cur_lim_primary_neutral_level + "/" + GameManager.Instance.Stats.lim_primary_neutral_level + "\n\n"
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
            + "Terciary Water level: " + cur_lim_terciary_water_level + "/" + GameManager.Instance.Stats.lim_terciary_water_level + "\n\n";*/
        GameManager.Instance.resetPlayerStats();
        GameManager.Instance.resetAbilityStats();
    }

    public void UpdatePlayerStats()
    {
        SkillTreeNode node = startNode.GetComponent<SkillTreeNode>();
        iterate(node.sucessors);
        Debug.Log("Finished updating game variables");
    }

    void OnApplicationQuit()
    {
        LoggingManager.Instance.wrapUp();
    }
}
