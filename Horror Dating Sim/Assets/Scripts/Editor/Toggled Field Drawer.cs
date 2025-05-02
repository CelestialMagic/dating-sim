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

        EditorGUI.indentLevel++;

        float fieldWidth = position.width / 2;
        float bufferWidth = position.width / 16;
        float toggleWidth = fieldWidth / 2;

        float offsetSize = 10;

        SerializedProperty isEnabled = property.FindPropertyRelative("_isEnabled");

        EditorGUI.PropertyField(new Rect(position.x + bufferWidth, position.y, toggleWidth - offsetSize, position.height), isEnabled, new GUIContent(property.displayName));

        if (isEnabled.boolValue)
        {
            SerializedProperty value = property.FindPropertyRelative("_value");
            EditorGUI.PropertyField(new Rect(position.x + fieldWidth, position.y, fieldWidth - offsetSize, position.height), value, GUIContent.none);
        }

        EditorGUI.indentLevel--;

        EditorGUI.EndProperty();
    }
}
#endif