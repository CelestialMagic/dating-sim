using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAdvanceCard : TitleCard
{
    // Start is called before the first frame update
    void Start()
    {
        startFade = true; 
        dialogueHandler.ProceedWithText();
    }

}
