using UnityEngine;

/// <summary>
/// Character image containing the sprite type and a corresponding sprite.
/// 
/// Author: William Min
/// </summary>
[System.Serializable]
public struct CharacterImage
{
    #region Serialized Fields

    [SerializeField] private ImageType _spriteType; // Sprite type
    [SerializeField] private Sprite _imageSprite; // Reference to sprite

    #endregion

    #region Enums

    /// <summary>
    /// Avaliable image types for sprites.
    /// </summary>
    public enum ImageType
    {
        IDLE,
        HAPPY,
        SAD,
        ANGRY,
        SURPRISED,
        DISTORTED,
        DISGUSTED,
        CRYING,

    }

    #endregion

    #region Properties

    /// <summary>
    /// Returns the sprite type of the stored sprite.
    /// </summary>
    public ImageType SpriteType { get => _spriteType; }

    /// <summary>
    /// Returns the sprite.
    /// </summary>
    public Sprite ImageSprite { get => _imageSprite; }

    #endregion
}
