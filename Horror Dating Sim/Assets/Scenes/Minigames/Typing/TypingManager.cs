using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypingManager : MonoBehaviour
{

    [SerializeField]
    private TMP_InputField inputField;//A field to enter in the text

    [SerializeField]
    private TMP_Text promptText, storyTracker;//Text to prompt the user to type the word 

    [SerializeField]
    private List<string> promptWords;//A list of words that will be randomly selected.

    private string selectedWord; //A word from the list of strings in promptWords

    private int index = 0; //An index to track the current string

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip typeSound;

    // Start is called before the first frame update
    //Sets the input field active
    void Start()
    {
        inputField.ActivateInputField();
        SetSelectedWord(promptWords[index]);
        SetPromptText();
    }

    void UpdateStory(){
        storyTracker.text += " " + promptWords[index];
    }

    // Update is called once per frame
    void Update()
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

    private void SelectRandomWord(){
        SetSelectedWord(promptWords[UnityEngine.Random.Range(0, promptWords.Count)]);
    }

    private void AdvanceSelectedWord(){
        
        if(index < promptWords.Count && index + 1 != promptWords.Count){
            index++;
            SetSelectedWord(promptWords[index]);
            SetPromptText();
        }else{
            selectedWord = "";
            SetPromptText();
        }
            
        
    }

    private void SetPromptText(){
           promptText.text = selectedWord;
    }

    private void SetSelectedWord(string word){
        selectedWord = word;
    }

    public void TypingSFX()
    {
        int[] Semitones = new[] {0, 2, 4, 7, 9};
        int random = UnityEngine.Random.Range(0, 2);
        audioSource.pitch = 2f;

        for (int i = 0; i < Semitones[random]; i++)
            audioSource.pitch *= 1.059463f;

        audioSource.PlayOneShot(typeSound);
    }

}
