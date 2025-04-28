using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A line of dialogue with data on the line, the featured speakers, and the sprite settings for those featured in the script.
/// 
/// Author: William Min
/// </summary>
[Serializable]
public class DialogueLine
{
    #region Serialized Fields

    [SerializeField] private SpeakerProperties[] _speakerProperties; // 
    [SerializeField] private SpriteSettings[] _characterSpriteSettings; // 
    [TextArea(3, 20)] [SerializeField] private string _line; // 

    #endregion

    #region Private Fields

    private string[] _speakerNames;
    private string _speakerNamesDisplay;

    #endregion

    #region Properties

    /// <summary>
    /// Returns the line of dialogue as a string.
    /// </summary>
    public string Line { get => _line; }

    /// <summary>
    /// Returns the list of speaker names featured in the line.
    /// </summary>
    public string[] SpeakerNames { get => _speakerNames; }

    /// <summary>
    /// Returns the whole string displaying all the speaker names in the line.
    /// </summary>
    public string SpeakerNamesDisplay { get => _speakerNamesDisplay; }

    #endregion

    #region Public Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="featuredCharacters"></param>
    /// <param name="narratorName"></param>
    /// <param name="playerName"></param>
    /// <param name="hiddenName"></param>
    public void ProcessSpeakerNames(CharacterProfile[] featuredCharacters, string narratorName, string playerName, string hiddenName)
    {
        int indexLength = _speakerProperties.Length;
        _speakerNames = new string[indexLength];

        for (int i = 0; i < indexLength; i++)
        {
            int speakerIndex = _speakerProperties[i].SpeakerIndex;
            string name = null;

            if (_speakerProperties[i].IsHidden)
                name = hiddenName;
            else
            {
                switch (speakerIndex)
                {
                    case -2:
                        name = narratorName;
                        break;

                    case -1:
                        name = playerName;
                        break;

                    default:
                        name = featuredCharacters[speakerIndex].CharacterName;
                        break;
                }
            }

            if (name != null) _speakerNames[i] = name;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void ProcessSpeakerNamesDisplay()
    {
        if (_speakerNames == null)
        {
            Debug.LogError($"The list of speaker names hasn't been processed for this line.");
            return;
        }

        _speakerNamesDisplay = _replaceLastOccurrence(String.Join(", ", _speakerNames), ", ", _speakerNames.Length > 2 ? ", and " : " and ");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="characterSprites"></param>
    public void ToggleCharacterSprites(List<CharacterSprite> characterSprites)
    {
        int i = 0;

        while (i < _characterSpriteSettings.Length && i < characterSprites.Count)
        {
            _characterSpriteSettings[i].UpdateCharacterSprite(characterSprites[i]);
            i++;
        }
    }

    #endregion

    #region Private Methods

    // Returns a string that has the last occurrence of a substring in the given source string replaced by the new substring.
    private static string _replaceLastOccurrence(string source, string find, string replace)
    {
        int place = source.LastIndexOf(find);

        if (place == -1)
            return source;

        return source.Remove(place, find.Length).Insert(place, replace);
    }

    #endregion
}
