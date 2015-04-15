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


    public bool shoot(float ability1, float ability2, float ability3)
    {
        if (ability1 > 0)
        {
            if(data.currentElement == (int) Elements.FIRE && GameManager.Instance.Stats.primary_fire_level == 1 )
            {
                fireShoot.fireMain(AbilityStats.Fire.ability1.attackSpeed, AbilityStats.Fire.ability1.projectile_number, (GameObject) Resources.Load(AbilityStats.Fire.ability1.projectile));
                return true;
            }

            if (data.currentElement == (int)Elements.FROST && GameManager.Instance.Stats.primary_water_level == 1)
            {
                frostShoot.fireMain(AbilityStats.Frost.ability1.attackSpeed, AbilityStats.Frost.ability1.projectile_number, (GameObject)Resources.Load(AbilityStats.Frost.ability1.projectile));
                return true;
            }

            if (data.currentElement == (int)Elements.EARTH && GameManager.Instance.Stats.primary_earth_level == 1)
            {
                earthShoot.fireMain(AbilityStats.Earth.ability1.attackSpeed, AbilityStats.Earth.ability1.projectile_number, (GameObject)Resources.Load(AbilityStats.Earth.ability1.projectile));
                return true;
            }

            if (data.currentElement == (int)Elements.NEUTRAL && GameManager.Instance.Stats.primary_neutral_level == 1)
            {
                neutralShoot.fireMain(AbilityStats.Neutral.ability1.attackSpeed, AbilityStats.Neutral.ability1.projectile_number, (GameObject)Resources.Load(AbilityStats.Neutral.ability1.projectile));
                return true;
            }
        }
        else if (ability2 > 0)
        {
            if (data.currentElement == (int)Elements.FIRE && GameManager.Instance.Stats.secondary_fire_level == 1)
            {
                fireShoot.fireSecondary();
                return true;
            }

            if (data.currentElement == (int)Elements.FROST && GameManager.Instance.Stats.secondary_water_level == 1)
            {
                frostShoot.fireSecondary(AbilityStats.Frost.ability2.attackSpeed, AbilityStats.Frost.ability2.projectile_number, (GameObject)Resources.Load(AbilityStats.Frost.ability2.projectile));
                return true;
            }

            if (data.currentElement == (int)Elements.EARTH && GameManager.Instance.Stats.secondary_earth_level == 1)
            {
                earthShoot.fireSecondary(AbilityStats.Earth.ability2.attackSpeed, AbilityStats.Earth.ability2.projectile_number, (GameObject)Resources.Load(AbilityStats.Earth.ability2.projectile));
                return true;
            }

            if (data.currentElement == (int)Elements.NEUTRAL && GameManager.Instance.Stats.secondary_neutral_level == 1)
            {
                neutralShoot.fireSecondary(AbilityStats.Neutral.ability2.attackSpeed, AbilityStats.Neutral.ability2.projectile_number, (GameObject)Resources.Load(AbilityStats.Neutral.ability2.projectile));
                return true;
            }
        }
        else if (ability3 > 0)
        {
            if (data.currentElement == (int)Elements.FIRE && GameManager.Instance.Stats.terciary_fire_level == 1)
            {
                fireShoot.fireTerciary(AbilityStats.Fire.ability3.attackSpeed, AbilityStats.Fire.ability3.projectile_number, (GameObject)Resources.Load(AbilityStats.Fire.ability3.projectile));
                return true;
            }

            if (data.currentElement == (int)Elements.FROST && GameManager.Instance.Stats.terciary_water_level == 1)
            {
                frostShoot.fireTerciary(AbilityStats.Frost.ability3.attackSpeed, AbilityStats.Frost.ability3.projectile_number, (GameObject)Resources.Load(AbilityStats.Frost.ability3.projectile));
                return true;
            }

            if (data.currentElement == (int)Elements.EARTH && GameManager.Instance.Stats.terciary_earth_level == 1)
            {
                earthShoot.fireTerciary(AbilityStats.Earth.ability3.attackSpeed, AbilityStats.Earth.ability3.projectile_number, (GameObject)Resources.Load(AbilityStats.Earth.ability3.projectile));
                return true;
            }

            if (data.currentElement == (int)Elements.NEUTRAL && GameManager.Instance.Stats.terciary_neutral_level == 1)
            {
                neutralShoot.fireTerciary(AbilityStats.Neutral.ability3.attackSpeed, AbilityStats.Neutral.ability3.projectile_number, (GameObject)Resources.Load(AbilityStats.Neutral.ability3.projectile));
                return true;
            }
        }
        return false;
    }

	// Update is called once per frame
	void Update () {
	
	}
}
