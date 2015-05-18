using UnityEngine;
using System.Collections;
using System.IO;
using Includes;

public class LoggingEntry
{
    protected string filePath;

    public string Filepath { get { return filePath; } }

    public LoggingEntry(string path)
    {
        filePath = path;
    }

    public virtual void writeEntry(string enemyName, Elements enemyType, string projectileName, Elements projectileType) { }
    public virtual void writeEntry(string abilityName, Elements abilityType, string zone, string zoneType) { }

    protected void addTextToFile(string text)
    {
        File.AppendAllText(filePath, text);
    }

    public virtual void wrapUp() {}
}
