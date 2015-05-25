using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Includes;

public class RandomTreasureChest : TemporaryTreasureChest {

    private List<string> stats;
    private float increase;

	void Awake(){
		stats = new List<string>();
	}

    public override void Start()
    {
        base.Start();
        increase = 1.1f; // 10%
        /*stats.Add("maxHealth");
        stats.Add("damage");
        stats.Add("defence");
        stats.Add("lim_points");
        stats.Add("lim_points");
        stats.Add("lim_points");*/
    }

	public void addStats(string stat){
		stats.Add(stat);
	}

    public override void applyEffect()
    {
        LoggingManager.Instance.TreasuresGained += 1;

        Player player = GameManager.Instance.Player;
        int random = Random.Range(0, stats.Count);

        float intensity = increase;
        switch (stats[random])
        {
            case "maxHealth":
                intensity = updateEffectIntesity(false);
                GameManager.Instance.changeStatVariable("maxHealth", intensity, MathOperations.MAXHP);
                effectApplied = stats[random] + " increased by " + (float)System.Math.Round((intensity-1)*100) + "%";
                break;
            case "damage":
                intensity = updateEffectIntesity(false);
                GameManager.Instance.changeStatVariable("damage", intensity, MathOperations.MUL);
                effectApplied = stats[random] + " increased by " + (float)System.Math.Round((intensity-1)*100) + "%";
                break;
            case "defence":
                intensity = updateEffectIntesity(true);
                effectApplied = stats[random] + " increased by " + (float)System.Math.Round(GameManager.Instance.changeStatVariable("defence", intensity, MathOperations.DEFENCE)) + "%";
                break;
            case "lim_points":
                GameManager.Instance.changeStatVariable("lim_points", 1, MathOperations.SUM);
                effectApplied = stats[random] + " increased by " + 1;
                break;
			case "":
				effectApplied = "No more treasures available in this area";
				break;    
			default:
                break;
        }
        FloatingText.Instance.createFloatingText(transform, effectApplied, Color.yellow, 1);
        
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
