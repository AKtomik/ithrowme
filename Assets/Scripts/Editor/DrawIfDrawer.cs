#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DrawIfAttribute))]
public class DrawIfDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        DrawIfAttribute drawIf = (DrawIfAttribute)attribute;
        SerializedProperty condProp = property.serializedObject.FindProperty(drawIf.conditionField);

        bool enabled = condProp != null && condProp.boolValue;
        if (drawIf.inverse) enabled = !enabled;

        EditorGUI.BeginDisabledGroup(!enabled);
        EditorGUI.PropertyField(position, property, label);
        EditorGUI.EndDisabledGroup();
    }
}
#endif