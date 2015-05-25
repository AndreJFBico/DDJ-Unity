using UnityEngine;
using System.Collections;
using Includes;

public class Interactable : MonoBehaviour {

    protected GameObject textDisplay;
    protected GameObject textImage;
    protected Interactions playerInteractions;

    public virtual void Start()
    {
        playerInteractions = GameManager.Instance.Player.GetComponent<Interactions>();
        textDisplay = transform.FindChild("InteractionText").gameObject;
        textImage = transform.FindChild("InteractionImage").gameObject;
        textDisplay.SetActive(false);
        textImage.SetActive(false);
    }

    protected void displayText(bool used)
    {
        if(used)
        {
            textDisplay.SetActive(true);
            textImage.SetActive(false);
            textDisplay.GetComponent<TextMesh>().text = "[F]";
        }
        else
        {
            if (Input.GetJoystickNames().Length > 0)
            {
                textImage.SetActive(true);
            }
            else
            {
                textImage.SetActive(false);
                textDisplay.SetActive(true);
                textDisplay.GetComponent<TextMesh>().text = "[F]";
            }
        }
    }

    protected void hideText()
    {
        textDisplay.SetActive(false);
        textImage.SetActive(false);
    }

    public virtual void applyEffect() { }

}
