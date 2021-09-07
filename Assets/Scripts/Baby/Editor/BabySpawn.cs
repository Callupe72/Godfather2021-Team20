using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Spawn))]
public class BabySpawn : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Spawn myScript = (Spawn)target;
        if (GUILayout.Button("Spawn a Baby"))
        {
            myScript.SpawnABaby();
        }
    }
}
