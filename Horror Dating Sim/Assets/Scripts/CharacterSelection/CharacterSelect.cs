using UnityEngine;


[CreateAssetMenu(fileName = "New Character Selection", menuName = "Player Data Scriptables/Character Selection", order = 1)]
public class CharacterSelect : ScriptableObject
{
    [SerializeField]
    public string characterName; 

    [SerializeField]
    public CharacterName internalName;

    public enum CharacterName{
        Ben,
        Jane,
        NA, 
    }
    
}
