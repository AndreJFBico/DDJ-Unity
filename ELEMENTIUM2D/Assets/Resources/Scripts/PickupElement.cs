using UnityEngine;
using System.Collections;
using Includes;

public class PickupElement : Interactable {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.CompareTo("Player") == 0)
        {
            displayText();
            GameManager.Instance.Player.GetComponent<Interactions>().Interactable = this;

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.CompareTo("Player") == 0)
        {
            hideText();
            GameManager.Instance.Player.GetComponent<Interactions>().Interactable = null;
        }
    }

    public override void applyEffect()
    {

    }
}
