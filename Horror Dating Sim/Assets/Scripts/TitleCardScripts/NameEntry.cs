using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class NameEntry : MonoBehaviour
{

    [SerializeField]
    private TMP_InputField nameEntry;

    [SerializeField]
    private string normalGreeting, distortedGreeting; 

    [SerializeField]
    private TMP_Text greetingText; 

    private PlayerData playerData; 
    // Start is called before the first frame update
    void Start()
    {
       playerData = FindObjectOfType<PlayerData>(); 
    }

    private void Update(){
        if(playerData.FullyCorrupted && playerData.PlayerName != "")
            greetingText.text = distortedGreeting;
        else
            greetingText.text = normalGreeting; 

    }

    public void SetPlayerName(){
        playerData.PlayerName = nameEntry.text; 
    }

   
}
