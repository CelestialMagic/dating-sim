using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuDistortionToggle : DistortionToggle
{

    [SerializeField]
    private Image backgroundImage; 

    protected override void DistortionEvent(bool canDistort){
        if(canDistort == true){
            audioSource.Stop();
            backgroundImage.sprite = distortedBackground;
            audioSource.clip = distortedMusic;
            audioSource.Play();
            SwapFonts(distortedFont);
            
        }else{
            audioSource.Stop();
            backgroundImage.sprite = normalBackground;
            audioSource.clip = normalMusic;
            audioSource.Play();
            SwapFonts(normalFont);
        }
    }
}
