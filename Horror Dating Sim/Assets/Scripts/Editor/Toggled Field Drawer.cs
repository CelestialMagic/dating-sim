using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
/// <summary>
/// Property drawer of a ToggledField<T> instance.
/// 
/// Author: William Min
/// </summary>
[CustomPropertyDrawer(typeof(ToggledField<>))]
public class ToggledFieldDrawer : PropertyDrawer
{
    /// <summary>
    /// Draws the ToggledField instance.
    /// </summary>
    /// <param name="position">Current position and dimensions of drawing the instance</param>
    /// <param name="property">Reference to property to be drawn</param>
    /// <param name="label">The display mode of drawing the instance</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        int indentDelta = 4;
        EditorGUI.indentLevel += indentDelta;

        float variableWidth = position.width * 3 / 8;
        float enabledPrefixWidth = variableWidth * 3 / 4;
        float enabledFieldWidth = variableWidth / 8;
        float offsetSize = 10;

        SerializedProperty isEnabled = property.FindPropertyRelative("_isEnabled");

        EditorGUI.PrefixLabel(new Rect(position.x, position.y, enabledPrefixWidth - offsetSize, position.height), new GUIContent(property.displayName));
        EditorGUI.PropertyField(new Rect(position.x + enabledPrefixWidth, position.y, enabledFieldWidth - offsetSize, position.height), isEnabled, GUIContent.none);

        if (isEnabled.boolValue)
        {
            float valueWidth = position.width * 5 / 8;
            float valuePrefixWidth = valueWidth / 4;
            float valueFieldWidth = valueWidth * 3 / 4;

            SerializedProperty value = property.FindPropertyRelative("_value");
            EditorGUI.PrefixLabel(new Rect(position.x + variableWidth, position.y, valuePrefixWidth - offsetSize, position.height), new GUIContent(value.displayName));
            EditorGUI.PropertyField(new Rect(position.x + variableWidth + valuePrefixWidth, position.y, valueFieldWidth - offsetSize, position.height), value, GUIContent.none);
        }

        EditorGUI.indentLevel -= indentDelta;

        EditorGUI.EndProperty();
    }
}
#endif