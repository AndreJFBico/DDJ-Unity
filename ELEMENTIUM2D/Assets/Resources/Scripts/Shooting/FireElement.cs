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

        //bulletPrefab = (GameObject) Resources.Load("Prefabs/Projectiles/Fireball");

        //attackSpeed = 0.5f;
	}

    protected void fire()
    {
        gunBlast1.SetActive(true);
        gunBlast2.SetActive(true);
        Invoke("deactivateBlast", 0.1f);
        OilPuddleManager.Instance.addOilPuddle((GameObject)Instantiate(OilPuddleManager.Instance.OilPuddle, barrelEnd.position, OilPuddleManager.Instance.OilPuddle.transform.rotation));        
    }

    public void fireSecondary()
    {
        if (canSecondary)
        {
            canSecondary = false;
            Invoke("resetSecondary", OilPuddleManager.Instance.internalCooldown());
            fire();
        }
    }

}
