using UnityEngine;
using System.Collections;
using Includes;

public class NumTypeEnemieAndAbility : LoggingEntry
{
    public string Filepath { get { return filePath; } }

    public NumTypeEnemieAndAbility(string path) : base(path) {}

    public override void writeEntry(string enemyName, Elements enemyType, string projectileName, Elements projectileType)
    {
        base.writeEntry(enemyName, enemyType, projectileName, projectileType);
        addTextToFile(enemyName + "|" + System.Enum.GetName(typeof(Elements), enemyType) + "||" + projectileName + "|" + System.Enum.GetName(typeof(Elements), projectileType));
    }

    public void wrapUp()
    {

    }
}
