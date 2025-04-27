using UnityEngine;

/// <summary>
/// 
/// 
/// Author: William Min
/// </summary>
[System.Serializable]
public struct SpriteSettings
{
    #region Public Fields

    /// <summary>
    /// 
    /// </summary>
    public ToggledField<bool> IsSubject;

    /// <summary>
    /// 
    /// </summary>
    public ToggledField<bool> IsRevealed;

    /// <summary>
    /// 
    /// </summary>
    public ToggledField<bool> HasArrived;

    /// <summary>
    /// 
    /// </summary>
    public ToggledField<Vector2> StartingAnchorPosition;

    /// <summary>
    /// 
    /// </summary>
    public ToggledField<Vector2> StartingDimensions;

    /// <summary>
    /// 
    /// </summary>
    public ToggledField<SpriteSearch> ImageSearchParameters;

    #endregion

    #region Public Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sprite"></param>
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