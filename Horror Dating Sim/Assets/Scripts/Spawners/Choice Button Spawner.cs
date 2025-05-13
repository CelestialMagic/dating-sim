using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// OpjectPool Spawner of choice buttons.
/// 
/// Author: William Min
/// </summary>
public class ChoiceButtonSpawner : Spawner<Button>
{
    #region Singleton Structure

    /// <summary>
    /// Reference to single instance of Choice Button Spawner singleton.
    /// </summary>
    public static ChoiceButtonSpawner Instance { get; private set; }

    // Initializes singleton and sets up variables
    protected override void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            base.Awake();
        }
    }

    #endregion

    #region Spawner Callbacks

    // 
    protected override void _setupSpawned(GameObject spawnedObject, Button button)
    {
        
    }

    #endregion
}
