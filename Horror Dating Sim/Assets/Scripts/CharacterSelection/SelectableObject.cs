using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    [SerializeField]
    private CharacterSelect character;

    private PlayerData playerData;


      public void Start(){
        playerData = FindObjectOfType<PlayerData>(); 
        //saveDataJSON = FindObjectOfType<SaveDataJSON>(); 
        switch(character.internalName){
            case CharacterSelect.CharacterName.Ben:
                if(playerData.FinishedBen)
                    gameObject.SetActive(false);
                break;
            case CharacterSelect.CharacterName.Jane:
                if(playerData.FinishedJane)
                    gameObject.SetActive(false);
                break;
            default:
            break; 
        }

    }
    
    public void OnMouseDown(){
        playerData.CurrentRoute = character.internalName; 
        RouteManager.SetRouteStart(character.internalName);
        //saveDataJSON.SaveData();
    }

}
