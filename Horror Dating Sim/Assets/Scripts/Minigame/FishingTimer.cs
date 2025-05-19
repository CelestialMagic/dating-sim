using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FishingTimer : TypingTimer
{
    [SerializeField]
    private Catcher catcher;

    [SerializeField]
    private GameObject firstMinigameEndScreen;

    [SerializeField]
    private TMP_Text caughtFishText;

    [SerializeField]
    private AudioSource music; 
    

    protected override void Update()
    {
        if (stopTimer == false)
        {
            currentTime -= Time.deltaTime;
            UpdateText();

        }

        if (catcher.GetCaughtTarget())
        {
            StopTimer();
            NextScene();

        }

        if (currentTime <= 0)
        {
            if (catcher.GetIsFirstMinigame() == true)
            {
                firstMinigameEndScreen.SetActive(true);
                caughtFishText.text = $"You caught {catcher.GetCaughtItems()} fish.";

            }
            else if (catcher.GetCaughtTarget())
            {
                NextScene();

            }
            else
            {
                SetCanvasVisibility(lostCanvas, true);
                music.Stop();
                //SetCanvasVisibility(minigameCanvas, false);

            }

        }


    }
}
