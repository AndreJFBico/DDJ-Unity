using UnityEngine;
using System.Collections;

public class OilBarrel : BreakableProp {

    public GameObject barrel;
    public GameObject puddle;

	// Use this for initialization
	void Start () {
        maxDurability = 1;
        durability = maxDurability;
        puddle.SetActive(false);
	}

    protected override void dealWithDeath()
    {
        barrel.SetActive(false);
        puddle.SetActive(true);
        GetComponent<CapsuleCollider>().enabled = false;
        Invoke("Eliminate", 15f);
        //GetComponent<BoxCollider>().enabled = true;
    }

    private void Eliminate()
    {
        Destroy(gameObject);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
