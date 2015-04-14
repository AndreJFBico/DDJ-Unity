using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FloatingText{

	private static FloatingText _instance = null;

    private static int numberTexts;

    private static List<TextFloat> _beingUsedTexts;

    private static List<TextFloat> _floatingTexts;

    private static GameObject _floatText;

    #region Initialization

    protected FloatingText() { }
    // Singleton pattern implementation
    public static FloatingText Instance { get { if (_instance == null) { _instance = new FloatingText(); init(); } return _instance; } }

    private static void init()
    {
        _floatText = Resources.Load("Prefabs/FloatingText") as GameObject;
        _beingUsedTexts = new List<TextFloat>();
        _floatingTexts = new List<TextFloat>();        
    }

    public void sceneInit()
    {
        _beingUsedTexts = new List<TextFloat>();
        _floatingTexts = new List<TextFloat>();        
        numberTexts = 30;
        List<GameObject> objs = PrefabFactory.createPrefabs(_floatText, numberTexts);
        foreach (GameObject o in objs)
        {
            TextFloat text = o.GetComponent<TextFloat>();
            o.SetActive(false);
            _floatingTexts.Add(text);
        }
    }
 
	#endregion

    public void createFloatingText(Transform transf, string message, Color color)
    {
        TextFloat text;
        if(_floatingTexts.Count > 0)
        {
            text = _floatingTexts[0];
            _floatingTexts.RemoveAt(0);
            _beingUsedTexts.Add(text);
        }
        else
        {
            text = _beingUsedTexts[0];
            _beingUsedTexts.RemoveAt(0);
            _beingUsedTexts.Add(text);
        }

        text.gameObject.transform.position = transf.position;
        text.gameObject.SetActive(true);
        text.startFloating(message, color);
    }

    public void returnText(TextFloat obj)
    {
        _beingUsedTexts.Remove(obj);
        _floatingTexts.Add(obj);
        obj.gameObject.SetActive(false);
    }
}
