using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Spawner<T> : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private GameObject _spawnedPrefab; // Reference to character sprite prefab
    [SerializeField] private int _defaultCount = 5; //
    [SerializeField] private int _maxCount = 10; //

    #endregion

    #region Private Fields

    private List<T> _spawnedObjects;        // The list of active sprites spawned
    private ObjectPool<GameObject> _pool;   // Object pool that handles spawning of character sprites
    private Action<T> _defaultSpawnSetUp;   // 

    #endregion

    #region Properties

    /// <summary>
    /// Returns a list of currently active sprites.
    /// </summary>
    public List<T> SpawnedObjects { get => _spawnedObjects; }

    #endregion

    // Initializes spawner and sets up variables
    protected virtual void Awake()
    {
        _spawnedObjects = new List<T>();
        _pool = new ObjectPool<GameObject>(_create, _onTakeFromPool, _onReturnFromPool, _onDestroy, true, _defaultCount, _maxCount);
    }

    #region Public Methods

    /// <summary>
    /// Sets up the spawner.
    /// </summary>
    public void SetUpSpawner()
    {
        if (_spawnedPrefab.TryGetComponent<T>(out T component))
        {
            _spawnedObjects = new List<T>();
            _pool = new ObjectPool<GameObject>(_create, _onTakeFromPool, _onReturnFromPool, _onDestroy, true, _defaultCount, _maxCount);
        }
        else
        {
            Debug.LogError($"The spawn prefab is not type {typeof(T)}.");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="localPosition"></param>
    /// <param name="localRotation"></param>
    /// <param name="localScale"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public T Spawn(Vector3 localPosition, Quaternion localRotation, Vector3 localScale, Transform parent = null)
    {
        GameObject newObject = _pool.Get();

        if (newObject.TryGetComponent<T>(out T component))
        {
            _spawnedObjects.Add(component);

            _setupSpawned(newObject, component);

            //if (_defaultSpawnSetUp != null) _defaultSpawnSetUp.Invoke(component);

            Transform objTransform = newObject.transform;
            if (parent != null) objTransform.SetParent(parent);
            objTransform.localPosition = localPosition;
            objTransform.localRotation = localRotation;
            objTransform.localScale = localScale;

            return component;
        }
        else
        {
            Debug.LogError($"The spawn prefab is not type {typeof(T)}.");
            return default(T);
        }
    }

    /// <summary>
    /// Despawns a sprite and returns it to the pool.
    /// </summary>
    /// <param name="poolObject">Pooled object to be released</param>
    public bool Despawn(GameObject poolObject)
    {
        if (poolObject.TryGetComponent<T>(out T component))
        {
            _spawnedObjects.Remove(component);
            _pool.Release(poolObject);
            return true;
        }
        else
            return false;
    }

    #endregion

    #region Protected Methods

    // Sets up the traits of a newly spawned object
    protected abstract void _setupSpawned(GameObject spawnedObject, T component);

    #endregion

    #region Private Methods

    // Creates a new instance of a character sprite
    private GameObject _create()
    {
        return Instantiate(_spawnedPrefab, Vector3.zero, Quaternion.identity);
    }

    // Takes a given inactive sprite from the pool and sets it up for use
    private void _onTakeFromPool(GameObject poolObject)
    {
        poolObject.SetActive(true);
    }

    // Returns a given active sprite to the pool and deactivates it
    private void _onReturnFromPool(GameObject poolObject)
    {
        poolObject.SetActive(false);
    }

    // Destroys the given sprite
    private void _onDestroy(GameObject poolObject)
    {
        Destroy(poolObject);
    }

    #endregion
}
