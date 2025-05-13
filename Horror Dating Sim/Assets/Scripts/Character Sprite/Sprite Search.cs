using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Parameters for searching and returning a sprite.
/// 
/// Author: William Min
/// </summary>
[System.Serializable]
public struct SpriteSearch
{
    #region Serialized Fields

    [SerializeField] private CharacterProfile _profile;             // Character profile to get images from
    [SerializeField] private CharacterImage.ImageType _imageType;   // Type of sprite to search for
    [SerializeField] private int _version;                          // Version number of the type of sprite to searh for

    #endregion

    #region Properties

    #endregion

    /// <summary>
    /// Changes the image's sprite according to the sprite search.
    /// </summary>
    /// <param name="spriteImage">Image reference to change sprite</param>
    public void ChangeImage(Image spriteImage)
    {
        spriteImage.sprite = _profile.GetSprite(_imageType, _version);
    }
}
