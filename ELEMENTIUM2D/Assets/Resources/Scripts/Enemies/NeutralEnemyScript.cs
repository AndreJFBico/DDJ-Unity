using UnityEngine;
using System.Collections;
using Includes;

public class NeutralEnemyScript : EnemyScript
{
    protected LineRenderer lr;

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
        //lr = gameObject.AddComponent<LineRenderer>();
        //lr.SetWidth(0.01f, 0.01f);
        //lr.SetVertexCount(2);
        base.Awake();
    }

    void alignLineRenderer()
    {
        //lr.SetPosition(0, transform.position);
        //lr.SetPosition(1, pathAgent.target.transform.position);
    }

    // Update is called once per frame
    /*protected override void Update()
    {
        if (pathAgent.target != null)
        {
            alignLineRenderer();
        }
        base.Update();
    }*/

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    /*protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }*/

    // Health bar
    /*protected override void OnGUI()
    {
        base.OnGUI();
    }*/
}