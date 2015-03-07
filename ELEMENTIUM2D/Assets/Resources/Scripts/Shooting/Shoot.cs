using UnityEngine;
using System.Collections;
using Includes;

[RequireComponent(typeof(FireElement))]
[RequireComponent(typeof(FrostElement))]
[RequireComponent(typeof(EarthElement))]
[RequireComponent(typeof(PlayerData))]
public class Shoot : MonoBehaviour {

    private FireElement fireShoot;
    private FrostElement frostShoot;
    private EarthElement earthShoot;
    private NeutralElement neutralShoot;

    private PlayerData data;

	// Use this for initialization
	void Start () {
	    fireShoot = GetComponent<FireElement>();
        frostShoot = GetComponent<FrostElement>();
        earthShoot = GetComponent<EarthElement>();
        neutralShoot = GetComponent<NeutralElement>();
        data = GetComponent<PlayerData>();
	}


    public void shoot(float ability1, float ability2, float ability3)
    {
        if (ability1 > 0)
        {
            if(data.currentElement == (int) Elements.FIRE)
            {
                fireShoot.fireMain(Cooldowns.Fire.ability1);
            }

            if (data.currentElement == (int)Elements.FROST)
            {
                frostShoot.fireMain(Cooldowns.Frost.ability1);
            }

            if (data.currentElement == (int)Elements.EARTH)
            {
                earthShoot.fireMain(Cooldowns.Earth.ability1);
            }

            if (data.currentElement == (int)Elements.NEUTRAL)
            {
                neutralShoot.fireMain(Cooldowns.Neutral.ability1);
            }
        }
        else if (ability2 > 0)
        {

        }
        else if (ability3 > 0)
        {

        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
