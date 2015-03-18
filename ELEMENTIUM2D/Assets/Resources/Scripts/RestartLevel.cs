using UnityEngine;
using System.Collections;

public class RestartLevel : MonoBehaviour {

    public void reloadLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
