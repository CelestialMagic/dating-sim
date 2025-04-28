using System;
using UnityEngine;

/// <summary>
/// Properties of a given speaker in a line of dialogue.
/// 
/// Author: William Min
/// </summary>
[Serializable]
public struct SpeakerProperties : IComparable
{
    #region Serialized Fields

    [SerializeField] private int _speakerIndex; // Index of the speaker in relation to the list of character profiles
    [SerializeField] private bool _isHidden; // True if the name will be displayed as hidden

    #endregion

    #region Properties

    /// <summary>
    /// Returns the index of the speaker in relation to the list of character profiles.
    /// </summary>
    public int SpeakerIndex { get => _speakerIndex; }

    /// <summary>
    /// Returns true if the name will be displayed as a hidden name.
    /// </summary>
    public bool IsHidden { get => _isHidden; }

    #endregion

    #region IComparable Callbacks

    /// <summary>
    /// Compares the object to another object during a comparison operation.
    /// </summary>
    /// <param name="obj">Object to compare to</param>
    /// <returns>
    /// If the returned value is less than 0, then it is lower than the compared object.
    /// If the returned value is equal to 0, then it is equal in order to the compared object.
    /// If the returned value is greater than 0, then it is higher than the compared object.
    /// </returns>
    public int CompareTo(object obj)
    {
        return obj == null || obj.GetType() != typeof(SpeakerProperties) ? 0 : _speakerIndex.CompareTo(((SpeakerProperties)obj)._speakerIndex);
    }

    #endregion
}
