using UnityEngine;

/// <summary>
/// OpjectPool Spawner of character sprites.
/// 
/// Author: William Min
/// </summary>
public class CharacterSpriteSpawner : Spawner<CharacterSprite>
{
    #region Singleton Structure

    /// <summary>
    /// Reference to single instance of Character Sprite Spawner singleton.
    /// </summary>
    public static CharacterSpriteSpawner Instance { get; private set; }

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
    protected override void _setupSpawned(GameObject spawnedObject, CharacterSprite sprite)
    {
        sprite.ResetSprite();
    }

    #endregion
}
