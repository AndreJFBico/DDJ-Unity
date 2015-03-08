using UnityEngine;
using System.Collections;

public class ShootElement : MonoBehaviour
{

    protected Transform barrelEnd;
    protected Transform rotator;
   // protected GameObject bulletPrefab;

    protected GameObject gunBlast1;
    protected GameObject gunBlast2;

    private bool canMain = true;

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

    // Receives a certain ability attackSpeed
    public void fireMain(float attackSpeed, int projectileNumber, GameObject bulletPrefab)
    {
        if (canMain)
        {
            canMain = false;
            Invoke("resetMain", attackSpeed);
            gunBlast1.SetActive(true);
            gunBlast2.SetActive(true);
            Invoke("deactivateBlast", 0.1f);
            for(int i = 0; i < projectileNumber; i++)
            {
                GameObject bullet = (GameObject)Instantiate(bulletPrefab, barrelEnd.position, rotator.rotation);
                // Invokes ability specific movement behaviour
                bullet.GetComponent<ProjectileBehaviour>().applyMovement();
                //Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), transform.GetComponent<Collider2D>());
                bullet.transform.Rotate(new Vector3(0, 0, 90));
            }
            //bullet.GetComponent<Rigidbody2D>().AddForce(barrelEnd.transform.up * 10f, ForceMode2D.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
