using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Shoot))]
[RequireComponent(typeof(Interactions))]
public class Action : MonoBehaviour {

    public float fire1 = 0.0f;
    public float fire2 = 0.0f;
    public float fire3 = 0.0f;

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
	
	// Update is called once per frame
	void Update () {
        bool shot = false;

        #region Fire weapon
        //##########################################################################################
        //####################### FIRE #############################################################
        //##########################################################################################

        fire1 = Input.GetAxis("Fire1");
        fire2 = Input.GetAxis("Fire2");
        fire3 = Input.GetAxis("Fire3");

        if (fire1 + fire2 + fire3 > 0)
        {
            shot = shooting.shoot(fire1, fire2, fire3);
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

	}
}
