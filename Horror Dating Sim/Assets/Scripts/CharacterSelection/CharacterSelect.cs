using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField]
    private string characterName; 

    [SerializeField]
    private CharacterName internalName; 
    private PlayerData playerData;
    private SaveDataJSON saveDataJSON;


    public enum CharacterName{
        Ben,
        Jane,
    }


//Used to check for which characters have been completed 
    public void Start(){
        playerData = FindObjectOfType<PlayerData>(); 
        saveDataJSON = FindObjectOfType<SaveDataJSON>(); 
        switch(internalName){
            case CharacterName.Ben:
                if(playerData.FinishedBen)
                    gameObject.SetActive(false);
                break;
            case CharacterName.Jane:
                if(playerData.FinishedJane)
                    gameObject.SetActive(false);
                break;
            default:
            break; 
        }

    }
    
    public void OnMouseDown(){
        playerData.CurrentRoute = characterName; 
        //saveDataJSON.SaveData();

    }
}
