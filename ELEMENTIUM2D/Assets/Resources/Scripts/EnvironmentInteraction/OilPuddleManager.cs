using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Includes;

public class OilPuddleManager {

	private static OilPuddleManager _instance = null;

    private static CharacterMove _playerMove;
    private static List<OilPuddle> _activeOilPuddles;
    private static GameObject _oilPuddle;
    private static float distanceThreshold;

    public GameObject OilPuddle { get { return _oilPuddle;} }

    public float internalCooldown()
    {
        return 1/(_playerMove.MoveSpeed*3);
    }

    public void removeOilPuddle(OilPuddle puddle)
    {
        foreach (OilPuddle item in puddle._connected)
        {
            item._connected.Remove(puddle);
        }
        _activeOilPuddles.Remove(puddle);
        GameObject.Destroy(puddle.gameObject);
    }

    public void addOilPuddle(GameObject oilPuddle)
    {
        checkDistanceToPuddles(oilPuddle);
    }

    private void checkDistanceToPuddles(GameObject oilPuddle)
    {
        OilPuddle objScript = oilPuddle.GetComponent<OilPuddle>();
        foreach (OilPuddle item in _activeOilPuddles)
	    {
            if (closeEnough(item.transform.position, oilPuddle.transform.position))
            {
                addConnected(item, objScript);
            }
	    }
        objScript.checkIfShouldBurn();
        _activeOilPuddles.Add(objScript);
    }

    private bool closeEnough(Vector3 positionOld, Vector3 positionNew)
    {
        return Vector2.Distance(new Vector2(positionOld.x, positionOld.z), new Vector2(positionNew.x, positionNew.z)) < distanceThreshold;
    }

    private void addConnected(OilPuddle target, OilPuddle current)
    {
        target._connected.Add(current);
        current._connected.Add(target);
    }

    #region Initialization
		
    protected OilPuddleManager() { }
    // Singleton pattern implementation
    public static OilPuddleManager Instance { get { if (_instance == null) { _instance = new OilPuddleManager(); init(); } return _instance; } }

    private static void init()
    {
    }

    public void sceneInit()
    {
        _activeOilPuddles = new List<OilPuddle>();
        _playerMove = GameObject.FindWithTag("Player").GetComponent<CharacterMove>();
        _oilPuddle = GameManager.Instance.OilPuddle;
        distanceThreshold = 0.4f;
    }
 
	#endregion
}
