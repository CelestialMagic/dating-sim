using UnityEngine;

[CreateAssetMenu(fileName = "New Character Profile", menuName = "Simulation Game Objects/Character Profile", order = 1)]
public class CharacterProfile : ScriptableObject
{
    [SerializeField] private string _characterName;

    public string CharacterName { get => _characterName; }
}
