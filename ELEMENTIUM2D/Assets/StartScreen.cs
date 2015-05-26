using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {

	public void startGame()
    {
        Application.LoadLevel("Intro");
    }

    void Update()
    {
        if(Input.GetButtonDown("UI_ENTER"))
        {
            startGame();
        }
    }
}
