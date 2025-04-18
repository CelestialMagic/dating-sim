using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class DistortionToggle : MonoBehaviour
{
    protected PlayerData playerData;//The player data to refer to

    [SerializeField]
    protected Sprite normalBackground, distortedBackground;//The normal and regular BG Art

    [SerializeField]
    protected AudioSource audioSource;//The audio source to play music

    [SerializeField]
    protected AudioClip normalMusic, distortedMusic;//The music clips 

    protected bool previousValue;//Used to determine when to switch between normal and distorted versions

    [SerializeField]
    private SpriteRenderer backgroundArt;//Used for scenes where a sprite is used

    [SerializeField]
    protected List<TMP_Text> textToChangeFont;//A list of text to change fonts

    [SerializeField]
    protected TMP_FontAsset normalFont, distortedFont; 


    protected void Start(){
        playerData = FindObjectOfType<PlayerData>();
        DistortionEvent(playerData.FullyCorrupted);
    }

    protected void Update(){
        if(playerData.FullyCorrupted != previousValue){
            DistortionEvent(playerData.FullyCorrupted);
            previousValue = playerData.FullyCorrupted; 
        }
    }
    
    // Update is called once per frame
    protected virtual void DistortionEvent(bool canDistort){
        if(canDistort == true){
            audioSource.Stop();
            backgroundArt.sprite = distortedBackground;
            audioSource.PlayOneShot(distortedMusic);
            SwapFonts(normalFont);
        }else{
            audioSource.Stop();
            backgroundArt.sprite = normalBackground;
            audioSource.PlayOneShot(normalMusic);
            SwapFonts(distortedFont);
        }
    }

    protected virtual void SwapFonts(TMP_FontAsset fontAsset){
        foreach(TMP_Text t in textToChangeFont){
            t.font = fontAsset; 
        }
    }
}
