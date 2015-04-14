using UnityEngine;
using System.Collections;
using Includes;
using UnityEditor;

[CustomEditor(typeof(SkillTreeManager))]
public class SkilltreeManagerSetup : Editor {

    public override void OnInspectorGUI()
    {
        SkillTreeManager manager = target as SkillTreeManager;
        if (GUILayout.Button("SetupInCode"))
        {
            manager.setupHiearchy();
        }
        if(GUILayout.Button("ReformFromCode"))
        {
            manager.setupHiearchyBasedOfCode();
        }
    }
}
