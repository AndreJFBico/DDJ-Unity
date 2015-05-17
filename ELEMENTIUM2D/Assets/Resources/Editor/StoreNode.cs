using UnityEngine;
using System.Collections;
using Includes;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(SkillTreeNode))]
public class StoreNode : Editor
{
    string[] _choices = GameManager.Instance.StatNames.ToArray();
    private int _choiceIndex = 0;

    public SerializedProperty abilitychangeString;

    void OnEnable()
    {
        abilitychangeString = serializedObject.FindProperty("variableBeingChanged");
    }

    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();
        var node = target as SkillTreeNode;
        if (!node.changeAbilityNode)
        {
            // Update the selected choice in the underlying object
            _choiceIndex = node.getPreviousChoiceIndex();
            node.setVariableBeingChanged(_choices[_choiceIndex]);
            _choiceIndex = EditorGUILayout.Popup(_choiceIndex, _choices);
            node.setPreviousChoiceIndex(_choiceIndex);
            // Save the changes back to the object
        }
        else
        {
            serializedObject.Update();
            _choiceIndex = 0;
            node.setVariableBeingChanged(abilitychangeString.stringValue);
            abilitychangeString.stringValue = EditorGUILayout.TextArea(abilitychangeString.stringValue, GUILayout.MaxHeight(75));
            serializedObject.ApplyModifiedProperties();
        }

        if (GUILayout.Button("Generate Line Renderers"))
        {
            node.generateLineRenderers();
        }

        EditorUtility.SetDirty(target);
    }
}