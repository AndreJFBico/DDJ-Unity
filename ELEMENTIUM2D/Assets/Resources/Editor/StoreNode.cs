using UnityEngine;
using System.Collections;
using Includes;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(SkillTreeNode))]
public class StoreNode : Editor
{
    string[] _choices = GameManager.Instance.StatNames.ToArray();
    int _choiceIndex = 0;

    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();
        var node = target as SkillTreeNode;
        // Update the selected choice in the underlying object
        _choiceIndex = node.getPreviousChoiceIndex();
        node.setVariableBeingChanged(_choices[_choiceIndex]);
        _choiceIndex = EditorGUILayout.Popup(_choiceIndex, _choices);
        node.setPreviousChoiceIndex(_choiceIndex);
        // Save the changes back to the object

        EditorUtility.SetDirty(target);
    }
}