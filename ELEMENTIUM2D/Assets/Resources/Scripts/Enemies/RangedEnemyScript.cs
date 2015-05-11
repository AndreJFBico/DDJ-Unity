using UnityEngine;
using System.Collections;

public class RangedEnemyScript : EnemyScript {

    protected float attackSpeed;
    protected GameObject projectile;
    protected Vector3 latestTargetPosition;
    protected Transform activeWeapon;

    public Transform left;
    public Transform left_firepoint;
    public Transform right;
    public Transform right_firepoint;

    protected Transform currentFireTransform;

    protected GameObject firingTarget;

    void OnEnable()
    {
        StartCoroutine("sendProjectile");
    }

    public override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.CompareTo("Pathing") != 0 && other.tag.CompareTo("Player") == 0)
        {
            firingTarget = other.gameObject;
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.CompareTo("Pathing") != 0 && firingTarget != null && other.gameObject.GetInstanceID() == firingTarget.gameObject.GetInstanceID())
        {
            firingTarget = null;
        }
    }

    protected IEnumerator sendProjectile()
    {
        while (true)
        {
            if (firingTarget != null)
            {
                GameObject p = Instantiate(projectile, currentFireTransform.position, Quaternion.LookRotation(firingTarget.transform.position - transform.position)) as GameObject;
                p.GetComponent<AbilityBehaviour>().initiate(this.gameObject, damage);
            }
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    private bool hasFiringTarget()
    {
        return firingTarget != null;
    }

    protected override void LateUpdate()
    {
        if (hasFiringTarget())
        {
            if (firingTarget.transform.position.x >= transform.position.x)
            {
                left.gameObject.SetActive(true);
                activeWeapon = left;
                right.gameObject.SetActive(false);
                currentFireTransform = left_firepoint;
            }
            else
            {
                right.gameObject.SetActive(true);
                activeWeapon = right;
                left.gameObject.SetActive(false);
                currentFireTransform = right_firepoint;
            }
            pathAgent.setStoppingDistance(rangedRadius);
            activeWeapon.LookAt(firingTarget.transform.position);
        }
        else
        {
            pathAgent.resetStoppingDistance();
            activeWeapon.rotation = Quaternion.identity;
        }
        base.LateUpdate();
    }

    public override void dealDamage(Player player)
    {
        player.takeDamage(damage, type, false);
    }

    public override void Eliminate()
    {
        base.Eliminate();
        firingTarget = null;
    }
}
