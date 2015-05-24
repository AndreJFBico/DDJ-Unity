using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Includes;

public class LoggingManager
{
    private static LoggingManager _instance = null;

    //List of all filePaths -> add them in init
    private static List<LoggingEntry> allEntries;
    private static string folder = "Logging/";

    private static float startTime;
    private static float respawnTime;
    private static float endTime;
    private static float numRoomsSearched;
    private static float numTreasuresObtained;
    private static float timeToFire;
    private static float timeToEarth;
    private static float numMaxMultiplier;
    private static string id;

    private static bool started = false;
    private static bool alreadyWrapped = false;
    private static string PCprefix = "A";

    #region Getters
    public float StartTime { get { return startTime; } set { startTime = value; } }
    public float EndTime { get { return endTime; } set { endTime = value; } }
    public float RespawnTime { get { return respawnTime; } set { respawnTime = value; } }
    public string ID { get { return id; } set { id = value; } }
    public float RoomSearched { get { return numRoomsSearched; } set { numRoomsSearched = value; } }
    public float TreasuresGained { get { return numTreasuresObtained; } set { numTreasuresObtained = value; } }
    public float TimeToFire { get { return timeToFire; } set { timeToFire = value - startTime; } }
    public float TimeToEarth { get { return timeToEarth; } set { timeToEarth = value - startTime; } }
    public bool Started { get { return started; } set { started = value; } }

    public float MaxMultiplier { get { return numMaxMultiplier; } set { numMaxMultiplier = numMaxMultiplier < value ? value : numMaxMultiplier; } }
    #endregion

    #region Initialization

    protected LoggingManager() { }
    // Singleton pattern implementation
    public static LoggingManager Instance { get { if (_instance == null) { _instance = new LoggingManager(); init(); } return _instance; } }

    private static void init()
    {
        startTime = Time.time;
        endTime = 0;
        numMaxMultiplier = 0;
        timeToFire = 0;
        timeToEarth = 0;
        id = "";

        allEntries = new List<LoggingEntry>();

        #if (!UNITY_WEBPLAYER)
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder); 
            }
        #endif
        //add here the filePaths you want to use
        allEntries.Add(new NumTypeEnemieAndAbility(folder + "NumTypeEnemiesAndSkillsUsed.txt"));
        allEntries.Add(new NumTypeAbilityPerZone(folder + "NumTypeAbilitesPerZone.txt"));
        allEntries.Add(new StatsPerDeath(folder + "StatsPerDeath.txt"));
    }

    public void sceneInit()
    {
        respawnTime = Time.time;
        numRoomsSearched = 0;
        numTreasuresObtained = 0;
    }

    #endregion

    public LoggingEntry getEntry(System.Type type)
    {
        foreach (LoggingEntry entry in allEntries)
        {
            if(entry.GetType() == type)
            {
                return entry;
            }
        }
        return null;
    }

    public int numEnemiesKilled()
    {
        return getEntry(typeof(NumTypeEnemieAndAbility)).numEnemiesKilled();
    }

    #region WrapUp
    public void wrapUp()
    {
        foreach (LoggingEntry entry in allEntries)
        {
            entry.wrapUp();
        }
        endTime = Time.time;
        concatenateFiles();
        alreadyWrapped = true;
        Application.LoadLevel("EndScreen");
    }

    private void globalStatsWrapUp(string filePath)
    {
        File.AppendAllText(filePath,
            "Total Time: " + timeFormat(endTime - startTime) + "\r\n"
               + "Time Fighting: " + timeFormat(((StatsPerDeath)getEntry(typeof(StatsPerDeath))).getTotalTimeFighting()) + "\r\n"
               + "Total Deaths: " + ((StatsPerDeath)getEntry(typeof(StatsPerDeath))).Deaths + "\r\n"
               + "Total Rooms Cleared: " + ((StatsPerDeath)getEntry(typeof(StatsPerDeath))).getTotalRoomsCleared() + "\r\n"
               + "Total Treasures Gained: " + ((StatsPerDeath)getEntry(typeof(StatsPerDeath))).getTotalTreasuresGained() + " \r\n"
               + "Total SkillTree Points Gained: " + GameManager.Instance.Stats.lim_points + " \r\n"
               + "Maximum Multiplier Obtained: " + numMaxMultiplier + " \r\n"
               + "Time to Fire: " + timeFormat(timeToFire) + " \r\n"
               + "Time to Earth: " + timeFormat(timeToEarth) + " \r\n"
               );
    }

    public string statDump()
    {
        return ("Total Time: " + timeFormat(endTime - startTime) + "\r\n"
               + "Time Fighting: " + timeFormat(((StatsPerDeath)getEntry(typeof(StatsPerDeath))).getTotalTimeFighting()) + "\r\n"
               + "Total Deaths: " + ((StatsPerDeath)getEntry(typeof(StatsPerDeath))).Deaths + "\r\n"
               + "Total Rooms Cleared: " + ((StatsPerDeath)getEntry(typeof(StatsPerDeath))).getTotalRoomsCleared() + "\r\n"
               + "Total Treasures Gained: " + ((StatsPerDeath)getEntry(typeof(StatsPerDeath))).getTotalTreasuresGained() + " \r\n"
               + "Total SkillTree Points Gained: " + GameManager.Instance.Stats.lim_points + " \r\n"
               + "Maximum Multiplier Obtained: " + numMaxMultiplier + " \r\n");
    }

    public string timeFormat(float seconds)
    {
        return "" + (int)(seconds / 3600) + ":" + (int)(seconds / 60) + ":" + (int)(seconds % 60); 
    }

    private void concatenateFiles()
    {
        string finalFilePath = folder + "FinalLog" + PCprefix;
        int i = 0;
        while (File.Exists(finalFilePath + i + ".txt"))
        {
            i++;
        }
        string filePath = finalFilePath + i + ".txt";
        ID = PCprefix + i;
        (File.Create(filePath)).Close();

        globalStatsWrapUp(filePath);
        foreach (LoggingEntry entry in allEntries)
        {
            string path = entry.Filepath;
            #if (!UNITY_WEBPLAYER)
            File.AppendAllText(filePath, "#########################################################\r\n" + path + "\r\n" + "#########################################################\r\n");
                File.AppendAllText(filePath, File.ReadAllText(path));
            #endif
        }
    } 
    #endregion

}
