using UnityEngine;
using System.Collections;
using Includes;

public class Lava : BreakableWall {

    private bool affected;

    // Use this for initialization
    void Start()
    {
        maxDurability = 20;
        durability = maxDurability;
    }

    void activateParticleSystem()
    {
        if (particleSystem != null)
        {
            particleSystem.gameObject.SetActive(true);
        }
        else
        {
            if (durability <= maxDurability / 4)
            {
                transform.localScale = new Vector3(0.33f, 0.33f, 0.33f);
            }
            else if (durability <= maxDurability / 2)
            {
                transform.localScale = new Vector3(0.66f, 0.66f, 0.66f);
            }
        }
    }

    public override void dealWithProjectile(Elements projType, float damage)
    {
        switch (type)
        {
            case BreakableWalls.NEUTRAL:
                switch (projType)
                {
                    case Elements.NEUTRAL:
                        durability -= damage;
                        activateParticleSystem();
                        break;
                    default:
                        break;
                }
                break;
            case BreakableWalls.FIRE:
                switch (projType)
                {
                    case Elements.WATER:
                        durability -= damage;
                        activateParticleSystem();
                        affected = true;
                        StartCoroutine(DealTemporaryDamage(damage, 0.8f));
                        break;
                    default:
                        break;
                }
                break;
            case BreakableWalls.EARTH:
                switch (projType)
                {
                    case Elements.FIRE:
                        durability -= damage;
                        activateParticleSystem();
                        break;
                    default:
                        break;
                }
                break;
            case BreakableWalls.FROST:
                switch (projType)
                {
                    case Elements.EARTH:
                        durability -= damage;
                        activateParticleSystem();
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }

        if (durability <= 0)
            turnIntoColdLava();
    }

    IEnumerator DealTemporaryDamage(float damage, float time)
    {
        while (true)
        {
            durability -= damage;
            if (durability <= 0)
                turnIntoColdLava();
            yield return new WaitForSeconds(time);
        }
    }

    void turnIntoColdLava()
    {
        transform.FindChild("Sprite").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Environment/Lava/lava_cold");
        transform.gameObject.GetComponent<SphereCollider>().enabled = false;
        enabled = false;
    }
}
