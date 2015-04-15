using UnityEngine;
using System.Collections;
using Includes;

public class TreasureChest : Interactable{

    public SpriteRenderer spriteRenderer;
    private Sprite openChest;

    public bool locked = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.CompareTo("Player") == 0)
        {
            displayText();
            if (!locked)
            {
                GameManager.Instance.Player.GetComponent<Interactions>().Interactable = this;
            }
            else
                textDisplay.GetComponent<TextMesh>().text = "Locked";

        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag.CompareTo("Player") == 0)
        {
            hideText();
            GameManager.Instance.Player.GetComponent<Interactions>().Interactable = null;
        }
    }

    public override void applyEffect()
    {
        //use reflection to select form a list of player attributes a random one to increase by 1%
        spriteRenderer.sprite = openChest;
        Destroy(gameObject);
    }

}
