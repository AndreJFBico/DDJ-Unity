using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Point2Mesh))]
public class Point2MeshInspector : Editor {

    public override void OnInspectorGUI()
    {
        Point2Mesh myScript = (Point2Mesh)target;
        if(GUILayout.Button("Generate Mesh"))
        {
            myScript.GenerateMesh();
        }
        if (GUILayout.Button("Reset"))
        {
            myScript.Reset();
        }
    }
}
