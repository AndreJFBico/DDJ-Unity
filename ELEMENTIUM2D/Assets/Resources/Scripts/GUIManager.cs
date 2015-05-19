using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Includes;
using System.Collections.Generic;
using System;

public class GUIManager : MonoBehaviour {

    public GameObject currentElement;
    public GameObject playerHealth;
    public GameObject playerHealthText;
    public GameObject restartButton;
    public MultiplierManager multiplierManager;
    public GameObject statWindowText;
    public GameObject statWindow;

    private List<GameObject> coolDownsAll;
    private List<GameObject> coolDowns;

    private List<float[]> cds;//left - right - middle
    private List<float[]> maxCDs;//left - right - middle
    private List<List<GameObject>> abilitiesSlider;

    private int _currentElementIndex = 0;

    void Awake()
    {
        cds = new List<float[]>();
        maxCDs = new List<float[]>();
        abilitiesSlider = new List<List<GameObject>>();
        coolDowns = new List<GameObject>();
        coolDownsAll = new List<GameObject>();

        string[] elements = Enum.GetNames(typeof(Elements));
        for(int i = 0; i < elements.Length; i++)
        {
            coolDownsAll.Add(transform.FindChild("CoolDowns " + elements[i]).gameObject);
        }
    }

    void Start()
    {
        hideStats();
        StartCoroutine("decreaseCooldowns");
    }

    #region Cooldows
    public void initCoolDownWindows(List<ShootElement> elements)
    {
        coolDowns = new List<GameObject>();
        cds = new List<float[]>();
        maxCDs = new List<float[]>();
        abilitiesSlider = new List<List<GameObject>>();
        coolDowns.AddRange(coolDownsAll);

        List<int> inactiveIndexes = new List<int>();
        for(int i = 0; i < elements.Count; i++)
        {
            if (!elements[i].Active)
            {
                inactiveIndexes.Add(i);
            }
            coolDownsAll[i].SetActive(false);
        }
        for(int i = inactiveIndexes.Count; i > 0; i--)
        {
            coolDowns.RemoveAt(inactiveIndexes[i-1]);
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
        for (int j = 0; j < maxCDs.Count; j++)
        {
            for (int i = 0; i < maxCDs[j].Length; i++)
            {
                cds[j][i] = int.MaxValue;
                maxCDs[j][i] = int.MaxValue;
            }
        }
        foreach (ShootElement e in elements)
        {
            e.checkAbilitiesCoolDown();
        }
        coolDownsAll[0].SetActive(true);
    }

    public void processCoolDownWindows(List<ShootElement> elements)
    {
        coolDowns = new List<GameObject>();
        abilitiesSlider = new List<List<GameObject>>();
        coolDowns.AddRange(coolDownsAll);

        List<int> inactiveIndexes = new List<int>();
        for (int i = 0; i < elements.Count; i++)
        {
            if (!elements[i].Active)
            {
                inactiveIndexes.Add(i);
            }
            coolDownsAll[i].SetActive(false);
        }
        for (int i = inactiveIndexes.Count; i > 0; i--)
        {
            coolDowns.RemoveAt(inactiveIndexes[i - 1]);
        }
        for (int i = 0; i < coolDownsAll.Count; i++)
        {
            abilitiesSlider.Add(new List<GameObject>());
            abilitiesSlider[i].Add(coolDownsAll[i].transform.FindChild("LMBCDStrip").gameObject);
            abilitiesSlider[i].Add(coolDownsAll[i].transform.FindChild("RMBCDStrip").gameObject);
            abilitiesSlider[i].Add(coolDownsAll[i].transform.FindChild("MMBCDStrip").gameObject);
        }
        foreach (ShootElement e in elements)
        {
            e.checkAbilitiesCoolDown();
        }
        coolDownsAll[_currentElementIndex].SetActive(true);
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
                        if (j == _currentElementIndex)
                            abilitiesSlider[j][i].transform.localScale = new Vector3(1.0f, cds[j][i] / maxCDs[j][i], 1.0f);
                        continue;
                    }
                    cds[j][i] -= Time.deltaTime;
                    if (cds[j][i] < 0) cds[j][i] = 0;
                    if (j == _currentElementIndex)
                        abilitiesSlider[j][i].transform.localScale = new Vector3(1.0f, cds[j][i] / maxCDs[j][i], 1.0f);
                }
            }
        }
    }

    public void addCoolDown(Elements type, int whichAbility, float timer)
    {
        cds[(int)type][whichAbility] = timer;
        maxCDs[(int)type][whichAbility] = timer;
    }
 
    private void changeCoolDownsDisplay(int state)
    {
        for(int i = 0; i < coolDownsAll.Count; i++)
        {
            if (i == state)
                coolDownsAll[i].SetActive(true);
            else
                coolDownsAll[i].SetActive(false);
        }
    }
    #endregion

    public void changeCurrentElement(int state)
    {
        changeCurrentElementDisplay(state);
        changeCoolDownsDisplay(state);
    }

    public void changeCurrentElementDisplay(int state)
    {
        _currentElementIndex = state;
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
        restartButton.GetComponent<Image>().enabled = true;
        restartButton.GetComponent<Button>().enabled = true;
        restartButton.GetComponentInChildren<Text>().enabled = true;
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


