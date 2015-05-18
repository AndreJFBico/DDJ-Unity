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


    #region Initialization

    protected LoggingManager() { }
    // Singleton pattern implementation
    public static LoggingManager Instance { get { if (_instance == null) { _instance = new LoggingManager(); init(); } return _instance; } }

    private static void init()
    {
        allEntries = new List<LoggingEntry>();

        //add here the filePaths you want to use
        allEntries.Add(new NumTypeEnemieAndAbility(folder + "NumTypeEnemiesAndSkillsUsed.txt"));
        allEntries.Add(new NumTypeAbilityPerZone(folder + "NumTypeAbilitesPerZone.txt"));
    }

    public void sceneInit() { }

    public LoggingEntry getEntry(string filename)
    {
        foreach (LoggingEntry entry in allEntries)
        {
            if(entry.Filepath == folder + filename)
            {
                return entry;
            }
        }
        return null;
    }

    public void wrapUp()
    {
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
        while (File.Exists(finalFilePath + i))
        {
            i++;
        }
        string filePath = finalFilePath + i + ".txt";
        (File.Create(filePath)).Close();
        foreach (LoggingEntry entry in allEntries)
        {
            string path = entry.Filepath;
            File.AppendAllText(filePath, "#########################################################\n" + path + "\n");
            File.AppendAllText(filePath, File.ReadAllText(path));
        }
    }

    #endregion
}
