using UnityEngine;

/// <summary>
/// Parameters for searching and returning a sprite.
/// 
/// Author: William Min
/// </summary>
[System.Serializable]
public struct SpriteSearch
{
    #region Serialized Fields

    [SerializeField] private CharacterImage.ImageType _imageType; // Type of sprite to search for
    [SerializeField] private int _version; // Version number of the type of sprite to searh for

    #endregion

    #region Properties

    /// <summary>
    /// Returns the tyep of the searched sprite.
    /// </summary>
    public CharacterImage.ImageType ImageType { get => _imageType; }

    /// <summary>
    /// Returns the version number of the type of the searched sprite.
    /// </summary>
    public int Version { get => _version; }

    #endregion
}
