using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Reflection;
using System.IO;
using System;
using TelegraphEffect;

[CustomEditor(typeof(Telegraph))]
public class TelegraphInspector : Editor {

    string[] init = getItemsAt("Assets/Resources/Scripts/Telegraph/Initialization");
    int initIndex = 0;

    string[] damage = getItemsAt("Assets/Resources/Scripts/Telegraph/Damage");
    int damageIndex = 0;

    string[] motion = getItemsAt("Assets/Resources/Scripts/Telegraph/Motion");
    int motionIndex = 0;

    static string[] getItemsAt(string path)
    {
        List<string> items = new List<string>();
        DirectoryInfo dir = new DirectoryInfo(path);
        FileInfo[] info = dir.GetFiles("*.*");
        foreach (FileInfo f in info)
        {
            if (f.Extension == ".cs")
            {
                string tempName = f.Name;
                //Debug.Log("tempName = " + tempName);
                string extension = f.Extension;
                //Debug.Log("extention = " + extension);
                string strippedName = tempName.Replace(extension, "");
                //Debug.Log(strippedName + " Is in the Directory");
                items.Add(strippedName);
            }
        }
        return items.ToArray();
    }

    public static Type GetTypeByName(string name)
    {
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.Name == name)
                    return type;
            }
        }

        return null;
    }




    public override void OnInspectorGUI()
    {

        // Draw the default inspector
        DrawDefaultInspector();
        var telegraph = target as Telegraph;

                

        // INIT
        initIndex = telegraph.initIndex;
        telegraph.initScrpt = init[initIndex];
        initIndex = EditorGUILayout.Popup(initIndex, init);
        telegraph.initIndex = initIndex;


        // DAMAGE
        damageIndex = telegraph.damageIndex;
        telegraph.damScrpt = damage[damageIndex];
        damageIndex = EditorGUILayout.Popup(damageIndex, damage);
        telegraph.damageIndex = damageIndex;

        // MOTION
        motionIndex = telegraph.motionIndex;
        telegraph.motScrpt = motion[motionIndex];
        motionIndex = EditorGUILayout.Popup(motionIndex, motion);
        telegraph.motionIndex = initIndex;

        EditorUtility.SetDirty(target);
    }
}