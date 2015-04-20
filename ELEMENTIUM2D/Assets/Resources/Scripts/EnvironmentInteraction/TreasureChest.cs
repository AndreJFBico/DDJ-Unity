using UnityEngine;
using System.Collections;
using Includes;

public class TreasureChest : Interactable{

    public SpriteRenderer spriteRenderer;
    private Sprite openChest;

    public bool locked = false;

    public override void Start()
    {
        base.Start();
        openChest = Resources.Load<Sprite>("Sprites/Environment/Chests/treasureChestOpen");
    }

    #region OntriggerEnter and Exit

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.CompareTo("Player") == 0)
        {
            displayText();
            if (!locked)
            {
                playerInteractions.Interactable = this;
            }
            else
                textDisplay.GetComponent<TextMesh>().text = "Locked";

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
    
    #endregion

    public virtual void updateClosedSprite(Sprite sp)
    {
        spriteRenderer.sprite = sp;
    }

    public override void applyEffect()
    {
        //use reflection to select from a list of player attributes a random one to increase by 1%
        spriteRenderer.sprite = openChest;
        Destroy(gameObject);
    }

}
