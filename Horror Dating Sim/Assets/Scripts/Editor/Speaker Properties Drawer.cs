using UnityEngine;
using UnityEditor;

/// <summary>
/// 
/// 
/// Author: William Min
/// </summary>
[CustomPropertyDrawer(typeof(SpeakerProperties))]
public class SpeakerPropertiesDrawer : PropertyDrawer
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="position"></param>
    /// <param name="property"></param>
    /// <param name="label"></param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        SerializedProperty speakerIndex = property.FindPropertyRelative("_speakerIndex");
        SerializedProperty isHidden = property.FindPropertyRelative("_isHidden");

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        float variableWidth = position.width / 2;
        float prefixWidth = variableWidth * 3 / 8;
        float fieldWidth = variableWidth * 5 / 8;
        float offsetSize = 10;

        EditorGUI.PrefixLabel(new Rect(position.x, position.y, prefixWidth - offsetSize, position.height), new GUIContent(speakerIndex.displayName));
        EditorGUI.PropertyField(new Rect(position.x + prefixWidth, position.y, fieldWidth - offsetSize, position.height), speakerIndex, GUIContent.none);
        EditorGUI.PrefixLabel(new Rect(position.x + variableWidth, position.y, prefixWidth - offsetSize, position.height), new GUIContent(isHidden.displayName));
        EditorGUI.PropertyField(new Rect(position.x + variableWidth + prefixWidth, position.y, fieldWidth - offsetSize, position.height), isHidden, GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}