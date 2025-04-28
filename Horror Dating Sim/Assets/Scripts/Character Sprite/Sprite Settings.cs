using UnityEngine;

/// <summary>
/// Settings on the corresponding character sprite per line.
/// 
/// Author: William Min
/// </summary>
[System.Serializable]
public struct SpriteSettings
{
    #region Public Fields

    /// <summary>
    /// True if the character sprite will be a subject.
    /// </summary>
    public ToggledField<bool> IsSubject;

    /// <summary>
    /// True if the character sprite will be revealed.
    /// </summary>
    public ToggledField<bool> IsRevealed;

    /// <summary>
    /// True if the character sprite will arrive onto the scene.
    /// </summary>
    public ToggledField<bool> HasArrived;

    /// <summary>
    /// Starting acnhor position of the image object relative to the character sprite.
    /// </summary>
    public ToggledField<Vector2> StartingAnchorPosition;

    /// <summary>
    /// Starting dimensions of the image object relative to the character sprite.
    /// </summary>
    public ToggledField<Vector2> StartingDimensions;

    /// <summary>
    /// The sprite that will be displayed on the character sprite based on sprite type and version number.
    /// </summary>
    public ToggledField<SpriteSearch> ImageSearchParameters;

    #endregion

    #region Public Methods

    /// <summary>
    /// Updates the given sprite with its parameters.
    /// </summary>
    /// <param name="sprite">The character sprite to be updated</param>
    public void UpdateCharacterSprite(CharacterSprite sprite)
    {
        if (HasArrived.IsEnabled) sprite.HasArrived = HasArrived.Value;
        if (IsRevealed.IsEnabled) sprite.IsRevealed = IsRevealed.Value;
        if (IsSubject.IsEnabled) sprite.IsSubject = IsSubject.Value;
        if (StartingAnchorPosition.IsEnabled) sprite.SpritePosition = StartingAnchorPosition.Value;
        if (StartingDimensions.IsEnabled) sprite.SpriteScale = StartingDimensions.Value;
        if (ImageSearchParameters.IsEnabled) sprite.ChangeImage(ImageSearchParameters.Value);
    }

    #endregion
}