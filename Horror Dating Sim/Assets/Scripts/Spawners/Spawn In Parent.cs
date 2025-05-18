using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// 
/// Author: William Min
/// </summary>
/// <typeparam name="T"></typeparam>
public class SpawnInParent<T> : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private Transform _spawnerParent;

    #endregion

    #region Private Fields

    protected Spawner<T> _spawner;      // 
    private List<T> _spawnedInParent;   //

    #endregion

    #region Monobehavior Callbacks

    // Sets up spawner with parent
    protected virtual void Awake()
    {
        if (_spawnedInParent == null) _spawnerParent = transform;
        _spawnedInParent = new List<T>();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public T GetSpawned(int index)
    {
        if (index >= 0 && index < _spawnedInParent.Count)
            return _spawnedInParent[index];
        else
        {
            Debug.LogError("Index out of bounds for sprite holder.");
            return default(T);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public T[] GetAllSpawned()
    {
        return _spawnedInParent.ToArray();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="count"></param>
    public void SetSpawnedCount(int count)
    {
        int spawnedCount = _spawnedInParent.Count;

        if (count != spawnedCount)
        {
            if (count > spawnedCount)
                for (int i = spawnedCount; i < count; i++)
                    SpawnInstance(Vector3.zero, Quaternion.identity, Vector3.one);
            else
                for (int i = spawnedCount - 1; i >= count; i--)
                    DespawnInstance(i);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="localPosition"></param>
    /// <param name="localRotation"></param>
    /// <param name="localScale"></param>
    /// <returns></returns>
    public T SpawnInstance(Vector3 localPosition, Quaternion localRotation, Vector3 localScale)
    {
        T component = _spawnInstance(localPosition, localRotation, localScale);
        _spawnedInParent.Add(component);
        return component;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="spawnedInstance"></param>
    public void DespawnInstance(GameObject spawnedInstance)
    {
        if (spawnedInstance.TryGetComponent<T>(out T component) && _spawnedInParent.Contains(component))
        {
            _despawnInstance(spawnedInstance);
            _spawnedInParent.Remove(component);
        }
        else
            Debug.LogError($"{spawnedInstance} does not have a {typeof(T)} component in {name}'s transform.");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    public void DespawnInstance(int index)
    {
        if (index >= 0 && index < _spawnedInParent.Count)
            DespawnInstance(_spawnerParent.GetChild(index).gameObject);
        else
            Debug.LogError("Index out of range for collection in parent.");
    }

    #endregion

    #region Private Methods

    // 
    private T _spawnInstance(Vector3 localPosition, Quaternion localRotation, Vector3 localScale)
    {
        return _spawner.Spawn(localPosition, localRotation, localScale, _spawnerParent);
    }

    // 
    private void _despawnInstance(GameObject spawnedInstance)
    {
        _spawner.Despawn(spawnedInstance);
    }

    #endregion
}
