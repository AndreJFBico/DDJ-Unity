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
                fireShoot.fireMain(AbilityStats.Fire.ability1.attackSpeed, AbilityStats.Fire.ability1.projectile_number);
            }

            if (data.currentElement == (int)Elements.FROST)
            {
                frostShoot.fireMain(AbilityStats.Frost.ability1.attackSpeed, AbilityStats.Frost.ability1.projectile_number);
            }

            if (data.currentElement == (int)Elements.EARTH)
            {
                earthShoot.fireMain(AbilityStats.Earth.ability1.attackSpeed, AbilityStats.Earth.ability1.projectile_number);
            }

            if (data.currentElement == (int)Elements.NEUTRAL)
            {
                neutralShoot.fireMain(AbilityStats.Neutral.ability1.attackSpeed, AbilityStats.Neutral.ability1.projectile_number);
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
