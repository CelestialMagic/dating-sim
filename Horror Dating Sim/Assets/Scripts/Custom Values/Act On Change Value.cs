using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Decorator of a value type that calls events if the value changes.
/// 
/// Author: William Min
/// </summary>
/// <typeparam name="T">Type of the value</typeparam>
[Serializable]
public struct ActOnChangeValue<T>
{
    #region Serialized Fields

    [SerializeField] private T _value; // 

    #endregion

    #region Private Fields

    private T _originalValue; // 
    private UnityEvent _unityEventsOnChangedValue; // 

    #endregion

    #region Public Fields

    /// <summary>
    /// 
    /// </summary>
    public Action<T> ActionsOnChangedValue;

    #endregion

    #region Propertiees

    /// <summary>
    /// 
    /// </summary>
    public UnityEvent UnityEventsOnChangedValue { get { if (_unityEventsOnChangedValue == null) _unityEventsOnChangedValue = new UnityEvent(); return _unityEventsOnChangedValue; } set => _unityEventsOnChangedValue = value; }

    /// <summary>
    /// 
    /// </summary>
    public T Value
    {
        get => _value;
        set
        {
            _originalValue = _value;
            _value = value;
            ActOnValue();
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Invokes actions and events on the value
    /// </summary>
    public void ActOnValue()
    {
        if (_originalValue != null && _originalValue.Equals(_value)) return;

        if (ActionsOnChangedValue != null) ActionsOnChangedValue(_value);
        _unityEventsOnChangedValue?.Invoke();
        _originalValue = _value;
    }

    #endregion
}
