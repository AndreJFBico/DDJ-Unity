using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Includes;

public class RandomTreasureChest : TemporaryTreasureChest {

    private List<string> stats;
    private float increase;

    public override void Start()
    {
        base.Start();
        increase = 1.02f; // 10%
        stats = new List<string>();
        stats.Add("maxHealth");
        stats.Add("damage");
        stats.Add("defence");
    }

    public override void applyEffect()
    {
        Player player = GameManager.Instance.Player.GetComponent<Player>();
        int random = Random.Range(0, stats.Count);

        float intensity = increase;
        switch (stats[random])
        {
            case "maxHealth":
                intensity = updateEffectIntesity(false);
                GameManager.Instance.changeStatVariable("maxHealth", intensity, MathOperations.MAXHP);
                break;
            case "damage":
                intensity = updateEffectIntesity(false);
                GameManager.Instance.changeStatVariable("damage", intensity, MathOperations.MUL);
                break;
            case "defence":
                intensity = updateEffectIntesity(true);
                GameManager.Instance.changeStatVariable("defence", intensity, MathOperations.DEFENCE);
                break;
            default:
                break;
        }

        FloatingText.Instance.createFloatingText(transform, stats[random] + " increased by " + (intensity-1)*100 + "%", Color.yellow);
        
        base.applyEffect();
    }

    private float updateEffectIntesity(bool isDefence)
    {
        int index = PlayerStats.multiplierLevelIndex();

        if(isDefence)
        {
            return ((index / 2) + 1);
        }

        float intensity = increase;
        for(int i = 0 ; i < index; i++)
        {
            intensity += (increase - 1)/2;
        }
        return intensity;
    }
}
