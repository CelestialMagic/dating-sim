using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleMenu : MonoBehaviour
{
    [SerializeField]
    private string characterSelectionScreen;

    public void LoadCharacterSelection(){
        SceneManager.LoadScene(characterSelectionScreen);
    }

    public void QuitGame(){
        Application.Quit();
    }

}
