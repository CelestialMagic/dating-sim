using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
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

    #region Properties

    /// <summary>
    /// Line of Dialogue as a string
    /// </summary>
    public string Line { get => _line; }

    #endregion

    #region Public Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="featuredCharacters"></param>
    /// <returns></returns>
    public CharacterList GetSpeakerProfiles(CharacterProfile[] featuredCharacters)
    {
        Array.Sort(_speakerProperties);
        int narratorInstances = 0;
        int playerInstances = 0;
        CharacterProfile[] profiles = null;
        int indexLength = _speakerProperties.Length;
        bool[] hiddenToggles = new bool[indexLength];
        int totalNonCharacters = 0;

        for (int i = 0; i < indexLength; i++)
        {
            int index = _speakerProperties[i].SpeakerIndex;

            switch (index)
            {
                case -2:
                    narratorInstances++;
                    break;

                case -1:
                    playerInstances++;
                    break;

                default:
                    if (profiles == null)
                    {
                        totalNonCharacters = narratorInstances + playerInstances;
                        profiles = new CharacterProfile[indexLength - totalNonCharacters];
                    }
                    profiles[i - totalNonCharacters] = featuredCharacters[index];
                    break;
            }

            hiddenToggles[i] = _speakerProperties[i].IsHidden;
        }

        return new CharacterList(profiles, playerInstances, narratorInstances, hiddenToggles);
    }

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
}
