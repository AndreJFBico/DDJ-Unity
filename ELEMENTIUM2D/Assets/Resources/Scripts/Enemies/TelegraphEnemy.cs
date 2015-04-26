using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Includes;

public class TelegraphEnemy : EnemyScript {

    //List of telegraph prefabs
    public List<Transform> telegraphs;
    Transform targetedEnemy;

    protected Transform activeWeapon;

    public Transform left;
    public Transform left_firepoint;
    public Transform right;
    public Transform right_firepoint;

    private Transform currentFireTransform;

    // Use this for initialization
    protected override void Awake()
    {
        type = Elements.NEUTRAL;
        maxHealth = EnemyStats.BasicNeutral.maxHealth;
        health = maxHealth;
        damage = EnemyStats.BasicNeutral.damage;
        defence = EnemyStats.BasicNeutral.defence;
        waterResist = EnemyStats.BasicNeutral.waterResist;
        earthResist = EnemyStats.BasicNeutral.earthResist;
        fireResist = EnemyStats.BasicNeutral.fireResist;
        activeWeapon = left;
        //lr = gameObject.AddComponent<LineRenderer>();
        //lr.SetWidth(0.01f, 0.01f);
        //lr.SetVertexCount(2);
        base.Awake();
    }

    bool hasEnemyTarget()
    {
        return targetedEnemy != null;
    }

    void OnEnable()
    {
        StartCoroutine("telegraphControl");
    }

    Transform getBestTelegraph()
    {
        Transform bestTelegraph = null;
        Transform worstTelegraph = null;
        float bestPriority = float.MaxValue;
        float worsePriority = 0;
        foreach (Transform t in telegraphs)
        {
            Telegraph telegraph = t.GetComponent<Telegraph>();
            if(telegraph.priority < bestPriority)
            {
                bestTelegraph = t;
                bestPriority = telegraph.priority;
            }
            if(telegraph.priority > worsePriority)
            {
                worstTelegraph = t;
                worsePriority = telegraph.priority;
            }
        }
        bestTelegraph.GetComponent<Telegraph>().priority = worsePriority;
        if(worstTelegraph.GetComponent<Telegraph>().priority > 0)
        {
            worstTelegraph.GetComponent<Telegraph>().priority -= 1;
        }

        return bestTelegraph;
    }

    protected IEnumerator telegraphControl()
    {
        while (true)
        {
            if (pathAgent.hasTarget())
            {
                Transform t = getBestTelegraph();
                t.GetComponent<Telegraph>().init(pathAgent.target, currentFireTransform);
                t.GetComponent<Telegraph>().setupMotion();
                yield return new WaitForSeconds(t.GetComponent<Telegraph>().duration);
            }
            else yield return new WaitForSeconds(0.2f);
        }
    }

    protected override void LateUpdate()
    {
        if (pathAgent.hasTarget())
        {
            if (pathAgent.target.position.x >= transform.position.x)
            {
                left.gameObject.SetActive(true);
                activeWeapon = left;
                right.gameObject.SetActive(false);
                currentFireTransform = left_firepoint;
            }
            else
            {
                right.gameObject.SetActive(true);
                activeWeapon = right;
                left.gameObject.SetActive(false);
                currentFireTransform = right_firepoint;
            }
            activeWeapon.LookAt(pathAgent.target.position);
        }
        else
        {
            activeWeapon.rotation = Quaternion.identity;
        }
        base.LateUpdate();
    }
}
