using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Dialogue script filled with characters and their lines
/// 
/// Author: William Min
/// </summary>
[CreateAssetMenu(fileName = "New Dialogue Script", menuName = "Simulation Game Objects/Dialogue Script", order = 0)]
public class DialogueScript : ScriptableObject
{
    #region Serialized Fields

    [SerializeField] private CharacterProfile[] _characters; // 
    [SerializeField] private DialogueLine[] _dialogueLines; // 

    #endregion

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public CharacterProfile[] Characters { get => _characters; }

    /// <summary>
    /// 
    /// </summary>
    public DialogueLine[] DialogueLines { get => _dialogueLines; }

    #endregion

    #region Public Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public CharacterList GetSpeakersFromLineIndex(int index)
    {
        if (index < 0 || index >= _dialogueLines.Length) return null;

        return _dialogueLines[index].GetSpeakerProfiles(_characters);
    }

    #endregion
}

/// <summary>
/// 
/// 
/// Author: William Min
/// </summary>
[System.Serializable]
public class DialogueLine
{
    #region Serialized Fields

    [SerializeField] private SpeakerProperties[] _speakerProperties; // 
    [SerializeField] private CharacterSpriteSettingsSheet[] _characterSpriteSettings; // 
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
            _characterSpriteSettings[i].ToggleCharacterSprite(characterSprites[i]);
            i++;
        }
    }

    #endregion
}

/// <summary>
/// 
/// 
/// Author: William Min
/// </summary>
public class CharacterList
{
    #region Private Fields

    private int _narratorInstances; // 
    private int _playerInstances; // 
    private CharacterProfile[] _featuredCharacters; // 
    private bool[] _hiddenToggles; // 

    #endregion

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public int NarratorInstances { get => _narratorInstances; }

    /// <summary>
    /// 
    /// </summary>
    public int PlayerInstances { get => _playerInstances; }

    /// <summary>
    /// 
    /// </summary>
    public CharacterProfile[] FeaturedCharacters { get => _featuredCharacters; }

    /// <summary>
    /// 
    /// </summary>
    public int FeaturedCharacterCount { get => _featuredCharacters == null ? 0 : _featuredCharacters.Length; }

    /// <summary>
    /// 
    /// </summary>
    public bool[] HiddenToggles { get => _hiddenToggles; }

    /// <summary>
    /// 
    /// </summary>
    public int TotalCharacterCount { get => _narratorInstances + _playerInstances + FeaturedCharacterCount; }

    #endregion

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="featuredCharacters"></param>
    /// <param name="playerInstances"></param>
    /// <param name="narratorInstances"></param>
    /// <param name="hiddenToggles"></param>
    public CharacterList(CharacterProfile[] featuredCharacters, int playerInstances, int narratorInstances, bool[] hiddenToggles)
    {
        _narratorInstances = narratorInstances;
        _playerInstances = playerInstances;
        _featuredCharacters = featuredCharacters;
        _hiddenToggles = hiddenToggles;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="narratorName"></param>
    /// <param name="playerName"></param>
    /// <param name="unknown"></param>
    /// <returns></returns>
    public string GetListString(string narratorName, string playerName, string unknown)
    {
        string[] names = new string[TotalCharacterCount];
        int nameIndex = 0;

        for (int i = 0; i < _narratorInstances; i++)
            names[i + nameIndex] = _hiddenToggles[i + nameIndex] ? unknown : narratorName;

        nameIndex += _narratorInstances;

        for (int i = 0; i < _playerInstances; i++)
            names[i + nameIndex] = _hiddenToggles[i + nameIndex] ? unknown : playerName;

        nameIndex += _playerInstances;

        for (int i = 0; i < FeaturedCharacterCount; i++)
            names[i + nameIndex] = _hiddenToggles[i + nameIndex] ? unknown : _featuredCharacters[i].CharacterName;

        return ReplaceLastOccurrence(String.Join(", ", names), ", ", ", and ");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="find"></param>
    /// <param name="replace"></param>
    /// <returns></returns>
    public static string ReplaceLastOccurrence(string source, string find, string replace)
    {
        int place = source.LastIndexOf(find);

        if (place == -1)
            return source;

        return source.Remove(place, find.Length).Insert(place, replace);
    }

    #endregion
}

/// <summary>
/// 
/// 
/// Author: William Min
/// </summary>
[Serializable]
public struct SpeakerProperties : IComparable
{
    #region Serialized Fields

    [SerializeField] private int _speakerIndex; // 
    [SerializeField] private bool _isHidden; // 

    #endregion

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public int SpeakerIndex { get => _speakerIndex; }

    /// <summary>
    /// 
    /// </summary>
    public bool IsHidden { get => _isHidden; }

    #endregion

    #region IComparable Callbacks

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj">Object to compare to</param>
    /// <returns>
    /// If the returned value is les than 0, then it is lower than the compared object.
    /// If the returned value is equal to 0, then it is equal in order to the compared object.
    /// If the returned value is greater than 0, then it is higher than the compared object.
    /// </returns>
    public int CompareTo(object obj)
    {
        return obj == null || obj.GetType() != typeof(SpeakerProperties) ? 0 : _speakerIndex.CompareTo(((SpeakerProperties)obj)._speakerIndex);
    }

    #endregion
}
