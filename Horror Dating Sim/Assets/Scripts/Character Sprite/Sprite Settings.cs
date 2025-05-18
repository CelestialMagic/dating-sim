using UnityEngine;

/// <summary>
/// Settings on the corresponding character sprite per line.
/// 
/// Author: William Min
/// </summary>
[System.Serializable]
public struct SpriteSettings
{
    #region Serialized Fields

    [SerializeField] private int _spriteIndex;                                  // Index of sprite to modify
    [Space]
    [SerializeField] private ToggledField<bool> IsSubject;                      // True if the character sprite will be a subject.
    [SerializeField] private ToggledField<bool> IsRevealed;                     // True if the character sprite will be revealed.
    [SerializeField] private ToggledField<bool> HasArrived;                     // True if the character sprite will arrive onto the scene.
    [SerializeField] private ToggledField<Vector2> StartingAnchorPosition;      // Starting acnhor position of the image object relative to the character sprite.
    [SerializeField] private ToggledField<Vector2> StartingDimensions;          // Starting dimensions of the image object relative to the character sprite.
    [SerializeField] private ToggledField<SpriteSearch> ImageSearchParameters;  // The sprite that will be displayed on the character sprite based on sprite type and version number.

    #endregion

    #region Public Methods

    /// <summary>
    /// Updates the given sprite with its parameters.
    /// </summary>
    /// <param name="sprites">character sprite list to modify</param>
    public void UpdateCharacterSprites(CharacterSprite[] sprites)
    {
        if (_spriteIndex < 0 || _spriteIndex >= sprites.Length)
            return;

        CharacterSprite sprite = sprites[_spriteIndex];

        if (HasArrived.IsEnabled) sprite.HasArrived = HasArrived.Value;
        if (IsRevealed.IsEnabled) sprite.IsRevealed = IsRevealed.Value;
        if (IsSubject.IsEnabled) sprite.IsSubject = IsSubject.Value;
        if (StartingAnchorPosition.IsEnabled) sprite.SpritePosition = StartingAnchorPosition.Value;
        if (StartingDimensions.IsEnabled) sprite.SpriteDimensions = StartingDimensions.Value;
        if (ImageSearchParameters.IsEnabled) sprite.ChangeImage(ImageSearchParameters.Value);
    }

    #endregion
}