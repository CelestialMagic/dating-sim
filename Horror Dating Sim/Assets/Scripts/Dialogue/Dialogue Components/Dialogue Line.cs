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

    [SerializeField] private bool hasBackground; //A bool representing whether a line has a background or not (Jessie)
    [SerializeField] private Sprite background;//A Sprite for a background change (Jessie)

    [SerializeField] private bool hasSFX; //A bool indicating if a line has a SFX (Jessie)
    [SerializeField] private AudioClip soundEffect;//An AudioClip for a SFX (Jessie)

    [TextArea] [SerializeField] private string _line; // 
    [Space]
    [SerializeField] private SpeakerProperties[] _speakerProperties; // 
    [Space]
    [SerializeField] private SpriteSettings[] _characterSpriteSettings; // 


    #endregion

    #region Private Fields

    private string[] _speakerNames; // 
    private string _speakerNamesDisplay; // 

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
    /// 

//Returns whether a line has a background or not (Jessie)
    public bool GetHasBackground()
    {
        return hasBackground;
    }

//Returns the background attached to the line (to be used by background handler) (Jessie)
    public Sprite GetBackground()
    {
        return background;
    }

    public bool GetHasSFX()
    {
        return hasSFX;
    }

    public AudioClip GetSoundEffect()
    {
        return soundEffect; 
    }



    public void ProcessSpeakerNames()
    {
        int indexLength = _speakerProperties.Length;
        _speakerNames = new string[indexLength];

        for (int i = 0; i < indexLength; i++)
            _speakerNames[i] = _speakerProperties[i].GetName();

        ProcessSpeakerNamesDisplay();
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
    public void ToggleCharacterSprites(CharacterSprite[] characterSprites)
    {
        int i = 0;

        while (i < _characterSpriteSettings.Length)
        {
            _characterSpriteSettings[i].UpdateCharacterSprites(characterSprites);
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
