using UnityEngine;

/// <summary>
/// 
/// 
/// Author: William Min
/// </summary>
[CreateAssetMenu(fileName = "New Character Profile", menuName = "Simulation Game Objects/Character Profile", order = 1)]
public class CharacterProfile : ScriptableObject
{
    #region Serialized Fields

    [SerializeField] private string _characterName; // 
    [SerializeField] private CharacterImage[] _characterImages; // 

    #endregion

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public string CharacterName { get => _characterName; }

    #endregion

    #region Public Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    public Sprite GetSprite(CharacterImage.ImageType type, int version = 1)
    {
        // Error if version is 0 or lower
        if (version <= 0)
        {
            Debug.LogWarning($"Version = {version}. Version cannot be equal to or below 0.");
            return null;
        }

        Sprite sprite = null;

        foreach (CharacterImage image in _characterImages)
        {
            if (image.SpriteType == type)
            {
                version--;
                sprite = image.ImageSprite;

                if (version <= 0)
                    return sprite;
            }
        }

        Debug.LogWarning(sprite == null ? $"Character profile {name} does not have a sprite with the type {type}." : $"Character profile {name} cannot provide version {version} of sprite type {type} as it doesn't exist.");
        return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetSpriteTypeCount(CharacterImage.ImageType type)
    {
        int count = 0;

        foreach (CharacterImage image in _characterImages)
            if (image.SpriteType == type)
                count++;

        return count;
    }

    #endregion
}
