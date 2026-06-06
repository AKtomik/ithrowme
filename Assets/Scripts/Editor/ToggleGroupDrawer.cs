#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ToggleGroupAttribute))]
public class ToggleGroupDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ToggleGroupAttribute attr = (ToggleGroupAttribute)attribute;
        property.boolValue = EditorGUILayout.BeginToggleGroup(
            new GUIContent(attr.groupName), property.boolValue
        );
        EditorGUILayout.EndToggleGroup();
    }
}
#endif