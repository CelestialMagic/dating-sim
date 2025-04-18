using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData: MonoBehaviour
{
    public int Failures;
    public string CurrentScene; 
    public bool FinishedJane;
    public bool FinishedBen; 

    public delegate void OnPlayerDataChange(int failures, string currentScene, bool finishedJane, bool finishedBen); 
    public static event OnPlayerDataChange OnDataChange; 

    public void Start(){
        DontDestroyOnLoad(gameObject);

    }



    private static PlayerData _instance = null;

    public static PlayerData Instance
    {
        get{
            if(_instance == null){
                _instance = new PlayerData(0, "", false, false);
            }
            return _instance; 
        }
    }

    private PlayerData(int f, string scene, bool jane, bool ben){
        this.Failures = f;
        this.CurrentScene = scene; 
        this.FinishedJane = jane;
        this.FinishedBen = ben; 
    }

    public void SetPlayerData(int f, string scene, bool jane, bool ben){
        this.Failures = f;
        this.CurrentScene = scene; 
        this.FinishedJane = jane;
        this.FinishedBen = ben; 

        OnDataChange?.Invoke(f, scene, jane, ben);
    }


}
