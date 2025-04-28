using UnityEngine;

/// <summary>
/// 
/// 
/// Author: William Min
/// </summary>
[System.Serializable]
public struct SpriteSearch
{
    #region Serialized Fields

    [SerializeField] private CharacterImage.ImageType _imageType; // 
    [SerializeField] private int _version; // 

    #endregion

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public CharacterImage.ImageType ImageType { get => _imageType; }

    /// <summary>
    /// 
    /// </summary>
    public int Version { get => _version; }

    #endregion
}
