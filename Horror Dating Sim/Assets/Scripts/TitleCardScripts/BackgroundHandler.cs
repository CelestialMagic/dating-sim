using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundHandler : TitleCard
{

    const int fadedOutValue = 0, fadedInValue = 1;


    private bool canFadeIn, canFadeAway;

    [SerializeField]
    private Image backgroundImage;

    private Sprite selectedBackdrop; 

    public void SetSelectedBackdrop(Sprite sprite){
        selectedBackdrop = sprite; 
    }

    public void SetCanFadeAway(bool value)
    {
        canFadeAway = value; 
    }

/*
            [SerializeField]
            private List<BackgroundObject> backgrounds;

            [SerializeField]
            private int backgroundIndex;
        */



    //FadeAway() is called using Update
    protected override void Update()
    {
        /*
        if (backgrounds[backgroundIndex].GetIndex() == dialogueHandler.GetCurrentIndex() && !backgrounds[backgroundIndex].GetHasBeenTransitioned())
        {
            canFadeAway = true;
            backgrounds[backgroundIndex].SetHasBeenTransitioned(true);
        }
        */

        if (canFadeIn)
            FadeIn();
        else if (canFadeAway)
            FadeAway();
    }


    

    //FadeAway() slowly changes the alpha value of the canvas group 
    protected override void FadeAway()
    {
        if (canFadeAway)
        {
            startingAlpha = Mathf.MoveTowards(startingAlpha, fadedOutValue, fadeRate * Time.deltaTime);
            panel.alpha = startingAlpha;
            if (startingAlpha == fadedOutValue)
            {
                canFadeAway = false;
                //backgroundImage.sprite = backgrounds[backgroundIndex].GetBackground();
                backgroundImage.sprite = selectedBackdrop;  
                canFadeIn = true; 
            }

        }

    }

   private void FadeIn(){
        if(canFadeIn){
            startingAlpha = Mathf.MoveTowards(startingAlpha, fadedInValue, fadeRate * Time.deltaTime);
            panel.alpha = startingAlpha;
            if (startingAlpha == fadedInValue)
            {
                canFadeIn = false;
                /*if(backgroundIndex + 1 != backgrounds.Count )
                    backgroundIndex++;
                */
            }

        }

   }
}
