using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Includes;

public class GUIManager : MonoBehaviour {

    public GameObject currentElement;
    public GameObject playerHealth;
    public GameObject restartButton;
    public GameObject multiplierText;
    public GameObject statWindow;

    public void changeCurrentElementDisplay(int state)
    {
        switch (state)
        {
            case 0:
                currentElement.GetComponent<Image>().sprite = GameManager.Instance.NeutralElement;
                break;
            case 1:
                currentElement.GetComponent<Image>().sprite = GameManager.Instance.FireElement;
                break;
            case 2:
                currentElement.GetComponent<Image>().sprite = GameManager.Instance.EarthElement;
                break;
            case 3:
                currentElement.GetComponent<Image>().sprite = GameManager.Instance.WaterElement;
                break;

            default:
                break;
        }
    }

    public void playerDeath()
    {
        restartButton.SetActive(true);
    }

    public void restartLevel()
    {
        Time.timeScale = 1;
        Application.LoadLevel("SkillTree");
    }
}


