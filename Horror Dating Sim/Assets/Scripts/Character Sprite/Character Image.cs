using UnityEngine;

/// <summary>
/// 
/// 
/// Author: William Min
/// </summary>
[System.Serializable]
public struct CharacterImage
{
    #region Serialized Fields

    [SerializeField] private ImageType _spriteType; // 
    [SerializeField] private Sprite _imageSprite; // 

    #endregion

    #region Enums

    /// <summary>
    /// 
    /// </summary>
    public enum ImageType
    {
        IDLE,
        HAPPY,
        SAD,
        ANGRY,
        SURPRISED
    }

    #endregion

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public ImageType SpriteType { get => _spriteType; }

    /// <summary>
    /// 
    /// </summary>
    public Sprite ImageSprite { get => _imageSprite; }

    #endregion
}
