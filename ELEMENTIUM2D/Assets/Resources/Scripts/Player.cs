using UnityEngine;
using System.Collections;
using Includes;

public class Player : Agent {

    public int currentElement = (int)Elements.NEUTRAL;


    public override void takeDamage(float amount, Elements type)
    {
        switch (type)
        {
            case Elements.NEUTRAL:
                health -= amount * (1 - ((defence + waterResist + earthResist + fireResist) / 100.0f));
                break;

            case Elements.FIRE:
                health -= amount * (1 - ((defence + fireResist) / 100.0f));
                break;

            case Elements.EARTH:
                health -= amount * (1 - ((defence + earthResist) / 100.0f));
                break;

            case Elements.FROST:
                health -= amount * (1 - ((defence + waterResist) / 100.0f));
                break;

            default:
                break;
        }
        if (health <= 0)
        {
            Application.Quit();
        }
    }
}
