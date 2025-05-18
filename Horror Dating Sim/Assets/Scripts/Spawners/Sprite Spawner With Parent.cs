public class SpriteSpawnerWithParent : SpawnInParent<CharacterSprite>
{
    protected override void Awake()
    {
        base.Awake();
        _spawner = FindObjectOfType<Spawner<CharacterSprite>>();
    }
}
