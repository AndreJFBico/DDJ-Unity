using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Includes;
using Logging;

public class NumTypeEnemieAndAbility : LoggingEntry
{
    private Dictionary<string, EnemyKilledByProjectile> stats = new Dictionary<string, EnemyKilledByProjectile>();

    public string Filepath { get { return filePath; } }

    public NumTypeEnemieAndAbility(string path) : base(path) {}

    public override void writeEntry(string enemyName, Elements enemyType, string projectileName, Elements projectileType)
    {
        base.writeEntry(enemyName, enemyType, projectileName, projectileType);
        string key = enemyName + enemyType + projectileName + projectileType;
        if (!stats.ContainsKey(key))
        {
            stats.Add(enemyName + enemyType + projectileName + projectileType, new EnemyKilledByProjectile(enemyName, enemyType, projectileName, projectileType));
        }
        else
        {
            stats[key].amount = stats[key].amount + 1;
        }
        
        //addTextToFile(enemyName + "|" + System.Enum.GetName(typeof(Elements), enemyType) + "||" + projectileName + "|" + System.Enum.GetName(typeof(Elements), projectileType) + "\n");
    }

    public override void wrapUp()
    {
        foreach(KeyValuePair<string, EnemyKilledByProjectile> pair in stats)
        {
            addTextToFile("Amount: " + pair.Value.amount  + "|" + pair.Value.enemyName + "|" + System.Enum.GetName(typeof(Elements), pair.Value.enemyType) + "||" + pair.Value.projectileName + "|" + System.Enum.GetName(typeof(Elements), pair.Value.projectileType) + "\r\n");
        }
        //addTextToFile(enemyName + "|" + System.Enum.GetName(typeof(Elements), enemyType) + "||" + projectileName + "|" + System.Enum.GetName(typeof(Elements), projectileType) + "\n");
    }
}
