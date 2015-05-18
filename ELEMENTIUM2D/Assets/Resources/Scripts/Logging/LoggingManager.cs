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

    #region Initialization

    protected LoggingManager() { }
    // Singleton pattern implementation
    public static LoggingManager Instance { get { if (_instance == null) { _instance = new LoggingManager(); init(); } return _instance; } }

    private static void init()
    {
        allEntries = new List<LoggingEntry>();

        //add here the filePaths you want to use
        allEntries.Add(new LoggingEntry("NumTypeEnemiesAndSkillsUsed.txt"));
        allEntries.Add(new LoggingEntry("NumTypeAbilitesPerZone.txt"));

        foreach (LoggingEntry entry in allEntries)
        {
            if (File.Exists(entry.Filepath))
            {
                File.Delete(entry.Filepath);
            }
            File.Create(entry.Filepath);
        }
    }

    public void sceneInit() { }

    public LoggingEntry getEntry(string filename)
    {
        foreach (LoggingEntry entry in allEntries)
        {
            if(entry.Filepath == filename)
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
        string finalFilePath = "FinalLog";
        int i = 0;
        while (File.Exists(finalFilePath + i))
        {
            i++;
        }
        string filePath = finalFilePath + i + ".txt";
        File.Create(filePath);
        foreach (LoggingEntry entry in allEntries)
        {
            string path = entry.Filepath;
            File.AppendAllText(filePath, "#########################################################\n" + path);
            File.AppendAllText(filePath, File.ReadAllText(path));
        }
    }

    #endregion
}
