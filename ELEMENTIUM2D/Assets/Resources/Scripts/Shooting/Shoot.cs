using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Includes;

[RequireComponent(typeof(FireElement))]
[RequireComponent(typeof(FrostElement))]
[RequireComponent(typeof(EarthElement))]
[RequireComponent(typeof(Player))]
public class Shoot : MonoBehaviour {

    public bool shoot(float ability1, float ability2, float ability3, int priority)
    {
        if (ability3 > 0 && !(priority == 2))
            GameManager.Instance.CurrentElement.fireTerciary();
        if (ability2 > 0 && !(priority == 3))
            GameManager.Instance.CurrentElement.fireSecondary();
        if (ability1 > 0 && ability3 <= 0 && ability2 <= 0)
            GameManager.Instance.CurrentElement.fireMain();

        return false;
    }
}
