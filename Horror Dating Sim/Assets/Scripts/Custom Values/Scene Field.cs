using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class SceneField
{
    #region Serialized Fields

    [SerializeField] private Object _sceneAsset;
    [SerializeField] private string _sceneName = "";

    #endregion

    #region Properties

    public string SceneName { get => _sceneName; }

    #endregion

    #region Operators

    public static implicit operator string(SceneField sceneField)
    {
        return sceneField.SceneName;
    }

    #endregion
}

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(SceneField))]
public class SceneFieldPropertyDrawer: PropertyDrawer
{
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

#endif