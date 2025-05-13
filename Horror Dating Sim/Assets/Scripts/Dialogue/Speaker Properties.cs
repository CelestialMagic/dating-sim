using System;
using UnityEngine;

/// <summary>
/// Properties of a given speaker in a line of dialogue.
/// 
/// Author: William Min
/// </summary>
[Serializable]
public struct SpeakerProperties
{
    #region Serialized Fields

    [SerializeField] private SpeakerType _speakerType;  // Type of speaker
    [SerializeField] private string _name;              // Name of the speaker if allowed as a string itself
    [SerializeField] private CharacterProfile _profile; // Character profile containing a character name

    #endregion

    #region Enums

    /// <summary>
    /// Represents the type of speaker
    /// </summary>
    public enum SpeakerType
    {
        STRING_NAME = 0,
        PLAYER = 1,
        CHARACTER_NAME = 2
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Returns a string representing the speaker's name.
    /// </summary>
    /// <returns>The name of the speaker as a string</returns>
    public string GetName()
    {
        switch (_speakerType)
        {
            case SpeakerType.STRING_NAME:
                return _name;

            case SpeakerType.PLAYER:
                return PlayerData.Instance == null ? "Player" : PlayerData.Instance.PlayerName;

            default:
                return _profile.CharacterName;
        }
    }

    #endregion
}
