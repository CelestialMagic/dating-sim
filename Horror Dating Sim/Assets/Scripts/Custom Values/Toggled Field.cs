using UnityEngine;

/// <summary>
/// Stores and shows a value of a certain type if it is enabled to show it.
/// 
/// Author: William Min
/// </summary>
/// <typeparam name="T">The type of value this field will store</typeparam>
[System.Serializable]
public struct ToggledField<T>
{
    #region Serialized Fields

    [SerializeField] private bool _isEnabled; // True if the field is enabled to hold and return the value
    [SerializeField] private T _value; // Value with a certain type

    #endregion

    #region Properties

    /// <summary>
    /// Returns true if the field will store and return the value.
    /// </summary>
    public bool IsEnabled { get => _isEnabled; }

    /// <summary>
    /// Returns the value if it is enabled.
    /// </summary>
    public T Value { get => _isEnabled ? _value : default(T); }

    #endregion
}
