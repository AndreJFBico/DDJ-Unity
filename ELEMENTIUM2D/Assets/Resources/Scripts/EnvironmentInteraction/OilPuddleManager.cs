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
                addAfter(item, objScript);
                break;
            }
	    }
        _activeOilPuddles.Add(objScript);
    }

    private bool closeEnough(Vector3 positionOld, Vector3 positionNew)
    {
        return Vector2.Distance(new Vector2(positionOld.x, positionOld.z), new Vector2(positionNew.x, positionNew.z)) < distanceThreshold;
    }

    private void addAfter(OilPuddle target, OilPuddle current)
    {
        current.previous = target;
        current.next = target.next;
        if(target.next != null)
            target.next.previous = current;
        target.next = current;
    }

    #region Initialization
		
    protected OilPuddleManager() { }
    // Singleton pattern implementation
    public static OilPuddleManager Instance { get { if (_instance == null) { _instance = new OilPuddleManager(); init(); } return _instance; } }

    private static void init()
    {
        _activeOilPuddles = new List<OilPuddle>();
        _playerMove = GameObject.FindWithTag("Player").GetComponent<CharacterMove>();
        _oilPuddle = GameManager.Instance.OilPuddle;
        distanceThreshold = 0.5f;
    }
 
	#endregion
}
