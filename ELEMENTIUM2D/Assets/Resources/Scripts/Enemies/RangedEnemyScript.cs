using UnityEngine;
using System.Collections;
using Includes;

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

    protected Transform firingTarget;

    void OnEnable()
    {
        StartCoroutine("sendProjectile");
    }

    protected IEnumerator sendProjectile()
    {
        while (true)
        {
            if (firingTarget != null)
            {
                GameObject p = Instantiate(projectile, currentFireTransform.position, Quaternion.LookRotation(firingTarget.position - transform.position)) as GameObject;
                p.GetComponent<AbilityBehaviour>().initiate(this.gameObject, damage);
            }
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    private bool hasFiringTarget()
    {
        return firingTarget != null;
    }

    private void checkIfTargetInRange()
    {
        if(pathAgent.hasTarget())
        {
            if ((pathAgent.target.position - transform.position).magnitude < rangedRadius && checkIfTargetInLOS(pathAgent.target))
            {
                firingTarget = pathAgent.target;
            }
            else
            {
                firingTarget = null;
            }
        }
        else
        {
            firingTarget = null;
        }
    }

    private bool checkIfTargetInLOS(Transform target)
    {
        Vector3 direction = (target.position - currentFireTransform.position).normalized;
        Vector3 origin = currentFireTransform.position - direction * 0.3f;
        RaycastHit hit;
        if (Physics.SphereCast(new Ray(origin, direction), 0.1f, out hit, (target.position - currentFireTransform.position).magnitude, LayerMask.GetMask(Constants.obstacles) | LayerMask.GetMask(Constants.breakable)))
        {
            Debug.DrawRay(origin, hit.point - origin, Color.cyan);
            return false;
        }
        Debug.DrawRay(origin, target.position - origin, Color.red);
        return true;
    }

    protected override void LateUpdate()
    {
        checkIfTargetInRange();
        if (pathAgent.hasTarget())
        {
            if (pathAgent.target.position.x >= transform.position.x)
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
            if (hasFiringTarget())
            {
                pathAgent.setStoppingDistance(rangedRadius);
            }
            else
            {
                pathAgent.resetStoppingDistance();
            }
            activeWeapon.LookAt(pathAgent.target.position);
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
