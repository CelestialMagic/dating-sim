using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// 
/// 
/// Author: William Min
/// </summary>
public class CharacterSpriteSpawner : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private CharacterSprite _spritePrefab; // 
    [SerializeField] private ObjectPool<CharacterSprite> _pool; // 

    #endregion

    #region Private Fields

    private List<CharacterSprite> _activeSprites; // 

    #endregion

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public List<CharacterSprite> ActiveSprites { get => _activeSprites; }

    #endregion

    #region Monobehavior Callbacks

    // Sets up spawner
    private void Awake()
    {
        _activeSprites = new List<CharacterSprite>();
        _pool = new ObjectPool<CharacterSprite>(_createSprite, _onTakeSpriteFromPool, _onReturnSpriteFromPool, _onDestroySprite, true, 4, 8);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parent"></param>
    public void CreateSprite(Transform parent)
    {
        Transform spriteTransform = _pool.Get().transform;
        spriteTransform.SetParent(parent);
        spriteTransform.localPosition = Vector3.zero;
        spriteTransform.localRotation = Quaternion.identity;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sprite"></param>
    public void ReleaseSprite(CharacterSprite sprite)
    {
        _pool.Release(sprite);
    }

    #endregion

    #region Private Methods

    // Creates a new instance of a character sprite
    private CharacterSprite _createSprite()
    {
        CharacterSprite sprite = Instantiate(_spritePrefab, Vector3.zero, Quaternion.identity);
        sprite.SetPool(_pool);
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
