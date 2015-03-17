using UnityEngine;
using System.Collections;
using Includes;

public class IceWall : ElementalyModifiable {

    private float health;
    private float maxHealth;
    public GameObject waterPuddle;

    // Use this for initialization
    void Start()
    {
        maxHealth = 5;
        health = maxHealth;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.CompareTo("Enemy") == 0)
        {
        }
    }

    public override void dealWithProjectile(Elements type)
    {
        if (type == Elements.EARTH)
        {
            health--;
            if (health <= 0)
            {
                Destroy(transform.parent.gameObject);
            }
        }

        if (type == Elements.FIRE)
        {
            resetValues();
            waterPuddle.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void resetValues()
    {
        health = 5;
    }
}
