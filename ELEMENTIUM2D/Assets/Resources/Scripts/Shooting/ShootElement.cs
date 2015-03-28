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
    private bool canSecondary = true;
    private bool canTerciary = true;

    // Use this for initialization
    void Start()
    {
    }

    private void resetMain()
    {
        canMain = true;
    }

    private void resetSecondary()
    {
        canSecondary = true;
    }

    private void resetTerciary()
    {
        canTerciary = true;
    }

    private void deactivateBlast()
    {
        gunBlast1.SetActive(false);
        gunBlast2.SetActive(false);
    }

    private void fire(float attackSpeed, int projectileNumber, GameObject bulletPrefab)
    {
        gunBlast1.SetActive(true);
        gunBlast2.SetActive(true);
        Invoke("deactivateBlast", 0.1f);
        for(int i = 0; i < projectileNumber; i++)
        {
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, barrelEnd.position, rotator.localRotation);
            bullet.transform.Rotate(new Vector3(-90, 0, 0));
            // Invokes ability specific movement behaviour
            bullet.GetComponent<AbilityBehaviour>().initiate(gameObject);
            bullet.name = bulletPrefab.name;
            //Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), transform.GetComponent<Collider2D>());
                
        }
        //bullet.GetComponent<Rigidbody2D>().AddForce(barrelEnd.transform.up * 10f, ForceMode2D.Impulse);
    }

    // Receives a certain ability attackSpeed
    public void fireMain(float attackSpeed, int projectileNumber, GameObject bulletPrefab)
    {
        if(canMain)
        {
            canMain = false;
            Invoke("resetMain", attackSpeed);
            fire(attackSpeed, projectileNumber, bulletPrefab);
        }
    }

    // Receives a certain ability attackSpeed
    public void fireSecondary(float attackSpeed, int projectileNumber, GameObject bulletPrefab)
    {
        if (canSecondary)
        {
            canSecondary = false;
            Invoke("resetSecondary", attackSpeed);
            fire(attackSpeed, projectileNumber, bulletPrefab);
        }
    }

    // Receives a certain ability attackSpeed
    public void fireTerciary(float attackSpeed, int projectileNumber, GameObject bulletPrefab)
    {
        if(canTerciary)
        {
            canTerciary = false;
            Invoke("resetTerciary", attackSpeed);
            fire(attackSpeed, projectileNumber, bulletPrefab);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
