using UnityEditor;

[CustomEditor(typeof(HealthBar))]
public class HealthBarInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.HelpBox("HealthBar ImageType Must be Filled and FillMethod Must Be Horizontal | man kheili khafanam :D ", MessageType.Info);
        

    }
}
