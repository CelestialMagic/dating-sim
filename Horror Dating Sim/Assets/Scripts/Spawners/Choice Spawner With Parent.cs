using UnityEngine.UI;

/// <summary>
/// 
/// 
/// Author: William Min
/// </summary>
public class ChoiceSpawnerWithParent : SpawnInParent<Button>
{
    // Sets up spawner with parent
    protected override void Awake()
    {
        base.Awake();
        _spawner = FindObjectOfType<Spawner<Button>>();
    }
}
