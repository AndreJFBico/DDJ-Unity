using UnityEngine;
using System.Collections;
using Includes;

public class Pickup : Interactable {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.CompareTo("Player") == 0)
        {
            displayText(false);
            playerInteractions.Interactable = this;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.CompareTo("Player") == 0)
        {
            hideText();
            playerInteractions.Interactable = null;
        }
    }
}
