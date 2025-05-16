using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField]
    private SceneField sceneToLoad;

    public void LoadNextScene(){
        SceneManager.LoadScene(sceneToLoad);
    }
}
