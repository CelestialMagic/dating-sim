using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypingManager : MonoBehaviour
{

    [SerializeField]
    protected TMP_InputField inputField;//A field to enter in the text

    [SerializeField]
    protected TMP_Text promptText, storyTracker;//Text to prompt the user to type the word 

    [SerializeField]
    protected TypingPrompt typingPrompt;

    protected List<string> promptWords;//A list of words that will be randomly selected.



    protected string selectedWord; //A word from the list of strings in promptWords

    protected int index = 0; //An index to track the current string

    [SerializeField]
    protected AudioSource audioSource;

    [SerializeField]
    protected AudioClip typeSound;

    public bool wonMinigame = false; 

    // Start is called before the first frame update
    //Sets the input field active
    protected void Start()
    {
        inputField.ActivateInputField();
        promptWords = typingPrompt.GetWords();
        SetSelectedWord(promptWords[index]);
        SetPromptText();
    }

    protected void UpdateStory(){
        storyTracker.text += " " + promptWords[index];
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(inputField.isFocused && Input.anyKeyDown){
            TypingSFX();
        }
        inputField.Select(); 
        if(String.Equals(inputField.text, selectedWord, StringComparison.OrdinalIgnoreCase) && inputField.text != String.Empty){
            inputField.text = String.Empty; 
            UpdateStory();
            AdvanceSelectedWord(); 
        }
        
    }

    protected void SelectRandomWord(){
        SetSelectedWord(promptWords[UnityEngine.Random.Range(0, promptWords.Count)]);
    }

    protected void AdvanceSelectedWord(){
        
        if(index < promptWords.Count && index + 1 != promptWords.Count){
            index++;
            SetSelectedWord(promptWords[index]);
            SetPromptText();
        }else{
            selectedWord = "";
            SetPromptText();
            promptText.gameObject.SetActive(false);
            wonMinigame = true;
        }
            
        
    }

    protected void SetPromptText(){
           promptText.text = selectedWord;
    }

    protected void SetSelectedWord(string word){
        selectedWord = word;
    }

    protected void TypingSFX()
    {
        int[] Semitones = new[] {0, 2, 4, 7, 9};
        int random = UnityEngine.Random.Range(0, 2);
        audioSource.pitch = 2f;

        for (int i = 0; i < Semitones[random]; i++)
            audioSource.pitch *= 1.059463f;

        audioSource.PlayOneShot(typeSound);
    }

}
