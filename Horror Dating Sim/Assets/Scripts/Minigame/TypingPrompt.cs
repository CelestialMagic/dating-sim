using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Typing Prompt", menuName = "Minigames/Typing", order = 1)]
public class TypingPrompt : ScriptableObject
{
    [TextArea(5,10)]
    [SerializeField]
    private string sentence;//A sentence or paragraph to reference

//Takes any provided sentence and breaks it into a list of words. 
    public List<string> GetWords(){
        List<string> words = new List<string>(sentence.Split(' '));
        return words; 

    }


}
