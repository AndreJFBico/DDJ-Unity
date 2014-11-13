using UnityEngine;
using System.Collections;

public class Store : MonoBehaviour
{
    private float wWidth = Screen.width / 2;
    private float wHeight = Screen.height / 2;
    private int healthInc = 0;
    private int attackInc = 0;
    private int defenceInc = 0;
    private int projRangeInc = 0;
    private int fireInc = 0;
    private int earthInc = 0;
    private PlayerStats playerStats;
    private int pointsToSpend = 10;

    // Use this for initialization
    void Start()
    {
        playerStats = (GameObject.FindWithTag("Player") as GameObject).GetComponent<PlayerStats>();
        Time.timeScale = 0;
    }

    void OnGUI()
    {
        GUI.Window(0, new Rect(20, 20, wWidth, wHeight), storeFunc, "Store");
    }

    private void storeFunc(int windowid)
    {
        GUI.Box(new Rect(0, wHeight / 12, wWidth / 2, wHeight / 12), "Available points: " + pointsToSpend);
        GUI.Box(new Rect(0, wHeight *2/ 12, wWidth / 2, wHeight / 12), "Character: ");
        
        //HEALTH CONTROL
        GUI.Box(new Rect(0, wHeight * 3 / 12, wWidth / 2, wHeight / 12), "Health: ");
        int j = 0;
        for (; j < healthInc; j++ )
        {
            GUI.Toggle(new Rect(wWidth * (6+j) / 12, wHeight * 3 / 12, wWidth / 12 , wHeight / 12), true, "");
        }
        for (; j < 3; j++)
        {
            GUI.Toggle(new Rect(wWidth * (6 + j) / 12, wHeight * 3 / 12, wWidth / 12, wHeight / 12), false, "");
        }
        if (GUI.Button(new Rect(wWidth * 10/12, wHeight * 3 / 12, wWidth / 12, wHeight / 12), "-"))
        {
            if(healthInc > 0)
            {
                healthInc--;
                pointsToSpend++;
            }
        }
        if (GUI.Button(new Rect(wWidth * 11 / 12, wHeight * 3 / 12, wWidth / 12, wHeight / 12), "+"))
        {
            if (healthInc < 3)
            {
                healthInc++;
                pointsToSpend--;
            }
        }

        //ATTACK CONTROL
        GUI.Box(new Rect(0, wHeight * 4 / 12, wWidth / 2, wHeight / 12), "Attack: ");
        j = 0;
        for (; j < attackInc; j++)
        {
            GUI.Toggle(new Rect(wWidth * (6 + j) / 12, wHeight * 4 / 12, wWidth / 12, wHeight / 12), true, "");
        }
        for (; j < 3; j++)
        {
            GUI.Toggle(new Rect(wWidth * (6 + j) / 12, wHeight * 4 / 12, wWidth / 12, wHeight / 12), false, "");
        }
        if (GUI.Button(new Rect(wWidth * 10 / 12, wHeight * 4 / 12, wWidth / 12, wHeight / 12), "-"))
        {
            if (attackInc > 0)
            {
                attackInc--;
                pointsToSpend++;
            }
        }
        if (GUI.Button(new Rect(wWidth * 11 / 12, wHeight * 4 / 12, wWidth / 12, wHeight / 12), "+"))
        {
            if (attackInc < 3)
            {
                attackInc++;
                pointsToSpend--;
            }
        }
        
        //Defence Control
        GUI.Box(new Rect(0, wHeight * 5 / 12, wWidth / 2, wHeight / 12), "Defence: ");
        j = 0;
        for (; j < defenceInc; j++)
        {
            GUI.Toggle(new Rect(wWidth * (6 + j) / 12, wHeight * 5 / 12, wWidth / 12, wHeight / 12), true, "");
        }
        for (; j < 3; j++)
        {
            GUI.Toggle(new Rect(wWidth * (6 + j) / 12, wHeight * 5 / 12, wWidth / 12, wHeight / 12), false, "");
        }
        if (GUI.Button(new Rect(wWidth * 10 / 12, wHeight * 5 / 12, wWidth / 12, wHeight / 12), "-"))
        {
            if (defenceInc > 0)
            {
                defenceInc--;
                pointsToSpend++;
            }
        }
        if (GUI.Button(new Rect(wWidth * 11 / 12, wHeight * 5 / 12, wWidth / 12, wHeight / 12), "+"))
        {
            if (defenceInc < 3)
            {
                defenceInc++;
                pointsToSpend--;
            }
        }

        //Projectile Range Control
        GUI.Box(new Rect(0, wHeight * 6 / 12, wWidth / 2, wHeight / 12), "Projectile Range: ");
        j = 0;
        for (; j < projRangeInc; j++)
        {
            GUI.Toggle(new Rect(wWidth * (6 + j) / 12, wHeight * 6 / 12, wWidth / 12, wHeight / 12), true, "");
        }
        for (; j < 3; j++)
        {
            GUI.Toggle(new Rect(wWidth * (6 + j) / 12, wHeight * 6 / 12, wWidth / 12, wHeight / 12), false, "");
        }
        if (GUI.Button(new Rect(wWidth * 10 / 12, wHeight * 6 / 12, wWidth / 12, wHeight / 12), "-"))
        {
            if (projRangeInc > 0)
            {
                projRangeInc--;
                pointsToSpend++;
            }
        }
        if (GUI.Button(new Rect(wWidth * 11 / 12, wHeight * 6 / 12, wWidth / 12, wHeight / 12), "+"))
        {
            if (projRangeInc < 3)
            {
                projRangeInc++;
                pointsToSpend--;
            }
        }

        //Elements
        GUI.Box(new Rect(0, wHeight * 7 / 12, wWidth / 2, wHeight / 12), "Elements: ");
        
        //Fire Element control
        GUI.Box(new Rect(0, wHeight * 8 / 12, wWidth / 2, wHeight / 12), "Fire: ");
        j = 0;
        for (; j < fireInc; j++)
        {
            GUI.Toggle(new Rect(wWidth * (6 + j) / 12, wHeight * 8 / 12, wWidth / 12, wHeight / 12), true, "");
        }
        for (; j < 3; j++)
        {
            GUI.Toggle(new Rect(wWidth * (6 + j) / 12, wHeight * 8 / 12, wWidth / 12, wHeight / 12), false, "");
        }
        if (GUI.Button(new Rect(wWidth * 10 / 12, wHeight * 8 / 12, wWidth / 12, wHeight / 12), "-"))
        {
            if (fireInc > 0)
            {
                fireInc--;
                pointsToSpend++;
            }
        }
        if (GUI.Button(new Rect(wWidth * 11 / 12, wHeight * 8 / 12, wWidth / 12, wHeight / 12), "+"))
        {
            if (fireInc < 3)
            {
                fireInc++;
                pointsToSpend--;
            }
        }

        //Earth Element control
        GUI.Box(new Rect(0, wHeight * 9 / 12, wWidth / 2, wHeight / 12), "Earth: ");
        j = 0;
        for (; j < earthInc; j++)
        {
            GUI.Toggle(new Rect(wWidth * (6 + j) / 12, wHeight * 9 / 12, wWidth / 12, wHeight / 12), true, "");
        }
        for (; j < 3; j++)
        {
            GUI.Toggle(new Rect(wWidth * (6 + j) / 12, wHeight * 9 / 12, wWidth / 12, wHeight / 12), false, "");
        }
        if (GUI.Button(new Rect(wWidth * 10 / 12, wHeight * 9 / 12, wWidth / 12, wHeight / 12), "-"))
        {
            if (earthInc > 0)
            {
                earthInc--;
                pointsToSpend++;
            }
        }
        if (GUI.Button(new Rect(wWidth * 11 / 12, wHeight * 9 / 12, wWidth / 12, wHeight / 12), "+"))
        {
            if (earthInc < 3)
            {
                earthInc++;
                pointsToSpend--;
            }
        }

        //Blank space
        GUI.Box(new Rect(0, wHeight * 10 / 12, wWidth / 2, wHeight / 12), "");

        //Start Game button
        if (GUI.Button(new Rect(0, wHeight * 11 / 12, wWidth / 2, wHeight / 12), "Begin: "))
        {
            playerStats.hp += healthInc;
            playerStats.attack += attackInc;
            playerStats.defence += defenceInc;
            playerStats.projectileRange += projRangeInc;
            playerStats.fireElementLevel += fireInc;
            playerStats.earthElementLevel += earthInc;
            Time.timeScale = 1;
            GameObject.Destroy(gameObject);
        }
    }
}
