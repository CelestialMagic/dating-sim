using System;
using UnityEngine;

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
