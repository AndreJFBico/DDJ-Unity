using System.IO;
using UnityEngine;
using System.Collections;
using Includes;

public abstract class LoggingEntry
{
    protected string filePath;
    protected FileStream stream;

    public string Filepath { get { return filePath; } }

    public LoggingEntry(string path)
    {
        #if (!UNITY_WEBPLAYER)
            filePath = path;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            stream = File.Create(filePath);
            stream.Close();
        #endif
    }

    public virtual void writeEntry(string enemyName, Elements enemyType, string projectileName, Elements projectileType) { }
    public virtual void writeEntry(string abilityName, Elements abilityType, string zone, string zoneType) { }
    public virtual void writeEntry() { }

    public virtual int numEnemiesKilled() { return -1; }

    protected void addTextToFile(string text)
    {
        #if (!UNITY_WEBPLAYER)
            File.AppendAllText(filePath, text);
        #endif
    }

    public virtual void wrapUp() {}
}
