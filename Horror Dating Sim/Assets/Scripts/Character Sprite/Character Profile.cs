using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Profile that represents an npc speaker in the dialogue.
/// 
/// Author: William Min
/// </summary>
[CreateAssetMenu(fileName = "New Character Profile", menuName = "Simulation Game Objects/Character Profile", order = 1)]
public class CharacterProfile : ScriptableObject
{
    #region Serialized Fields

    [SerializeField] private string _characterName; // String name of the character
    [SerializeField] private CharacterImage[] _characterImages; // Sprites under sprite types for the character

    #endregion

    #region Private Fields

    private Dictionary<CharacterImage.ImageType, List<Sprite>> _spriteDatabase; // Database containing the sprites and their types after processing the profile

    #endregion

    #region Properties

    /// <summary>
    /// Returns the name of the character.
    /// </summary>
    public string CharacterName { get => _characterName; }

    #endregion

    #region Public Methods

    /// <summary>
    /// Processes the profile before usage.
    /// Order of sprites in each type is based on order of appearance.
    /// </summary>
    public void Process()
    {
        // Cleans up the database
        if (_spriteDatabase == null) _spriteDatabase = new Dictionary<CharacterImage.ImageType, List<Sprite>>();
        _spriteDatabase.Clear();

        // Processes all the sprites into their respective sprite types
        foreach (CharacterImage image in _characterImages)
        {
            CharacterImage.ImageType type = image.SpriteType;

            if (!_spriteDatabase.ContainsKey(type))
                _spriteDatabase[type] = new List<Sprite>();

            _spriteDatabase[type].Add(image.ImageSprite);
        }
    }

    /// <summary>
    /// Returns the sprite based on a given sprite type and version.
    /// </summary>
    /// <param name="type">The type of sprite</param>
    /// <param name="version">The version of the type of sprite</param>
    /// <returns>The sprite corresponding to the type and version given</returns>
    public Sprite GetSprite(CharacterImage.ImageType type, int version = 1)
    {
        if (!_checkDatabase())
            return null;

        // Error if version is 0 or lower
        if (version <= 0)
        {
            Debug.LogError($"Version = {version}. Version cannot be equal to or below 0.");
            return null;
        }

        int count = GetSpriteTypeCount(type);

        if (count < 0)
        {
            Debug.LogError($"Character profile {name} does not have a sprite under the type {type}.");
            return null;
        }
        else if (version > count)
        {
            Debug.LogError($"Character profile {name} only has {count} sprites under type {type}. Cannot provide version {version}.");
            return null;
        }
        else
            return _spriteDatabase[type][version - 1];
    }

    /// <summary>
    /// Returns the count of sprites for the type.
    /// Will return -1 if the type is not in the profile.
    /// </summary>
    /// <param name="type">The type of sprite to return the count of</param>
    /// <returns>Int value repreesnting the count of sprites under the given type</returns>
    public int GetSpriteTypeCount(CharacterImage.ImageType type)
    {
        if (!_checkDatabase())
            return -1;

        return _spriteDatabase.ContainsKey(type) ? _spriteDatabase[type].Count : -1;
    }

    #endregion

    #region Private Methods

    // Checks the database before using it
    private bool _checkDatabase()
    {
        if (_spriteDatabase == null)
        {
            Debug.LogError($"Character profile {name} has not processed itself yet through the ProcessProfile method.");
            return false;
        }
        else
            return true;
    }

    #endregion
}
