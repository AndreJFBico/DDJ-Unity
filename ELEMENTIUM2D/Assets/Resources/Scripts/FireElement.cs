using UnityEngine;
using System.Collections;

public class FireElement: ShootElement {

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

        gunBlast1 = GameObject.Find("FireBlast1");
        gunBlast2 = GameObject.Find("FireBlast2");
        gunBlast1.SetActive(false);
        gunBlast2.SetActive(false);

        bulletPrefab = (GameObject) Resources.Load("Prefabs/Fireball");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
