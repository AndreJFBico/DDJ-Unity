using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Includes;
using Logging;

public class NumTypeAbilityPerZone : LoggingEntry
{
    private Dictionary<string, StringTypeStringType> stats = new Dictionary<string, StringTypeStringType>();

    public string Filepath { get { return filePath; } }

    public NumTypeAbilityPerZone(string path) : base(path) { }

    public override void writeEntry(string abilityName, Elements abilityType, string zone, Elements zoneType)
    {
        base.writeEntry(abilityName, abilityType, zone, zoneType);
        // Ignoring zone name by default theres no reason to use the zone name for logging
        string key = abilityName + abilityType + zoneType;
        if (!stats.ContainsKey(key))
        {
            stats.Add(key, new StringTypeStringType(abilityName, abilityType, zone, zoneType));
        }
        else
        {
            stats[key].amount = stats[key].amount + 1;
        }
    }

    public override void wrapUp()
    {
        foreach (KeyValuePair<string, StringTypeStringType> pair in stats)
        {
            addTextToFile("Amount: " + pair.Value.amount + "|" + pair.Value._s1 + "|" + System.Enum.GetName(typeof(Elements), pair.Value._s2) + "||" + System.Enum.GetName(typeof(Elements), pair.Value._s4) + "\r\n");
        }
    }
}
