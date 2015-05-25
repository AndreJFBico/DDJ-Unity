using UnityEngine;
using System.Collections;
using Includes;

public class TreasureChest : Interactable{

    public SpriteRenderer spriteRenderer;
    private Sprite openChest;
    private IEnumerator st;

    public bool locked = false;
    private bool used = false;
    protected string effectApplied;

	public int state = 0;

    public override void Start()
    {
        base.Start();
        st = showText(0.5f);
        openChest = Resources.Load<Sprite>("Sprites/Environment/Chests/treasureChestOpen");
    }

    #region OntriggerEnter and Exit

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.CompareTo("Player") == 0)
        {
            displayText();
            if(used)
            {
                textDisplay.GetComponent<TextMesh>().text = effectApplied;
            }
            else
            {
                if (!locked)
                {
                    playerInteractions.Interactable = this;
                }
                else
                    textDisplay.GetComponent<TextMesh>().text = "Locked";
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.CompareTo("Player") == 0)
        {
            StopCoroutine(st);
            hideText();
            playerInteractions.Interactable = null;
        }
    }
    
    #endregion

	public void Update(){
		if(state == 1){
			state = 2;
			applyEffect();
		}
	}

    public virtual void updateClosedSprite(Sprite sp)
    {
        spriteRenderer.sprite = sp;
    }

    IEnumerator showText(float time)
    {
        yield return new WaitForSeconds(time);
        if(state != 2){
			displayText();
		}else{
			state = 3;
		}
        if (used)
        {
            textDisplay.GetComponent<TextMesh>().text = effectApplied;
        }
        else
        {
            if (!locked)
            {
                playerInteractions.Interactable = this;
            }
            else
                textDisplay.GetComponent<TextMesh>().text = "Locked";
        }
    }

    public override void applyEffect()
    {
        //use reflection to select from a list of player attributes a random one to increase by 1%
        spriteRenderer.sprite = openChest;
        //Destroy(gameObject);
        used = true;
        hideText();
        playerInteractions.Interactable = null;
        StartCoroutine(st);
    }


	public void forceOpen(){
		state = 1;
	}
}
