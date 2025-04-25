using UnityEngine;
using UnityEngine.Pool;

public class CharacterSpriteSpawner : MonoBehaviour
{
    [SerializeField] private Transform _spriteParent;
    [SerializeField] private CharacterSprite _spritePrefab;
    [SerializeField] private ObjectPool<CharacterSprite> _pool;

    private CharacterSprite CreateSprite()
    {
        CharacterSprite sprite = Instantiate(_spritePrefab);
        
        return sprite;
    }
}
