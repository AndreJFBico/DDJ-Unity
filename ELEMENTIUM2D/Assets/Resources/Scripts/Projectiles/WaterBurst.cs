using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Includes;

public class WaterBurst : AbilityBehaviour
{

    private GameObject cs;
    private GameObject iceBlock;
    private List<GameObject> waterProjectiles;
    protected bool iced = false;
    

    protected override void Awake()
    {
        waterProjectiles = new List<GameObject>();
        cs = Resources.Load(AbilityStats.Frost.ability2.childSprite) as GameObject;
        explosion = Resources.Load("Prefabs/Explosions/frostExplosion") as GameObject;
        iceBlock = Resources.Load("Prefabs/Environment/IceWall") as GameObject;
    }

    protected override void Start()
    {
        base.Start();
        damage = 0;
        type = Elements.FROST;
    }

    public void handleCollision(Collision collider)
    {
        if(!iced)
        {
            iced = true;
            Vector3 averagedPosition = new Vector3(0.0f, 0.0f, 0.0f);
            int number = waterProjectiles.Count;
            // For the moment we dont use the collider for anything
            foreach (GameObject wp in waterProjectiles)
            {
                averagedPosition += wp.transform.position;
                Destroy(wp);
            }
            averagedPosition = averagedPosition / number;
            Instantiate(iceBlock, averagedPosition, Quaternion.identity);          
        }
        Destroy(gameObject);
    }

    public override void initiate(GameObject startingObject)
    {
        for (int i = 0; i < AbilityStats.Frost.ability2.child_projectile_number; i++ )
        {
            GameObject projectile = Instantiate(cs, transform.position, transform.rotation) as GameObject;
            projectile.GetComponent<AbilityBehaviour>().initiate(this.gameObject);
            projectile.GetComponent<WaterSpray>().waterBurst = this;
            projectile.transform.parent = transform;
            waterProjectiles.Add(projectile);
        }
    }
}
