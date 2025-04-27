using UnityEngine;

[System.Serializable]
public struct ToggledField<T>
{
    [SerializeField] private bool _isEnabled;
    [SerializeField] private T _value;

    public bool IsEnabled { get => _isEnabled; }
    public T Value { get => _isEnabled ? _value : default(T); }
}
