using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowStatsInEndGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = LoggingManager.Instance.statDump();
	}
	
}
