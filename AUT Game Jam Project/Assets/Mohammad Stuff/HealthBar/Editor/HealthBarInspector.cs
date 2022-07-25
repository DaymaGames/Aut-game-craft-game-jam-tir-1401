using UnityEditor;

[CustomEditor(typeof(HealthBar))]
public class HealthBarInspector : Editor
{
    public override void OnInspectorGUI()
    {
        //EditorGUILayout.HelpBox()
        base.OnInspectorGUI();

    }
}
