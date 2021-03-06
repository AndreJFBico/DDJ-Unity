﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Includes;

public class StatsPerDeath : LoggingEntry
{
    private int numDeaths;
    private List<int> _enemiesKilledPerDeath;
    private List<float> _numberPointsSpentOnSKPerDeath;
    private List<float> _timePerDeath;
    private List<float> _treasuresPerDeath;
    private List<float> _roomsClearedPerDeath;
    private List<string> _enemiesPerDeath;
    private List<string> _abilitiesZonePerDeath;

    public string Filepath { get { return filePath; } }
    public int Deaths { get { return numDeaths; } }

    public StatsPerDeath(string path)
        : base(path)
    {
        numDeaths = 0;
        _enemiesKilledPerDeath = new List<int>();
        _numberPointsSpentOnSKPerDeath = new List<float>();
        _timePerDeath = new List<float>();
        _treasuresPerDeath = new List<float>();
        _roomsClearedPerDeath = new List<float>();
        _enemiesPerDeath = new List<string>();
        _abilitiesZonePerDeath = new List<string>();

        _numberPointsSpentOnSKPerDeath.Add(0);
    }

    //To be called when player dies
    public override void writeEntry()
    {
        int numEnemiesKilled = LoggingManager.Instance.numEnemiesKilled();
        //get number killed from NumTypeEnemieAndAbility
        //get number Points on SK from Auxiliars
        base.writeEntry();
        
        if (numDeaths != 0)
            numEnemiesKilled -= _enemiesKilledPerDeath[numDeaths - 1];

        //SK points corresponding to the next playthrough are added when player dies
        _numberPointsSpentOnSKPerDeath.Add(GameManager.Instance.Stats.lim_points);
        _enemiesKilledPerDeath.Add(numEnemiesKilled);
        _timePerDeath.Add(Time.time - LoggingManager.Instance.RespawnTime);
        _treasuresPerDeath.Add(LoggingManager.Instance.TreasuresGained);
        _roomsClearedPerDeath.Add(LoggingManager.Instance.RoomSearched);
        _enemiesPerDeath.Add(printPerDeathEnemies());
        _abilitiesZonePerDeath.Add(printPerDeathAbilities());

        numDeaths++;
    }

    public float getTotalTimeFighting()
    {
        float totalTime = 0;
        foreach (float item in _timePerDeath)
        {
            totalTime += item;
        }
        return totalTime;
    }

    public float getTotalRoomsCleared()
    {
        float totalRooms = 0;
        foreach (float item in _roomsClearedPerDeath)
        {
            totalRooms += item;
        }
        return totalRooms;
    }

    public float getTotalTreasuresGained()
    {
        float totalTreasures = 0;
        foreach (float item in _treasuresPerDeath)
        {
            totalTreasures += item;
        }
        return totalTreasures;
    }

    private string printPerDeathEnemies()
    {
        string result = ((NumTypeEnemieAndAbility)LoggingManager.Instance.getEntry(typeof(NumTypeEnemieAndAbility))).printPerDeathStats();
        ((NumTypeEnemieAndAbility)LoggingManager.Instance.getEntry(typeof(NumTypeEnemieAndAbility))).clearPerDeath();
        return result;
    }

    private string printPerDeathAbilities()
    {
        string result = ((NumTypeAbilityPerZone)LoggingManager.Instance.getEntry(typeof(NumTypeAbilityPerZone))).printPerDeathStats();
        ((NumTypeAbilityPerZone)LoggingManager.Instance.getEntry(typeof(NumTypeAbilityPerZone))).clearPerDeath();
        return result;
    }

    public override void wrapUp()
    {
        if (numDeaths == 0)
            writeEntry();
        for (int i = 0; i < numDeaths; i++)
        {
            addTextToFile(
                "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\r\n" 
               + "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\r\n" 
               + "DEATH NUMBER: " + i + "\r\n"
               + "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\r\n"
               + "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\r\n" 
               + "\t" + "Time Alive: " + LoggingManager.Instance.timeFormat(_timePerDeath[i]) + "\r\n"
               + "\t" + "Num Points on SK: " + _numberPointsSpentOnSKPerDeath[i] + "\r\n"
               + "\t" + "Number Enemies Killed: " + _enemiesKilledPerDeath[i] + " \r\n"
               + "\t" + "Rooms Cleared: " + _roomsClearedPerDeath[i] + " \r\n"
               + "\t" + "Treasures Gained: " + _treasuresPerDeath[i] + " \r\n"
               + "\r\n" + "~~~~~~~~~~~~~~~~ KILLS + ABILITIES ~~~~~~~~~~~~~~~~\r\n"
               + _enemiesPerDeath[i]
               + "\r\n" + "~~~~~~~~~~~~~~~~ ABILITIES PER DEATH ~~~~~~~~~~~~~~~~\r\n"
               + _abilitiesZonePerDeath[i]
            );
        }    
    }
}
