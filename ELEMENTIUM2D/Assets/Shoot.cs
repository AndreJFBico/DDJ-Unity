using UnityEngine;
using System.Collections;

[RequireComponent(typeof(FireElement))]
[RequireComponent(typeof(FrostElement))]
[RequireComponent(typeof(EarthElement))]
[RequireComponent(typeof(PlayerData))]
public class Shoot : Auxiliars {

    private FireElement fireShoot;
    private FrostElement frostShoot;
    private EarthElement earthShoot;

    private PlayerData data;

	// Use this for initialization
	void Start () {
	    fireShoot = GetComponent<FireElement>();
        frostShoot = GetComponent<FrostElement>();
        earthShoot = GetComponent<EarthElement>();
        data = GetComponent<PlayerData>();
	}
	
    public void shoot (float fire1, float fire2, float fire3)
    {
        if(fire1 > 0)
        {
            if(data.currentElement == (int) Elements.FIRE)
            {
                fireShoot.fireMain();
            }

            if (data.currentElement == (int)Elements.FROST)
            {
                frostShoot.fireMain();
            }
        }
        else if(fire2 > 0)
        {

        }
        else if (fire3 > 0)
        {

        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
