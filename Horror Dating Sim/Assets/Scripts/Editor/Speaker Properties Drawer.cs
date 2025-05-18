using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
/// <summary>
/// Property drawer of a SpeakerProperties instance.
/// 
/// Author: William Min
/// </summary>
[CustomPropertyDrawer(typeof(SpeakerProperties))]
public class SpeakerPropertiesDrawer : PropertyDrawer
{
    private const int STRING_NAME_ENUM_INDEX = (int)SpeakerProperties.SpeakerType.STRING_NAME;
    private const int CHARACTER_NAME_ENUM_INDEX = (int)SpeakerProperties.SpeakerType.CHARACTER_NAME;

    /// <summary>
    /// Draws the SpeakerProperties instance.
    /// </summary>
    /// <param name="position">Current position and dimensions of drawing the instance</param>
    /// <param name="property">Reference to property to be drawn</param>
    /// <param name="label">The display mode of drawing the instance</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        float variableWidth = position.width / 2;
        float typePrefixWidth = variableWidth * 5 / 16;
        float typeFieldWidth = variableWidth / 2;
        float offsetSize = 10;

        SerializedProperty speakerType = property.FindPropertyRelative("_speakerType");
        EditorGUI.PrefixLabel(new Rect(position.x, position.y, typePrefixWidth - offsetSize, position.height), new GUIContent(speakerType.displayName));
        EditorGUI.PropertyField(new Rect(position.x + typePrefixWidth, position.y, typeFieldWidth - offsetSize, position.height), speakerType, GUIContent.none);

        SerializedProperty revealedProperty = null;

        switch (speakerType.enumValueIndex)
        {
            case STRING_NAME_ENUM_INDEX:
                revealedProperty = property.FindPropertyRelative("_name");
                break;

            case CHARACTER_NAME_ENUM_INDEX:
                revealedProperty = property.FindPropertyRelative("_profile");
                break;
        }

        float valuePrefixWidth = variableWidth * 3 / 16;
        float valueFieldWidth = variableWidth * 13 / 16;

        if (revealedProperty != null)
        {
            EditorGUI.PrefixLabel(new Rect(position.x + variableWidth, position.y, valuePrefixWidth - offsetSize, position.height), new GUIContent(revealedProperty.displayName));
            EditorGUI.PropertyField(new Rect(position.x + variableWidth + valuePrefixWidth, position.y, valueFieldWidth - offsetSize, position.height), revealedProperty, GUIContent.none);
        }

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }

}
#endif