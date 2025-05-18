using UnityEngine;

/// <summary>
/// 
/// 
/// Author: William Min
/// </summary>
[System.Serializable]
public abstract class Skit : ScriptableObject
{
    #region Serialized Field

    [Header("Skit Properties")]
    [SerializeField] protected int _spriteCount; //

    #endregion

    #region Static Fields

    /// <summary>
    /// 
    /// </summary>
    public static SpawnInParent<CharacterSprite> SPRITE_SPAWNER;

    #endregion

    #region Public Methods

    /// <summary>
    /// Returns a dialogue line based on index.
    /// </summary>
    /// <param name="index">Index of the dialogue line in skit.</param>
    public abstract DialogueLine GetLine(int index);

    /// <summary>
    /// Returns the number of lines in the skit.
    /// </summary>
    /// <returns>Number of lines in skit</returns>
    public abstract int GetLineCount();

    /// <summary>
    /// Sets up the skit before playing the game.
    /// Call this first during game.
    /// </summary>
    public virtual void SetupSkit()
    {
        if (SPRITE_SPAWNER == null)
            SPRITE_SPAWNER = FindObjectOfType<SpawnInParent<CharacterSprite>>();
    }

    /// <summary>
    /// Processes the skit before played in dialogue handler.
    /// </summary>
    public virtual void Process()
    {
        SPRITE_SPAWNER.SetSpawnedCount(_spriteCount);
    }

    #endregion
}
