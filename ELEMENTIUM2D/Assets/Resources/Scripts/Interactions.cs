using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Reflection;
using Includes;

[RequireComponent(typeof(Player))]
public class Interactions : MonoBehaviour {

    private Image elementGUI;

    private Sprite neutralElement;
    private Sprite fireElement;
    private Sprite earthElement;
    private Sprite frostElement;

    private int _currentElement = (int)Elements.NEUTRAL;

    private List<ShootElement> _elements;

    private Player data;
    private PlayerStats stats;

    private Interactable _interactable = null;

    //private float cycleTime = 0.2f;

    //private bool canCycle = true;

	// Use this for initialization
	void Start () {
        stats = GameManager.Instance.Stats;
        _elements = new List<ShootElement>();
        elementGUI = GameObject.Find("CurrentElementGUI").GetComponent<Image>();
        neutralElement = Resources.Load<Sprite>("GUIImages/Elements/Neutral");
        fireElement = Resources.Load<Sprite>("GUIImages/Elements/Fire");
        earthElement = Resources.Load<Sprite>("GUIImages/Elements/Earth");
        frostElement = Resources.Load<Sprite>("GUIImages/Elements/Frost");
        _elements.Add(GetComponent<NeutralElement>());
        _elements.Add(GetComponent<FireElement>());
        _elements.Add(GetComponent<EarthElement>());
        _elements.Add(GetComponent<FrostElement>());
        data = GetComponent<Player>();
        checkAvailableElements();
        GameManager.Instance.CurrentElement = _elements[0];
    }

    private void checkAvailableElements()
    {
        //Iterate all element limits and check which ones are unlock
        var type = typeof(PlayerStats); // Get type pointer
        FieldInfo[] fields = type.GetFields(); // Obtain all fields
        foreach (var field in fields) // Loop through fields
        {
            // is a float
            if (typeof(float).IsAssignableFrom(field.FieldType))
            {
                if (field.Name.Contains("primary_") && !field.Name.Contains("def_") && !field.Name.Contains("lim_"))
                {
                    if (field.Name.Contains("neutral"))
                    {
                        if ((float)field.GetValue(GameManager.Instance.Stats) > 0)
                            _elements[0].Active = true;
                    }
                    if (field.Name.Contains("fire"))
                    {
                        if ((float)field.GetValue(GameManager.Instance.Stats) > 0)
                            _elements[1].Active = true;
                    }
                    if (field.Name.Contains("earth"))
                    {
                        if ((float)field.GetValue(GameManager.Instance.Stats) > 0)
                            _elements[2].Active = true;
                    }
                    if (field.Name.Contains("water"))
                    {
                        if ((float)field.GetValue(GameManager.Instance.Stats) > 0)
                            _elements[3].Active = true;
                    }
                }
            }
        }
    }

    public void updateActiveElements(string element)
    {
        var type = typeof(PlayerStats); // Get type pointer
        FieldInfo[] fields = type.GetFields(); // Obtain all fields
        foreach (var field in fields) // Loop through fields
        {
            // is a float
            if (typeof(float).IsAssignableFrom(field.FieldType))
            {
                if (field.Name.Contains("primary_") && !field.Name.Contains("def_"))
                {
                    if (field.Name.Contains("neutral") && string.Compare("neutral", element) == 0)
                    {
                        _elements[0].Active = true;
                        typeof(PlayerStats).GetField(field.Name).SetValue(GameManager.Instance.Stats, 1);
                        _elements[0].updateUnlocked();
                        continue;
                    }
                    if (field.Name.Contains("fire") && string.Compare("fire", element) == 0)
                    {
                        _elements[1].Active = true;
                        typeof(PlayerStats).GetField(field.Name).SetValue(GameManager.Instance.Stats, 1);
                        _elements[1].updateUnlocked();
                        continue;
                    }
                    if (field.Name.Contains("earth") && string.Compare("earth", element) == 0)
                    {
                        _elements[2].Active = true;
                        typeof(PlayerStats).GetField(field.Name).SetValue(GameManager.Instance.Stats, 1);
                        _elements[2].updateUnlocked();
                        continue;
                    }
                    if (field.Name.Contains("water") && string.Compare("water", element) == 0)
                    {
                        _elements[3].Active = true;
                        typeof(PlayerStats).GetField(field.Name).SetValue(GameManager.Instance.Stats, 1);
                        _elements[3].updateUnlocked();
                    }
                }
                else if (field.Name.Contains("lim_") && (field.Name.Contains("terciary_") || field.Name.Contains("secondary_")))
                {
                    if (field.Name.Contains(element))
                    {
                        typeof(PlayerStats).GetField(field.Name).SetValue(GameManager.Instance.Stats, 1);
                    }
                }
            }
        }
    }

    public int CurrentElement { get { return _currentElement; } set { _currentElement = value; } }

    private void cycleElements()
    {
        while (true)
        {
            
        }
    }

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
        updateIcon();
    }

    private void updateIcon()
    {
        switch (_currentElement)
        {
            case 0:
                elementGUI.sprite = neutralElement;
                break;
            case 1:
                elementGUI.sprite = fireElement;
                break;
            case 2:
                elementGUI.sprite = earthElement;
                break;
            case 3:
                elementGUI.sprite = frostElement;
                break;

            default:
                break;
        }
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

}
