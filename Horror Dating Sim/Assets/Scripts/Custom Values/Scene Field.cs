using UnityEngine;

/// <summary>
/// A field for storing a scene asset.
/// 
/// Author: William Min
/// </summary>
[System.Serializable]
public class SceneField
{
    #region Serialized Fields

    [SerializeField] private Object _sceneAsset; // Reference to a scene asset
    [SerializeField] private string _sceneName = ""; // Name of the scene asset

    #endregion

    #region Properties

    /// <summary>
    /// Returns the scene name stored in the SceneField.
    /// </summary>
    public string SceneName { get => _sceneName; }

    #endregion

    #region Operators

    /// <summary>
    /// Returns a SceneField as a string through its scene name.
    /// </summary>
    /// <param name="sceneField">SceneField instance</param>
    public static implicit operator string(SceneField sceneField)
    {
        return sceneField.SceneName;
    }

    #endregion
}