using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
    private static float numSkillTreePoints;
    private static float numMaxMultiplier;

    #region Getters
    public float StartTime { get { return startTime; } set { startTime = value; } }
    public float EndTime { get { return endTime; } set { endTime = value; } }
    public float RespawnTime { get { return respawnTime; } set { respawnTime = value; } }

    #endregion

    #region Initialization

    protected LoggingManager() { }
    // Singleton pattern implementation
    public static LoggingManager Instance { get { if (_instance == null) { _instance = new LoggingManager(); init(); } return _instance; } }

    private static void init()
    {
        startTime = 0;
        allEntries = new List<LoggingEntry>();

        //add here the filePaths you want to use
        allEntries.Add(new NumTypeEnemieAndAbility(folder + "NumTypeEnemiesAndSkillsUsed.txt"));
        allEntries.Add(new NumTypeAbilityPerZone(folder + "NumTypeAbilitesPerZone.txt"));
        allEntries.Add(new AvgAliveSKPointsEnemiesKilled(folder + "AvgAliveSKPointsEnemiesKilled.txt"));
    }

    public void sceneInit() { }

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
        endTime = Time.time;
        foreach (LoggingEntry entry in allEntries)
        {
            entry.wrapUp();
        }
        concatenateFiles();
    }

    private void concatenateFiles()
    {
        string finalFilePath = folder + "FinalLog";
        int i = 0;
        while (File.Exists(finalFilePath + i + ".txt"))
        {
            i++;
        }
        string filePath = finalFilePath + i + ".txt";
        (File.Create(filePath)).Close();
        foreach (LoggingEntry entry in allEntries)
        {
            string path = entry.Filepath;
            File.AppendAllText(filePath, "#########################################################\r\n" + path + "\r\n");
            File.AppendAllText(filePath, File.ReadAllText(path));
        }
    } 
    #endregion

}
