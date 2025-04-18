using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DistortionToggle : MonoBehaviour
{
    private PlayerData playerData;

    [SerializeField]
    private Sprite normalBackground, distortedBackground;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip normalMusic, distortedMusic; 

    [SerializeField]
    private Image backgroundImage; 

    private bool previousValue; 

    public void Start(){
        playerData = FindObjectOfType<PlayerData>();
        DistortionEvent(playerData.FullyCorrupted);
    }

    private void Update(){
        if(playerData.FullyCorrupted != previousValue){
            DistortionEvent(playerData.FullyCorrupted);
            previousValue = playerData.FullyCorrupted; 
        }
    }
    
    // Update is called once per frame
    private void DistortionEvent(bool canDistort){
        if(canDistort == true){
            audioSource.Stop();
            backgroundImage.sprite = distortedBackground;
            audioSource.PlayOneShot(distortedMusic);
        }else{
            audioSource.Stop();
            backgroundImage.sprite = normalBackground;
            audioSource.PlayOneShot(normalMusic);
        }
    }
}
