using UnityEngine;
using System.Collections;

public class FrostElement : ShootElement {

	// Use this for initialization
    void Start()
    {
        Transform[] aux = GetComponentsInChildren<Transform>();
        foreach (Transform item in aux)
        {
            if (item.name == "BarrelEnd")
            {
                barrelEnd = item;
            }
            if (item.name == "Rotator")
            {
                rotator = item;
            }
        }

        gunBlast1 = GameObject.Find("FrostBlast1");
        gunBlast2 = GameObject.Find("FrostBlast2");
        gunBlast1.SetActive(false);
        gunBlast2.SetActive(false);

        bulletPrefab = (GameObject) Resources.Load("Prefabs/Projectiles/FrostBolt");

        //attackSpeed = 0.25f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

