using UnityEngine;
using System.Collections;
using Includes;

public class ShootElement : MonoBehaviour
{
    protected Elements _elementType;
    protected bool _active;

    #region Variables
    protected Transform barrelEnd;
    protected Transform rotator;
    // protected GameObject bulletPrefab;

    protected GameObject gunBlast1;
    protected GameObject gunBlast2;

    protected GameObject projectile1;
    protected GameObject projectile2;
    protected GameObject projectile3;

    protected bool canMain = true;
    protected bool canSecondary = true;
    protected bool canTerciary = true;

    protected bool mainUnlocked;
    protected bool secondaryUnlocked;
    protected bool terciaryUnlocked;
    
    #endregion

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

    protected void fire(float attackSpeed, int projectileNumber, float damage, GameObject bulletPrefab)
    {
        gunBlast1.SetActive(true);
        gunBlast2.SetActive(true);
        Invoke("deactivateBlast", 0.1f);
        for (int i = 0; i < projectileNumber; i++)
        {
            GameObject bullet = (GameObject)Instantiate(bulletPrefab, barrelEnd.position, rotator.localRotation);
            bullet.transform.Rotate(new Vector3(-90, 0, 0));
            // Invokes ability specific movement behaviour
            bullet.GetComponent<AbilityBehaviour>().initiate(gameObject, damage);
            bullet.name = bulletPrefab.name;

        }
    }

    public virtual void fireMain(){ }
    public virtual void fireSecondary() { }
    public virtual void fireTerciary() { }
    public virtual void updateUnlocked() { }
    
    public Elements Type
    {
        get
        {
            return _elementType;
        }
        set
        {
            _elementType = value;
        }
    }

    public bool Active
    {
        get
        {
            return _active;
        }
        set
        {
            _active = value;
        }
    }

    public void setBarrelEnd(Transform t)
    {
        barrelEnd = t;
    }

    public virtual void checkAbilitiesCoolDown(){}

}
