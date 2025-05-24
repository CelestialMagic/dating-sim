using UnityEngine;


public class TitleCard : MonoBehaviour
{
    [SerializeField]
    protected CanvasGroup panel; 

    [SerializeField]
    protected DialogueHandler dialogueHandler; 

    [SerializeField]
    protected float startingAlpha, endingAlpha, fadeRate;

    protected bool startFade = false; 


//Sets the FadeOnClick() value to true
    public void FadeOnClick(){
        if(dialogueHandler != null)
            dialogueHandler.ProceedWithText(); 
        startFade = true;
    }


    //FadeAway() is called using Update
    protected virtual void Update()
    {
        FadeAway(); 
    }

    //FadeAway() slowly changes the alpha value of the canvas group 
    protected virtual void FadeAway()
    {
        if(startFade)
        {
            startingAlpha = Mathf.MoveTowards(startingAlpha, endingAlpha, fadeRate * Time.deltaTime);
            panel.alpha = startingAlpha;

            if(startingAlpha == endingAlpha)
            {
                panel.gameObject.SetActive(false);
                startFade = false; 
            }
        }
    }
}
