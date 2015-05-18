using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Includes;

public class EndGamePortal : Interactable
{
    public bool locked = false;
    private Transform restartButton;
    private Transform endgameMessage;
    private bool used = false;
    protected string effectApplied;

    public override void Start()
    {
        endgameMessage = GameObject.Find("EndgameMessage").transform;
        restartButton = GameObject.Find("RestartButton").transform;
        restartButton.GetComponent<Image>().enabled = false;
        restartButton.GetComponent<Button>().enabled = false;
        restartButton.GetComponentInChildren<Text>().enabled = false;
        endgameMessage.GetComponent<MeshRenderer>().enabled = false;
        base.Start();
    }

    #region OntriggerEnter and Exit
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.CompareTo("Player") == 0)
        {
            displayText();
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
    #endregion

    public override void applyEffect()
    {
        hideText();
        playerInteractions.Interactable = null;
        //ENDGAME
        endgameMessage.GetComponent<MeshRenderer>().enabled = true;
        restartButton.gameObject.SetActive(true);
        Destroy(this);
    }
}
