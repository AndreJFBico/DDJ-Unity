using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(StoreUi))]
public class StoreUiGenerator : Editor
{
    private StoreUi storeUI;


    void OnEnable()
    {

    }

    public override void OnInspectorGUI()
    {
        storeUI = (StoreUi)target;
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("values"), true);
        serializedObject.ApplyModifiedProperties();
        if(GUILayout.Button("Generate Ui"))
        {
            storeUI.GenerateUi();
        }
        if (GUILayout.Button("Reset"))
        {
            storeUI.Reset();
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }


}
