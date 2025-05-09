using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TypingTimer : MonoBehaviour
{

    [SerializeField]
    private TMP_Text timerText; 

    [SerializeField]
    private float originalTime; 
    private float currentTime;

    private bool stopTimer = false;

    [SerializeField]
    private TypingManager typingManager; 

    [SerializeField]
    private GameObject lostCanvas, minigameCanvas; 

    [SerializeField]
    private string nextScene, resetScene;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = originalTime;
        UpdateText();
    }

    private void SetCanvasVisibility(GameObject canvas, bool value){
        canvas.SetActive(value);

    }
        
    


    private void UpdateText(){
        timerText.text = $"{Mathf.FloorToInt(currentTime)}";
    }

    // Update is called once per frame
    void Update()
    {
        if(stopTimer == false){
            currentTime -= Time.deltaTime;
            UpdateText();

        }

        if(typingManager.wonMinigame){
            StopTimer();
            NextScene();

        }
        
        if(currentTime <= 0){
            if(typingManager.wonMinigame){
                NextScene();

            }
            else{
                SetCanvasVisibility(lostCanvas, true);
                SetCanvasVisibility(minigameCanvas, false);

            }

        }
    
        
    }

    public void StopTimer(){
        stopTimer = true; 
    }

    public void ResetScene(){
        SceneManager.LoadScene(resetScene);
    }

    public void NextScene(){
        SceneManager.LoadScene(nextScene);
    }


}
