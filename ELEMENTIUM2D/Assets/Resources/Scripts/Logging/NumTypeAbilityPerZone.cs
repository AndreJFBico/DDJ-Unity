using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Includes;
using Logging;

public class NumTypeAbilityPerZone : LoggingEntry
{
    private Dictionary<string, Dictionary<string, StringTypeStringType>> stats = new Dictionary<string, Dictionary<string, StringTypeStringType>>();
    private Dictionary<string, Dictionary<string, StringTypeStringType>> perDeathStats = new Dictionary<string, Dictionary<string, StringTypeStringType>>();

    public string Filepath { get { return filePath; } }
    public Dictionary<string, Dictionary<string, StringTypeStringType>> PerDeath { get { return perDeathStats; } }

    public NumTypeAbilityPerZone(string path) : base(path) { }

    public override void writeEntry(string abilityName, Elements abilityType, string zone, Elements zoneType)
    {
        base.writeEntry(abilityName, abilityType, zone, zoneType);
        // Ignoring zone name by default theres no reason to use the zone name for logging
        addEntryToGlobal(abilityName, abilityType, zone, zoneType);
        addEntryToPerDeath(abilityName, abilityType, zone, zoneType);
    }

    private void addEntryToGlobal(string abilityName, Elements abilityType, string zone, Elements zoneType)
    {
        string mainKey = System.Enum.GetName(typeof(Elements), zoneType);
        string key = abilityName + abilityType;
        Dictionary<string, StringTypeStringType> abilities = new Dictionary<string, StringTypeStringType>();

        if (!stats.ContainsKey(mainKey))
        {
            stats.Add(mainKey, abilities);
            stats[mainKey].Add(key, new StringTypeStringType(abilityName, abilityType, zone, zoneType));
            //stats.Add(enemyName + enemyType + projectileName + projectileType, new StringTypeStringType(enemyName, enemyType, projectileName, projectileType));
        }
        else if (!stats[mainKey].ContainsKey(key))
        {
            stats[mainKey].Add(key, new StringTypeStringType(abilityName, abilityType, zone, zoneType));
        }
        else
        {
            stats[mainKey][key].amount += 1;
        }
    }
    private void addEntryToPerDeath(string abilityName, Elements abilityType, string zone, Elements zoneType)
    {
        string mainKey = System.Enum.GetName(typeof(Elements), zoneType);
        string key = abilityName + abilityType;
        Dictionary<string, StringTypeStringType> abilities = new Dictionary<string, StringTypeStringType>();

        if (!perDeathStats.ContainsKey(mainKey))
        {
            perDeathStats.Add(mainKey, abilities);
            perDeathStats[mainKey].Add(key, new StringTypeStringType(abilityName, abilityType, zone, zoneType));
            //stats.Add(enemyName + enemyType + projectileName + projectileType, new StringTypeStringType(enemyName, enemyType, projectileName, projectileType));
        }
        else if (!perDeathStats[mainKey].ContainsKey(key))
        {
            perDeathStats[mainKey].Add(key, new StringTypeStringType(abilityName, abilityType, zone, zoneType));
        }
        else
        {
            perDeathStats[mainKey][key].amount += 1;
        }
    }

    #region Functions used By Others
    public void clearPerDeath() { perDeathStats = new Dictionary<string, Dictionary<string, StringTypeStringType>>(); }

    public string printPerDeathStats()
    {
        string result = "";
        bool newZone = true;
        foreach (string main in stats.Keys)
        {
            newZone = true;
            foreach (string abilities in stats[main].Keys)
            {
                if (newZone)
                {
                    result += main + "\r\n";
                    newZone = false;
                }
                result += "\t" + "Amount: " + stats[main][abilities].amount + "|" + stats[main][abilities]._s1 + "|" + System.Enum.GetName(typeof(Elements), stats[main][abilities]._s2) + "\r\n";
            }
        }
        return result;
    }
    #endregion

    public override void wrapUp()
    {
        bool newZone = true;
        foreach (string main in stats.Keys)
        {
            newZone = true;
            foreach (string abilities in stats[main].Keys)
            {
                if (newZone)
                {
                    addTextToFile(main + "\r\n");
                    newZone = false;
                }
                addTextToFile("\t" + "Amount: " + stats[main][abilities].amount + "|" + stats[main][abilities]._s1 + "|" + System.Enum.GetName(typeof(Elements), stats[main][abilities]._s2) + "\r\n");
            }
        }
    }
}
