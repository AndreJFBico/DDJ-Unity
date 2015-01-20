using UnityEngine;
using System.Collections;

public class FireElement: MonoBehaviour {

    private Transform barrelEnd;
    private Transform rotator;
    private GameObject bulletPrefab;

    private bool canMain = true;

	// Use this for initialization
	void Start () {
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

        bulletPrefab = (GameObject) Resources.Load("Prefabs/Fireball");
	}
	
    private void resetMain()
    {
        canMain = true;
    }

    public void fireMain()
    {
        if (canMain)
        {
            canMain = false;
            Invoke("resetMain", 1.0f);
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, barrelEnd.position, rotator.rotation);
            bullet.transform.Rotate(new Vector3(90, 0,90));
            bullet.rigidbody.AddForce(barrelEnd.transform.forward * 100);
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
