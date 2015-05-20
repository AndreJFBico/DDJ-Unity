using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowIDInEndGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text += LoggingManager.Instance.ID;
	}
}
