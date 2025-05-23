using UnityEngine;
using UnityEngine.UI;

public class BackgroundHandler : TitleCard
{
    [SerializeField] private Image backgroundImage;

    private bool canFadeIn, canFadeAway;
    private Sprite selectedBackdrop;

    const int fadedOutValue = 0, fadedInValue = 1;

    public void SetSelectedBackdrop(Sprite sprite)
    {
        selectedBackdrop = sprite; 
    }

    public void SetCanFadeAway(bool value)
    {
        canFadeAway = value; 
    }

    //FadeAway() is called using Update
    protected override void Update()
    {
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
                backgroundImage.sprite = selectedBackdrop;  
                canFadeIn = true; 
            }
        }
    }

   private void FadeIn()
    {
        if(canFadeIn)
        {
            startingAlpha = Mathf.MoveTowards(startingAlpha, fadedInValue, fadeRate * Time.deltaTime);
            panel.alpha = startingAlpha;

            if (startingAlpha == fadedInValue)
            {
                canFadeIn = false;
            }
        }
   }
}
