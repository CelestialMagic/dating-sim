using UnityEngine;

/// <summary>
/// 
/// 
/// Author: William Min
/// </summary>
/// <typeparam name="T"></typeparam>
[System.Serializable]
public class SpawnerWithParent<T>
{
    #region Serialized Fields

    [SerializeField] private Transform _spawnerParent;  // 
    [SerializeField] private Spawner<T> _spawner;       //  

    #endregion

    #region Private Fields

    private T[] _spawnedInParent;           //
    private GameObject[] _objectsInParent;  //
    //
    private int _spawnedCount { get => _spawnedInParent == null ? 0 : _spawnedInParent.Length; }

    #endregion

    #region Public Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public T GetSpawned(int index)
    {
        if (index >= 0 && index < _spawnedCount)
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
    /// <param name="count"></param>
    public void SetSpawnedCount(int count)
    {
        if (count != _spawnedCount)
        {
            if (count > _spawnedCount)
                for (int i = _spawnedCount; i < count; i++)
                    SpawnInstance(Vector3.zero, Quaternion.identity, Vector3.one);
            else
                for (int i = _spawnedCount - 1; i >= count; i--)
                    DespawnInstance(i);

            _updateSpawnCollection();
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
        _updateSpawnCollection();
        return component;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="spawnedInstance"></param>
    public void DespawnInstance(GameObject spawnedInstance)
    {
        _despawnInstance(spawnedInstance);
        _updateSpawnCollection();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    public void DespawnInstance(int index)
    {
        if (index >= 0 && index < _spawnedInParent.Length)
            DespawnInstance(_objectsInParent[index]);
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

    // 
    private void _updateSpawnCollection()
    {
        int childCount = _spawnerParent.childCount;
        _spawnedInParent = new T[childCount];
        _objectsInParent = new GameObject[childCount];

        for (int i = 0; i < childCount; i++)
        {
            _objectsInParent[i] = _spawnerParent.GetChild(i).gameObject;
            _spawnedInParent[i] = _objectsInParent[i].GetComponent<T>();
        }
    }

    #endregion
}