using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Reflection;
using Includes;

[RequireComponent(typeof(Player))]
public class Interactions : MonoBehaviour {

    private GUIManager gui;

    private int _currentElement = (int)Elements.NEUTRAL;

    private List<ShootElement> _elements;

    private Player data;
    private PlayerStats stats;

    private Interactable _interactable = null;

    private bool showingStats = false;
    private int numberActiveElements = 0;

    //private float cycleTime = 0.2f;

    //private bool canCycle = true;

	// Use this for initialization
	void Start () {
        stats = GameManager.Instance.Stats;
        _elements = new List<ShootElement>();
        gui = GameObject.Find("GUI").GetComponent<GUIManager>();
        _elements.Add(GetComponent<NeutralElement>());
        _elements.Add(GetComponent<FireElement>());
        _elements.Add(GetComponent<EarthElement>());
        _elements.Add(GetComponent<FrostElement>());
        data = GetComponent<Player>();
        checkAvailableElements();
        GameManager.Instance.CurrentElement = _elements[0];
    }

    #region Element Updating
    private void checkAvailableElements()
    {
        //Iterate all element limits and check which ones are unlock
        var type = typeof(PlayerStats); // Get type pointer
        FieldInfo[] fields = type.GetFields(); // Obtain all fields
        int numActiveElem = 0;
        foreach (var field in fields) // Loop through fields
        {
            // is a float
            if (typeof(float).IsAssignableFrom(field.FieldType))
            {
                if ((field.Name.Contains("primary_") || field.Name.Contains("secondary_") || field.Name.Contains("terciary_")) && !field.Name.Contains("def_") && !field.Name.Contains("lim_"))
                {
                    if (field.Name.Contains("neutral"))
                    {
                        if ((float)field.GetValue(GameManager.Instance.Stats) > 0)
                        {
                            _elements[0].Active = true;
                            numActiveElem++;
                        }
                    }
                    if (field.Name.Contains("fire"))
                    {
                        if ((float)field.GetValue(GameManager.Instance.Stats) > 0)
                        {
                            _elements[1].Active = true;
                            numActiveElem++;
                        }
                    }
                    if (field.Name.Contains("earth"))
                    {
                        if ((float)field.GetValue(GameManager.Instance.Stats) > 0)
                        {
                            _elements[2].Active = true;
                            numActiveElem++;
                        }
                    }
                    if (field.Name.Contains("water"))
                    {
                        if ((float)field.GetValue(GameManager.Instance.Stats) > 0)
                        {
                            _elements[3].Active = true;
                            numActiveElem++;
                        }
                    }
                }
            }
        }
        numberActiveElements = numActiveElem;

        gui.initCoolDownWindows(_elements);
    }

    private void checkElementTypeToModify(FieldInfo field, ref int numActiveElem, string element)
    {
        if (field.Name.Contains("neutral") && element.Contains("neutral"))
        {
            _elements[0].Active = true;
            numActiveElem++;
            typeof(PlayerStats).GetField(field.Name).SetValue(GameManager.Instance.Stats, 1);
            _elements[0].updateUnlocked();
        }
        else if (field.Name.Contains("fire") && element.Contains("fire"))
        {
            _elements[1].Active = true;
            numActiveElem++;
            typeof(PlayerStats).GetField(field.Name).SetValue(GameManager.Instance.Stats, 1);
            _elements[1].updateUnlocked();
        }
        else if (field.Name.Contains("earth") && element.Contains("earth"))
        {
            _elements[2].Active = true;
            numActiveElem++;
            typeof(PlayerStats).GetField(field.Name).SetValue(GameManager.Instance.Stats, 1);
            _elements[2].updateUnlocked();
        }
        else if (field.Name.Contains("water") && element.Contains("water"))
        {
            _elements[3].Active = true;
            numActiveElem++;
            typeof(PlayerStats).GetField(field.Name).SetValue(GameManager.Instance.Stats, 1);
            _elements[3].updateUnlocked();
        }
    }

    public void updateActiveElements(string element)
    {
        var type = typeof(PlayerStats); // Get type pointer
        FieldInfo[] fields = type.GetFields(); // Obtain all fields
        int numActiveElem = 0;
        foreach (var field in fields) // Loop through fields
        {
            // is a float
            if (typeof(float).IsAssignableFrom(field.FieldType))
            {
                if (field.Name.Contains("primary_") && element.Contains("primary_") && !field.Name.Contains("def_"))
                {
                    checkElementTypeToModify(field, ref numActiveElem, element);
                }
                else if (field.Name.Contains("secondary_") && element.Contains("secondary_") && !field.Name.Contains("def_"))
                {
                    checkElementTypeToModify(field, ref numActiveElem, element);
                }
                else if (field.Name.Contains("terciary_") && element.Contains("terciary_") && !field.Name.Contains("def_"))
                {
                    checkElementTypeToModify(field, ref numActiveElem, element);
                }
                else if (field.Name.Contains("lim_"))
                {
                    if (field.Name.Contains(element))
                    {
                        typeof(PlayerStats).GetField(field.Name).SetValue(GameManager.Instance.Stats, 1);
                    }
                }
            }
        }
        numberActiveElements = numActiveElem;

        gui.processCoolDownWindows(_elements);
    }
    
    #endregion

    public int CurrentElement { get { return _currentElement; } set { _currentElement = value; } }

    #region Change Element
    public void changeCurrentElement(bool forward, bool backward)
    {
        if (forward)
        {
            do
            {
                _currentElement++;
                if (_currentElement > Elements.GetNames(typeof(Elements)).Length - 1)
                {
                    _currentElement = 0;
                }

            } while (!_elements[_currentElement].Active);
        }

        if (backward)
        {
            do
            {
                _currentElement--;
                if (_currentElement < 0)
                {
                    _currentElement = Elements.GetNames(typeof(Elements)).Length - 1;
                }

            } while (!_elements[_currentElement].Active);
        }

        GameManager.Instance.CurrentElement = _elements[_currentElement];
        gui.changeCurrentElement(_currentElement);
    }

    #endregion

    #region Environment Interaction

    public Interactable Interactable
    {
        get
        {
            return _interactable;
        }
        set
        {
            _interactable = value;
        }
    }

    public void interact()
    {
        _interactable.applyEffect();
    } 

    #endregion

    public void showStats()
    {
        if (!showingStats)
        {
            gui.showStats();
            showingStats = true;
        }
        else
        {
            showingStats = false;
            gui.hideStats();
        }
    }

}
