using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerData: MonoBehaviour
{
    public string CurrentRoute;
    public string CurrentScene; 
    public bool FinishedJane;
    public bool FinishedBen; 

    public bool FullyCorrupted;

    public void Start(){
        DontDestroyOnLoad(gameObject);
    }

    public void SetAllData(string route, string scene, bool jane, bool ben, bool corrupted){
        CurrentRoute = route;
        CurrentScene = scene;
        FinishedJane = jane; 
        FinishedBen = ben;
        FullyCorrupted = corrupted; 
    }

    




}
