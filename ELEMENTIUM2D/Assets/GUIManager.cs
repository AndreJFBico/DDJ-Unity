using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Includes;
using System.Collections.Generic;

public class GUIManager : MonoBehaviour {

    public GameObject currentElement;
    public GameObject playerHealth;
    public GameObject playerHealthText;
    public GameObject restartButton;
    public GameObject multiplierText;
    public GameObject statWindowText;
    public GameObject statWindow;
    public List<GameObject> coolDownsAll;

    private List<GameObject> coolDowns;

    private List<float[]> cds;//left - right - middle
    private List<float[]> maxCDs;//left - right - middle
    private List<List<GameObject>> abilitiesSlider;

    void Awake()
    {
        cds = new List<float[]>();
        maxCDs = new List<float[]>();
        abilitiesSlider = new List<List<GameObject>>();
        coolDowns = new List<GameObject>();
    }

    void Start()
    {
        hideStats();
        StartCoroutine("decreaseCooldowns");
    }

    #region Cooldows
    public void processCoolDownWindows(int numberElements)
    {
        coolDowns = new List<GameObject>();
        cds = new List<float[]>();
        maxCDs = new List<float[]>();
        abilitiesSlider = new List<List<GameObject>>();
        coolDowns.AddRange(coolDownsAll);

        while (coolDowns.Count > numberElements)
        {
            coolDowns.RemoveAt(coolDowns.Count - 1);
        }
        for (int i = 0; i < coolDownsAll.Count; i++)
        {
            cds.Add(new float[3]);
            maxCDs.Add(new float[3]);
            abilitiesSlider.Add(new List<GameObject>());
            abilitiesSlider[i].Add(coolDownsAll[i].transform.FindChild("LMBCDStrip").gameObject);
            abilitiesSlider[i].Add(coolDownsAll[i].transform.FindChild("RMBCDStrip").gameObject);
            abilitiesSlider[i].Add(coolDownsAll[i].transform.FindChild("MMBCDStrip").gameObject);
        }
    }

    private IEnumerator decreaseCooldowns()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            for (int j = 0; j < cds.Count; j++)
            {
                for (int i = 0; i < cds[j].Length; i++)
                {
                    if (cds[j][i] <= 0)
                    {
                        cds[j][i] = 0;
                        continue;
                    }
                    cds[j][i] -= Time.deltaTime;
                    abilitiesSlider[j][i].transform.localScale = new Vector3(1.0f, cds[j][i] / maxCDs[j][i], 1.0f);
                }
            }
        }
    }

    public void addCoolDown(int whichAbility, float timer)
    {
        switch (GameManager.Instance.CurrentElement.Type)
        {
            case Elements.NEUTRAL:
                cds[0][whichAbility] = timer;
                maxCDs[0][whichAbility] = timer;
                break;
            case Elements.FIRE:
                cds[1][whichAbility] = timer;
                maxCDs[1][whichAbility] = timer;
                break;
            case Elements.EARTH:
                cds[2][whichAbility] = timer;
                maxCDs[2][whichAbility] = timer;
                break;
            case Elements.WATER:
                cds[3][whichAbility] = timer;
                maxCDs[3][whichAbility] = timer;
                break;
            default:
                break;
        }
    }
 
    private void changeCoolDownsDisplay(int state)
    {
        switch (state)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
    }
    #endregion



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

    private IEnumerator updateStats()
    {
        while(true)
        {
            statWindowText.GetComponent<Text>().text =
                "Damage: " + System.Math.Round(GameManager.Instance.Stats.damage, 1) + "\n" + "\n" +
            "Defence: " + System.Math.Round(GameManager.Instance.Stats.defence, 1) + "%" + "\n" + "\n" +
            "Fire Resist: " + System.Math.Round(GameManager.Instance.Stats.fireResist, 1) + "\n" + "\n" +
            "Water Resist: " + System.Math.Round(GameManager.Instance.Stats.waterResist, 1) + "\n" + "\n" + 
            "Earth Resist: " + System.Math.Round(GameManager.Instance.Stats.earthResist, 1);
            playerHealthText.GetComponent<Text>().text = System.Math.Round(GameManager.Instance.Stats.health, 1) + "/" + System.Math.Round(GameManager.Instance.Stats.maxHealth, 1);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void showStats()
    {
        statWindow.SetActive(true);
        StartCoroutine("updateStats");
    }

    public void hideStats()
    {
        statWindow.SetActive(false);
        playerHealthText.GetComponent<Text>().text = "";
        StopCoroutine("updateStats");
    }
}


