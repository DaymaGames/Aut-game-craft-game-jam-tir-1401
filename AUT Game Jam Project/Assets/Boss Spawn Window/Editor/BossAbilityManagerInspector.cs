using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BossAbilityManager))]
public class BossAbilityManagerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("Add To List"))
        {
            BossAbilityManager manager = (BossAbilityManager)target;
            manager.wrongMatches.Add(manager.matchToAdd);
        }
    }
}
