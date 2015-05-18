using UnityEngine;
using System.Collections;
using Includes;

public class NumTypeAbilityPerZone : LoggingEntry
{
    public string Filepath { get { return filePath; } }

    public NumTypeAbilityPerZone(string path) : base(path) { }

    public override void writeEntry(string abilityName, Elements abilityType, string zone, string zoneType)
    {
        base.writeEntry(abilityName, abilityType, zone, zoneType);
        addTextToFile(abilityName + "|" + System.Enum.GetName(typeof(Elements), abilityType) + "||" + zone + "|" + zoneType + "\n");
    }

    public void wrapUp()
    {

    }
}
