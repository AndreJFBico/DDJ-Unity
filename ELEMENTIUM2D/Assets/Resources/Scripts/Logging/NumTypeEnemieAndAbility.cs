using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Includes;
using Logging;

public class NumTypeEnemieAndAbility : LoggingEntry
{
    private Dictionary<string, Dictionary<string, StringTypeStringType>> stats = new Dictionary<string, Dictionary<string, StringTypeStringType>>();
    private Dictionary<string, Dictionary<string, StringTypeStringType>> perDeathStats = new Dictionary<string, Dictionary<string, StringTypeStringType>>();

    public string Filepath { get { return filePath; } }
    public Dictionary<string, Dictionary<string, StringTypeStringType>> PerDeath { get { return perDeathStats; } }

    public NumTypeEnemieAndAbility(string path) : base(path) {}

    #region WriteEntry
    public override void writeEntry(string enemyName, Elements enemyType, string projectileName, Elements projectileType)
    {
        base.writeEntry(enemyName, enemyType, projectileName, projectileType);

        addEntryToGlobal(enemyName, enemyType, projectileName, projectileType);
        addEntryToPerDeath(enemyName, enemyType, projectileName, projectileType);
    }

    private void addEntryToGlobal(string enemyName, Elements enemyType, string projectileName, Elements projectileType)
    {
        string mainKey = projectileName + projectileType;
        string key = enemyName + enemyType;
        Dictionary<string, StringTypeStringType> enemies = new Dictionary<string, StringTypeStringType>();

        if (!stats.ContainsKey(mainKey))
        {
            stats.Add(mainKey, enemies);
            stats[mainKey].Add(key, new StringTypeStringType(enemyName, enemyType, projectileName, projectileType));
            //stats.Add(enemyName + enemyType + projectileName + projectileType, new StringTypeStringType(enemyName, enemyType, projectileName, projectileType));
        }
        else if (!stats[mainKey].ContainsKey(key))
        {
            stats[mainKey].Add(key, new StringTypeStringType(enemyName, enemyType, projectileName, projectileType));
        }
        else
        {
            stats[mainKey][key].amount += 1;
        }
    }
    private void addEntryToPerDeath(string enemyName, Elements enemyType, string projectileName, Elements projectileType)
    {
        string mainKey = projectileName + projectileType;
        string key = enemyName + enemyType;
        Dictionary<string, StringTypeStringType> enemies = new Dictionary<string, StringTypeStringType>();

        if (!perDeathStats.ContainsKey(mainKey))
        {
            perDeathStats.Add(mainKey, enemies);
            perDeathStats[mainKey].Add(key, new StringTypeStringType(enemyName, enemyType, projectileName, projectileType));
            //stats.Add(enemyName + enemyType + projectileName + projectileType, new StringTypeStringType(enemyName, enemyType, projectileName, projectileType));
        }
        else if (!perDeathStats[mainKey].ContainsKey(key))
        {
            perDeathStats[mainKey].Add(key, new StringTypeStringType(enemyName, enemyType, projectileName, projectileType));
        }
        else
        {
            perDeathStats[mainKey][key].amount += 1;
        }
    }
    
    #endregion

    #region Functions used By Others
    public void clearPerDeath() { perDeathStats = new Dictionary<string, Dictionary<string, StringTypeStringType>>(); }

    public override int numEnemiesKilled()
    {
        int sum = 0;
        foreach (string main in stats.Keys)
        {
            foreach (string enemies in stats[main].Keys)
            {
                sum += stats[main][enemies].amount;
            }
        }
        return sum;
    }

    public string printPerDeathStats()
    {
        string result = "";
        bool newAbility = true;
        foreach (string main in perDeathStats.Keys)
        {
            newAbility = true;
            foreach (string enemies in perDeathStats[main].Keys)
            {
                if (newAbility)
                {
                    result += perDeathStats[main][enemies]._s3 + "|" + perDeathStats[main][enemies]._s4 + "\r\n";
                    newAbility = false;
                }
                result += "\t" + "Amount: " + perDeathStats[main][enemies].amount + "|" + perDeathStats[main][enemies]._s1 + "|" + System.Enum.GetName(typeof(Elements), perDeathStats[main][enemies]._s2) + "\r\n";
            }
        }
        return result;
    } 
    #endregion

    public override void wrapUp()
    {
        bool newAbility = true;
        foreach (string main in stats.Keys)
        {
            newAbility = true;
            foreach (string enemies in stats[main].Keys)
            {
                if(newAbility)
                {
                    addTextToFile(stats[main][enemies]._s3 + "|" + stats[main][enemies]._s4 + "\r\n");
                    newAbility = false;
                }
                addTextToFile("\t" + "Amount: " + stats[main][enemies].amount + "|" + stats[main][enemies]._s1 + "|" + System.Enum.GetName(typeof(Elements), stats[main][enemies]._s2) + "\r\n");
            }
        }
    }
}
