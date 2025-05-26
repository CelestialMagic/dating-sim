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

    [SerializeField]
    private SpriteRenderer renderer; 

    [SerializeField]
    private Color selectedColor, unSelectedColor, highlightingColor;

    [SerializeField]
    private Sprite corruptedBen, corruptedJane;

    [SerializeField]
    private SpriteRenderer janeRenderer, benRenderer; 

    public bool isSelected = false; 
    
    


      public void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        name.text = character.characterName;
        UnselectCharacter();
        ToggleTextVisibility(false);
        //saveDataJSON = FindObjectOfType<SaveDataJSON>(); 
        switch (character.internalName)
        {
            case CharacterSelect.CharacterName.Ben:
                if (playerData.FinishedBen)
                    benRenderer.sprite = corruptedBen; 
                break;
            case CharacterSelect.CharacterName.Jane:
                if (playerData.FinishedJane)
                    janeRenderer.sprite = corruptedJane;
                break;
            default:
                break;
        }

    }
    
    public void OnMouseDown(){
        isSelected = true;
        SelectCharacter(); 
        DeselectOtherCharacters(); 
        playerData.CurrentRoute = character.internalName; 
        RouteManager.SetRouteStart(character.internalName);
        //saveDataJSON.SaveData();
    }

    public void OnMouseOver(){
        ToggleTextVisibility(true);
        HighlightCharacter();

    }

    public void OnMouseExit(){
        if(isSelected != true)
            ToggleTextVisibility(false);
        UnselectCharacter(); 
        
    }

    public void ToggleTextVisibility(bool isVisible){
        textObject.SetActive(isVisible);

    }

    public void HighlightCharacter(){
        if(isSelected != true)
            renderer.color = highlightingColor;
    }

    public void SelectCharacter(){
        renderer.color = selectedColor;
    }

    public void UnselectCharacter(){
        if(isSelected != true)
            renderer.color = unSelectedColor;
            

    }

    private void DeselectOtherCharacters(){
        SelectableObject[] allSelectables = FindObjectsOfType<SelectableObject>();
        foreach(SelectableObject s in allSelectables){
            if(s == this)
                continue;
            else{
                s.isSelected = false; 
                s.UnselectCharacter();
                s.ToggleTextVisibility(false);
            }
                
        }


    }



}
