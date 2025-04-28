using UnityEngine;
using UnityEditor;

/// <summary>
/// Property drawer of a CharacterImage instance.
/// 
/// Author: William Min
/// </summary>
[CustomPropertyDrawer(typeof(CharacterImage))]
public class CharacterImageDrawer : PropertyDrawer
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

        SerializedProperty spriteType = property.FindPropertyRelative("_spriteType");
        SerializedProperty spriteImage = property.FindPropertyRelative("_imageSprite");

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        float fieldWidth = position.width / 2;
        float offsetSize = 2;

        EditorGUI.PropertyField(new Rect(position.x, position.y, fieldWidth - offsetSize, position.height), spriteType, GUIContent.none);
        EditorGUI.PropertyField(new Rect(position.x + fieldWidth, position.y, fieldWidth - offsetSize, position.height), spriteImage, GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}