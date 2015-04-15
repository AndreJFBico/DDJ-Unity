using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Includes;

[RequireComponent(typeof(Player))]
public class Interactions : MonoBehaviour {

    private Image elementGUI;

    private Sprite neutralElement;
    private Sprite fireElement;
    private Sprite earthElement;
    private Sprite frostElement;

    private Player data;

    private Interactable _interactable = null;

    //private float cycleTime = 0.2f;

    //private bool canCycle = true;

	// Use this for initialization
	void Start () {
        elementGUI = GameObject.Find("CurrentElementGUI").GetComponent<Image>();
        neutralElement = Resources.Load<Sprite>("GUIImages/Elements/Neutral");
        fireElement = Resources.Load<Sprite>("GUIImages/Elements/Fire");
        earthElement = Resources.Load<Sprite>("GUIImages/Elements/Earth");
        frostElement = Resources.Load<Sprite>("GUIImages/Elements/Frost") ;
        data = GetComponent<Player>();
    }

    #region Change Element
    public void changeCurrentElement(bool forward, bool backward)
    {
        if (forward)
        {
            data.currentElement++;
            if (data.currentElement > Elements.GetNames(typeof(Elements)).Length - 1)
            {
                data.currentElement = 0;
            }
        }

        if (backward)
        {
            data.currentElement--;
            if (data.currentElement < 0)
            {
                data.currentElement = Elements.GetNames(typeof(Elements)).Length - 1;
            }
        }
        updateIcon();
    }

    private void updateIcon()
    {
        switch (data.currentElement)
        {
            case 0:
                elementGUI.sprite = neutralElement;
                break;
            case 1:
                elementGUI.sprite = fireElement;
                break;
            case 2:
                elementGUI.sprite = earthElement;
                break;
            case 3:
                elementGUI.sprite = frostElement;
                break;

            default:
                break;
        }
    } 
    #endregion

    #region Environment Interaction

    #region Chests

    public Interactable Interactable
    {
        get
        {
            return _interactable;
        }
        set
        {
            _interactable = value;
        }
    }

    public void openChest()
    {
        _interactable.applyEffect();
    } 

    #endregion

    #endregion

}
