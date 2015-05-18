using UnityEngine;
using System.Collections;
using System.IO;
using Includes;

public abstract class LoggingEntry
{
    protected string filePath;
    protected FileStream stream;

    public string Filepath { get { return filePath; } }

    public LoggingEntry(string path)
    {
        filePath = path;
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        stream = File.Create(filePath);
        stream.Close();
    }

    public virtual void writeEntry(string enemyName, Elements enemyType, string projectileName, Elements projectileType) { }
    public virtual void writeEntry(string abilityName, Elements abilityType, string zone, string zoneType) { }

    protected void addTextToFile(string text)
    {
        File.AppendAllText(filePath, text);
    }

    public virtual void wrapUp() {}
}
