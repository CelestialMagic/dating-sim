using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField]
    private string characterName; 


    private PlayerData playerData;
    private SaveDataJSON saveDataJSON;

    public void Start(){
        playerData = FindObjectOfType<PlayerData>(); 
        saveDataJSON = FindObjectOfType<SaveDataJSON>(); 
        
    }
    
    public void OnMouseDown(){
        playerData.CurrentRoute = characterName; 
        //saveDataJSON.SaveData();

    }
}
