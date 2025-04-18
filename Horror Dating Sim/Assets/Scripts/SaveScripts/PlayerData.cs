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

    public static PlayerData Instance; 

    void Awake(){
        InstantiatePlayerData();

    }

//Removes Repeating PlayerData Bug
    private void InstantiatePlayerData(){
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != Instance)
        {
            Destroy(this.gameObject);
        }

    } 

    public void SetAllData(string route, string scene, bool jane, bool ben, bool corrupted){
        CurrentRoute = route;
        CurrentScene = scene;
        FinishedJane = jane; 
        FinishedBen = ben;
        FullyCorrupted = corrupted; 
    }

    




}
