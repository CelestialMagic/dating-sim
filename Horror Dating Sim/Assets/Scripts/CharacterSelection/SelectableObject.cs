using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectableObject : MonoBehaviour
{
    [SerializeField]
    private CharacterSelect character;

    [SerializeField]
    private GameObject textObject; 

    [SerializeField]
    private TMP_Text name; 

    private PlayerData playerData;



      public void Start(){
        playerData = FindObjectOfType<PlayerData>();
        name.text = character.characterName;  
        ToggleTextVisibility(false);
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

    public void OnMouseOver(){
        ToggleTextVisibility(true);

    }

    public void OnMouseExit(){
        ToggleTextVisibility(false);
    }

    private void ToggleTextVisibility(bool isVisible){
        textObject.SetActive(isVisible);

    }



}
