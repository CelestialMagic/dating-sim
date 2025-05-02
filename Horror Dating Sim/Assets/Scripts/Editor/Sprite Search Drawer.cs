using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
/// <summary>
/// Property drawer of a SpriteSearch instance.
/// 
/// Author: William Min
/// </summary>
[CustomPropertyDrawer(typeof(SpriteSearch))]
public class SpriteSearchDrawer : PropertyDrawer
{
    /// <summary>
    /// Draws the SpriteSearch instance.
    /// </summary>
    /// <param name="position">Current position and dimensions of drawing the instance</param>
    /// <param name="property">Reference to property to be drawn</param>
    /// <param name="label">The display mode of drawing the instance</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty imageType = property.FindPropertyRelative("_imageType");
        SerializedProperty version = property.FindPropertyRelative("_version");

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        float variableWidth = position.width / 2;
        float prefixWidth = variableWidth * 5 / 8;
        float fieldWidth = variableWidth * 3 / 8;
        float offsetSize = 10;

        EditorGUI.PropertyField(new Rect(position.x, position.y, variableWidth - offsetSize, position.height), imageType, GUIContent.none);
        EditorGUI.PrefixLabel(new Rect(position.x + variableWidth, position.y, prefixWidth - offsetSize, position.height), new GUIContent(version.displayName));
        EditorGUI.PropertyField(new Rect(position.x + variableWidth + prefixWidth, position.y, fieldWidth - offsetSize, position.height), version, GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
#endif