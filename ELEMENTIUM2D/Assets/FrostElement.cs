using UnityEngine;
using System.Collections;

public class FrostElement : MonoBehaviour {

	private Transform barrelEnd;
    private Transform rotator;
    private GameObject bulletPrefab;

    private bool canMain = true;

    //Time between attacks
    private float attackSpeed = 0.5f;

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

        bulletPrefab = (GameObject) Resources.Load("Prefabs/FrostBolt");
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
            Invoke("resetMain", attackSpeed);
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, barrelEnd.position, rotator.rotation);
            bullet.transform.Rotate(new Vector3(90, 0,90));
            bullet.rigidbody.AddForce(barrelEnd.transform.forward * 75);
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}

