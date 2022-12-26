
using UnityEditor;
 
[CustomEditor(typeof(ToggleSwitchButton))]
public class MenuButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SerializedObject so = new SerializedObject(target);
        SerializedProperty sprites = so.FindProperty("stateSprites");
 
        EditorGUILayout.PropertyField(sprites, true);

        so.ApplyModifiedProperties();
        DrawDefaultInspector();
    }
}
