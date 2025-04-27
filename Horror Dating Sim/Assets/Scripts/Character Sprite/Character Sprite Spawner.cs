using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// OpjectPool Spawner of character sprites
/// 
/// Author: William Min
/// </summary>
public class CharacterSpriteSpawner : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private CharacterSprite _spritePrefab; // Reference to character sprite prefab

    #endregion

    #region Private Fields

    private List<CharacterSprite> _activeSprites; // The list of active sprites spawned
    private ObjectPool<CharacterSprite> _pool; // Object pool that handles spawning of character sprites

    #endregion

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public static List<CharacterSprite> ActiveSprites { get => Instance._activeSprites; }

    #endregion

    #region Singleton Structure

    /// <summary>
    /// Reference to single instance of Character Sprite Spawner singleton.
    /// </summary>
    public static CharacterSpriteSpawner Instance { get; private set; }

    // Initializes singleton and sets up spawner variables
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Sets up object
            _activeSprites = new List<CharacterSprite>();
            _pool = new ObjectPool<CharacterSprite>(_createSprite, _onTakeSpriteFromPool, _onReturnSpriteFromPool, _onDestroySprite, true, 4, 8);
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Creates a sprite from the object pool, assigns its character profile, and sets its local position and rotation to default
    /// </summary>
    /// <param name="profile">Character profile for the sprite to reference</param>
    /// <param name="parent">Parent transform for the pulled sprite</param>
    /// <returns>Newly created character sprite from the object pool</returns>
    public static CharacterSprite CreateSprite(CharacterProfile profile, Transform parent = null)
    {
        CharacterSprite sprite = Instance._pool.Get();

        Transform spriteTransform = sprite.transform;
        if (parent != null) spriteTransform.SetParent(parent);
        spriteTransform.localPosition = Vector3.zero;
        spriteTransform.localRotation = Quaternion.identity;

        sprite.Profile = profile;

        return sprite;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sprite">Character sprite to be released</param>
    public static void ReleaseSprite(CharacterSprite sprite)
    {
        Instance._pool.Release(sprite);
    }

    #endregion

    #region Private Methods

    // Creates a new instance of a character sprite
    private CharacterSprite _createSprite()
    {
        CharacterSprite sprite = Instantiate(_spritePrefab, Vector3.zero, Quaternion.identity);
        return sprite;
    }

    // 
    private void _onTakeSpriteFromPool(CharacterSprite sprite)
    {
        _activeSprites.Add(sprite);
        sprite.gameObject.SetActive(true);
    }

    // 
    private void _onReturnSpriteFromPool(CharacterSprite sprite)
    {
        _activeSprites.Remove(sprite);
        sprite.gameObject.SetActive(false);
    }

    // 
    private void _onDestroySprite(CharacterSprite sprite)
    {
        _activeSprites.Remove(sprite);
        Destroy(sprite.gameObject);
    }

    #endregion
}
