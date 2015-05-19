using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Includes;

[RequireComponent(typeof(FireElement))]
[RequireComponent(typeof(FrostElement))]
[RequireComponent(typeof(EarthElement))]
[RequireComponent(typeof(Player))]
public class Shoot : MonoBehaviour {

    private bool canShoot = true;
    private float globalCD = 0.3f;

    private void resetGlobalCD()
    {

    }

    public bool shoot(float ability1, float ability2, float ability3, int priority)
    {
        if (ability3 > 0 && canShoot)
            GameManager.Instance.CurrentElement.fireTerciary();
        if (ability2 > 0)
            GameManager.Instance.CurrentElement.fireSecondary();
        if (ability1 > 0)
            GameManager.Instance.CurrentElement.fireMain();

        return false;
    }
}
