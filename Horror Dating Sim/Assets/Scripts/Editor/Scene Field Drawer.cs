using UnityEngine;
using UnityEditor;

/// <summary>
/// Property drawer of a SceneField field.
/// 
/// Author: Sasquatch Studios
/// </summary>
[CustomPropertyDrawer(typeof(SceneField))]
public class SceneFieldDrawer : PropertyDrawer
{
    /// <summary>
    /// Draws the SceneField instance.
    /// </summary>
    /// <param name="position">Current position and dimensions of drawing the instance</param>
    /// <param name="property">Reference to property to be drawn</param>
    /// <param name="label">The display mode of drawing the instance</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, GUIContent.none, property);

        SerializedProperty sceneAsset = property.FindPropertyRelative("_sceneAsset");
        SerializedProperty sceneName = property.FindPropertyRelative("_sceneName");

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        if (sceneAsset != null)
        {
            Object objValue = EditorGUI.ObjectField(position, sceneAsset.objectReferenceValue, typeof(SceneAsset), false);
            sceneAsset.objectReferenceValue = objValue;
            if (objValue != null) sceneName.stringValue = (objValue as SceneAsset).name;
        }

        EditorGUI.EndProperty();
    }
}
