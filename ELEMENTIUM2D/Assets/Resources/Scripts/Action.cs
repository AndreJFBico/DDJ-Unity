using UnityEngine;
using System.Collections;
using Includes;

[RequireComponent(typeof(Shoot))]
[RequireComponent(typeof(Interactions))]
public class Action : MonoBehaviour {

    public float fire1 = 0.0f;
    public float fire2 = 0.0f;
    public float fire3 = 0.0f;
    public bool fired2 = false;
    public bool fired3 = false;
    public int priority = 0;


    public bool changeElementForward = false;
    public bool changeElementBackward = false;

    public bool interact = false;
    public bool showStats = false;

    private Shoot shooting;
    private Interactions interaction;

	// Use this for initialization
	void Start () {
        shooting = GetComponent<Shoot>();
        interaction = GetComponent<Interactions>();
	}
	
    private int checkIfWeaponFiredLastFrame(float fire1, float fire2, float fire3)
    {
        if (fire2 > 0)
            fired2 = fired2 ? false : true;
        if(fire3 > 0)
            fired3 = fired3 ? false : true;
        if (fired2)
            priority = 2;
        if (fired3)
            priority = 3;
        return priority;
    }

	// Update is called once per frame
	void Update () {
        bool shot = false;

        #region Fire weapon
        //##########################################################################################
        //####################### FIRE #############################################################
        //##########################################################################################

        fire1 = Input.GetAxis("Fire1");
		fire2 = Input.GetAxis("Fire2") + Input.GetAxis("JoystickFire2");
		fire3 = Input.GetAxis("Fire3") + Input.GetAxis("JoystickFire3");

		float hor = Mathf.Abs(Input.GetAxisRaw ("JoystickRightHor"));
		float ver = Mathf.Abs(Input.GetAxisRaw ("JoystickRightVer"));

		if(hor + ver > 0.4f)
			fire1 += hor + ver;

        if (fire1 + fire2 + fire3 > 0)
        {
            shot = shooting.shoot(fire1, fire2, fire3, checkIfWeaponFiredLastFrame(fire1, fire2, fire3));
            fired2 = fire2 > 0;
            fired3 = fire3 > 0;
        } 

        #endregion

        #region Change Element
        //##########################################################################################
        //####################### CHANGE ELEMENT ###################################################
        //##########################################################################################

        changeElementForward = Input.GetButtonDown("Change Element Forward");
        changeElementBackward = Input.GetButtonDown("Change Element Backward");

        if (changeElementForward || changeElementBackward)
        {
            interaction.changeCurrentElement(changeElementForward, changeElementBackward);
        } 
        #endregion

        #region Environment Interaction
        //##########################################################################################
        //####################### ENVIRONMENT INTERACTION ##########################################
        //##########################################################################################

        interact = Input.GetButtonDown("Interact");

        if (interact && interaction.Interactable)
        {
            interaction.interact();
        } 
        #endregion 

        #region Stats
        //##########################################################################################
        //##################################### STATS ##############################################
        //##########################################################################################

        showStats = Input.GetButtonDown("ShowStats");
        if(showStats)
            interaction.showStats();
        
        #endregion 

        if(Input.GetKeyDown(KeyCode.Comma))
        {
            GameManager.Instance.Player.endGame();
        }
	}
}
