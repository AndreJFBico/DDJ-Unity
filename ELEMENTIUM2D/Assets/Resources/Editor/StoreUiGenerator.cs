using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(StoreUi))]
public class StoreUiGenerator : Editor
{
    int horizontalElements = 1;
    int verticalElements = 1;

    public override void OnInspectorGUI()
    {
        int hE = EditorGUILayout.IntField("Number of Horizontal Elements:", horizontalElements);
        int vE = EditorGUILayout.IntField("Number of Vertical Elements:", verticalElements);
        horizontalElements = hE;
        verticalElements = vE;
        StoreUi myScript = (StoreUi)target;
        if (GUILayout.Button("Generate Ui"))
        {
            myScript.GenerateUi(hE, vE);
        }
        if (GUILayout.Button("Reset"))
        {
            myScript.Reset();
        }
    }
}
