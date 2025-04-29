using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TitleCard : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup panel; 

    [SerializeField]
    private DialogueHandler dialogueHandler; 

    [SerializeField]
    private float startingAlpha, endingAlpha, fadeRate;

    private bool startFade = false; 

//Sets the FadeOnClick() value to true
    public void FadeOnClick(){
        dialogueHandler.ProceedWithText(); 
        startFade = true;
    }

//FadeAway() is called using Update
    private void Update(){
        FadeAway(); 
    }

//FadeAway() slowly changes the alpha value of the canvas group 
   private void FadeAway(){
        if(startFade){
            startingAlpha = Mathf.MoveTowards(startingAlpha, endingAlpha, fadeRate * Time.deltaTime);
            panel.alpha = startingAlpha;
            if(startingAlpha == endingAlpha){
                panel.gameObject.SetActive(false);
                startFade = false; 
            }
            

        }

   }
}
