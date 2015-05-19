using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Includes;

public class AvgAliveSKPointsEnemiesKilled : LoggingEntry
{
    private int numDeaths;
    private List<int> _enemiesKilledPerDeath;
    private List<float> _numberPointsSpentOnSKPerDeath;
    private List<float> _timePerDeath;

    public string Filepath { get { return filePath; } }

    public AvgAliveSKPointsEnemiesKilled(string path)
        : base(path)
    {
        numDeaths = 0;
        _enemiesKilledPerDeath = new List<int>();
        _numberPointsSpentOnSKPerDeath = new List<float>();
        _timePerDeath = new List<float>();
        _numberPointsSpentOnSKPerDeath.Add(0);
    }

    //To be called when player dies
    public override void writeEntry()
    {
        int numEnemiesKilled = LoggingManager.Instance.numEnemiesKilled();
        float timePassed = Time.realtimeSinceStartup;
        //get number killed from NumTypeEnemieAndAbility
        //get number Points on SK from Auxiliars
        base.writeEntry();
        
        if (numDeaths != 0)
            numEnemiesKilled -= _enemiesKilledPerDeath[numDeaths - 1];

        //SK points corresponding to the next playthrough are added when player dies
        _numberPointsSpentOnSKPerDeath.Add(GameManager.Instance.Stats.lim_points);
        _enemiesKilledPerDeath.Add(numEnemiesKilled);
        _timePerDeath.Add(Time.time - LoggingManager.Instance.RespawnTime);

        numDeaths++;
    }

    public override void wrapUp()
    {
        if (numDeaths == 0)
            writeEntry();
        for (int i = 0; i < numDeaths; i++)
        {
            addTextToFile("Death Number: " + i + " | " 
                + "Time Alive: " + _timePerDeath[i] + " | " 
                + "Num Points on SK: " + _numberPointsSpentOnSKPerDeath[i] + " | "
                + "Number Enemies Killed: " + _enemiesKilledPerDeath[i] + " \r\n ");
        }    
    }
}
