using UnityEngine;
using System.Collections;
using Includes;

[RequireComponent(typeof(FireElement))]
[RequireComponent(typeof(FrostElement))]
[RequireComponent(typeof(EarthElement))]
[RequireComponent(typeof(Player))]
public class Shoot : MonoBehaviour {

    private FireElement fireShoot;
    private FrostElement frostShoot;
    private EarthElement earthShoot;
    private NeutralElement neutralShoot;

    private Player data;

	// Use this for initialization
	void Start () {
	    fireShoot = GetComponent<FireElement>();
        frostShoot = GetComponent<FrostElement>();
        earthShoot = GetComponent<EarthElement>();
        neutralShoot = GetComponent<NeutralElement>();
        data = GetComponent<Player>();
	}


    public void shoot(float ability1, float ability2, float ability3)
    {
        if (ability1 > 0)
        {
            if(data.currentElement == (int) Elements.FIRE)
            {
                fireShoot.fireMain(AbilityStats.Fire.ability1.attackSpeed, AbilityStats.Fire.ability1.projectile_number, (GameObject) Resources.Load(AbilityStats.Fire.ability1.projectile));
            }

            if (data.currentElement == (int)Elements.FROST)
            {
                frostShoot.fireMain(AbilityStats.Frost.ability1.attackSpeed, AbilityStats.Frost.ability1.projectile_number, (GameObject)Resources.Load(AbilityStats.Frost.ability1.projectile));
            }

            if (data.currentElement == (int)Elements.EARTH)
            {
                earthShoot.fireMain(AbilityStats.Earth.ability1.attackSpeed, AbilityStats.Earth.ability1.projectile_number, (GameObject)Resources.Load(AbilityStats.Earth.ability1.projectile));
            }

            if (data.currentElement == (int)Elements.NEUTRAL)
            {
                neutralShoot.fireMain(AbilityStats.Neutral.ability1.attackSpeed, AbilityStats.Neutral.ability1.projectile_number, (GameObject)Resources.Load(AbilityStats.Neutral.ability1.projectile));
            }
        }
        else if (ability2 > 0)
        {
            if (data.currentElement == (int)Elements.FIRE)
            {
                fireShoot.fireSecondary();
            }

            if (data.currentElement == (int)Elements.FROST)
            {
                frostShoot.fireSecondary(AbilityStats.Frost.ability2.attackSpeed, AbilityStats.Frost.ability2.projectile_number, (GameObject)Resources.Load(AbilityStats.Frost.ability2.projectile));
            }

            if (data.currentElement == (int)Elements.EARTH)
            {
                earthShoot.fireSecondary(AbilityStats.Earth.ability2.attackSpeed, AbilityStats.Earth.ability2.projectile_number, (GameObject)Resources.Load(AbilityStats.Earth.ability2.projectile));
            }

            if (data.currentElement == (int)Elements.NEUTRAL)
            {
                neutralShoot.fireSecondary(AbilityStats.Neutral.ability2.attackSpeed, AbilityStats.Neutral.ability2.projectile_number, (GameObject)Resources.Load(AbilityStats.Neutral.ability2.projectile));
            }
        }
        else if (ability3 > 0)
        {
            if (data.currentElement == (int)Elements.FIRE)
            {
                fireShoot.fireTerciary(AbilityStats.Fire.ability3.attackSpeed, AbilityStats.Fire.ability3.projectile_number, (GameObject)Resources.Load(AbilityStats.Fire.ability3.projectile));
            }

            if (data.currentElement == (int)Elements.FROST)
            {
                frostShoot.fireTerciary(AbilityStats.Frost.ability3.attackSpeed, AbilityStats.Frost.ability3.projectile_number, (GameObject)Resources.Load(AbilityStats.Frost.ability3.projectile));
            }

            if (data.currentElement == (int)Elements.EARTH)
            {
                earthShoot.fireTerciary(AbilityStats.Earth.ability3.attackSpeed, AbilityStats.Earth.ability3.projectile_number, (GameObject)Resources.Load(AbilityStats.Earth.ability3.projectile));
            }

            if (data.currentElement == (int)Elements.NEUTRAL)
            {
                neutralShoot.fireTerciary(AbilityStats.Neutral.ability3.attackSpeed, AbilityStats.Neutral.ability3.projectile_number, (GameObject)Resources.Load(AbilityStats.Neutral.ability3.projectile));
            }
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
