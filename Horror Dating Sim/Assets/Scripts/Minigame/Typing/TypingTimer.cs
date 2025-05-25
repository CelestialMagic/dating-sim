using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TypingTimer : MonoBehaviour
{

    [SerializeField]
    protected TMP_Text timerText; 

    [SerializeField]
    protected float originalTime; 
    protected float currentTime;

    protected bool stopTimer = false;

    [SerializeField]
    private TypingManager typingManager; 

    [SerializeField]
    protected GameObject lostCanvas, minigameCanvas; 

    [SerializeField]
    protected SceneField nextScene, resetScene;

    // Start is called before the first frame update
    protected void Start()
    {
        currentTime = originalTime;
        UpdateText();
    }

    protected void SetCanvasVisibility(GameObject canvas, bool value){
        canvas.SetActive(value);

    }
        
    


    protected void UpdateText(){
        timerText.text = $"{Mathf.FloorToInt(currentTime)}";
    }

    // Update is called once per frame
    protected virtual void Update()
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

    public void StartTimer()
    {
        stopTimer = false; 
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(resetScene);
    }

    public void NextScene(){
        SceneManager.LoadScene(nextScene);
    }


}
