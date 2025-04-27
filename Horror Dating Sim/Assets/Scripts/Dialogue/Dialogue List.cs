using System;

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
