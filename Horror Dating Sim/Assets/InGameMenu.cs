using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{

    [SerializeField]
    private SceneField mainMenu;

    [SerializeField]
    private GameObject popUpMenu;
    
    public void ReturnToMenu(){
        SceneManager.LoadScene(mainMenu);
    }

    public void ShowPopUp(){
        popUpMenu.SetActive(true);
    }

    public void HidePopUp(){
        popUpMenu.SetActive(false);
    }
}
