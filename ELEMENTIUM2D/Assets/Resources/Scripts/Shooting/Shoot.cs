using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Includes;

[RequireComponent(typeof(FireElement))]
[RequireComponent(typeof(FrostElement))]
[RequireComponent(typeof(EarthElement))]
[RequireComponent(typeof(Player))]
public class Shoot : MonoBehaviour {

    public bool shoot(float ability1, float ability2, float ability3)
    {
        if (ability1 > 0)
            GameManager.Instance.CurrentElement.fireMain();

        else if (ability2 > 0)
            GameManager.Instance.CurrentElement.fireSecondary();

        else if (ability3 > 0)
            GameManager.Instance.CurrentElement.fireTerciary();

        return false;
    }
}
