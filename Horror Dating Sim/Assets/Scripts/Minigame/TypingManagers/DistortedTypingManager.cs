using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DistortedTypingManager : TypingManager
{

    [SerializeField]
    private AudioSource backgroundMusic;

    [SerializeField]
    private float pitchModifier;

    // Start is called before the first frame update
     protected override void Update()
    {
        if(inputField.isFocused && Input.anyKeyDown){
            TypingSFX();
        }
        inputField.Select(); 
        if(String.Equals(inputField.text, selectedWord, StringComparison.OrdinalIgnoreCase) && inputField.text != String.Empty){
            inputField.text = String.Empty; 
            UpdateStory();
            AdvanceSelectedWord(); 
            DistortAudio(); 
        }
        
    }

    //A method to progress distortion
    private void DistortAudio(){
        backgroundMusic.pitch += pitchModifier;
    }
}
