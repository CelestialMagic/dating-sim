using UnityEngine;

/// <summary>
/// OpjectPool Spawner of character sprites.
/// 
/// Author: William Min
/// </summary>
public class CharacterSpriteSpawner : Spawner<CharacterSprite>
{
    #region Spawner Callbacks

    // Sets up the traits of a newly spawned object
    protected override void _setupSpawned(GameObject spawnedObject, CharacterSprite sprite)
    {
        sprite.ResetSprite();
    }

    #endregion
}
