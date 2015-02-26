using UnityEngine;
using System.Collections;

public class ShootElement: MonoBehaviour {

    protected Transform barrelEnd;
    protected Transform rotator;
    protected GameObject bulletPrefab;

    protected GameObject gunBlast1;
    protected GameObject gunBlast2;

    private bool canMain = true;

    //Time between attacks
    protected float attackSpeed = 0.1f;

	// Use this for initialization
    void Start()
    {
	}
	
    private void resetMain()
    {
        canMain = true;
    }

    private void deactivateBlast()
    {
        gunBlast1.SetActive(false);
        gunBlast2.SetActive(false);
    }

    public void fireMain()
    {
        if (canMain)
        {
            canMain = false;
            Invoke("resetMain", attackSpeed);
            gunBlast1.SetActive(true);
            gunBlast2.SetActive(true);
            Invoke("deactivateBlast", 0.1f);
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, barrelEnd.position, rotator.rotation);
            Physics2D.IgnoreCollision(bullet.collider2D, transform.collider2D);
            bullet.transform.Rotate(new Vector3(0, 0,90)); 
            bullet.rigidbody2D.AddForce(barrelEnd.transform.up * 10, ForceMode2D.Impulse);
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
