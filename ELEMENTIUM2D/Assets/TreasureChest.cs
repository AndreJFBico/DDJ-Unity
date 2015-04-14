using UnityEngine;
using System.Collections;
using Includes;

public class TreasureChest : MonoBehaviour {

    private GameObject textDisplay;

    public bool locked = false;

    void Awake()
    {
        textDisplay = transform.FindChild("InteractionText").gameObject;
        textDisplay.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.CompareTo("Player") == 0)
        {
            textDisplay.SetActive(true);
            if (!locked)
                textDisplay.GetComponent<TextMesh>().text = "[F]";
            else
                textDisplay.GetComponent<TextMesh>().text = "Locked";

            GameManager.Instance.Player.GetComponent<Interactions>().Treasure = this;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag.CompareTo("Player") == 0)
        {
            textDisplay.SetActive(false);
            GameManager.Instance.Player.GetComponent<Interactions>().Treasure = null;
        }
    }

    public void applyEffect()
    {

    }

}
