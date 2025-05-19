using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingTitleCard : TitleCard
{

    [SerializeField]
    private TypingTimer timer;

    public void Start()
    {
        timer.StopTimer();
    }
   protected override void FadeAway()
    {

        if (startFade)
        {
            startingAlpha = Mathf.MoveTowards(startingAlpha, endingAlpha, fadeRate * Time.deltaTime);
            panel.alpha = startingAlpha;
            if (startingAlpha == endingAlpha)
            {
                panel.gameObject.SetActive(false);
                startFade = false;
                timer.StartTimer();
            }
            }
        

    }
}
