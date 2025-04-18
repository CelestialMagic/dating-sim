using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int failures;
    public bool finishedJane;
    public bool finishedBen; 

    public delegate void OnPlayerDataChange(int failures, bool fJane, bool fBen); 
    public static event OnPlayerDataChange OnDataChange; 



    private static PlayerData _instance = null;

    public static PlayerData Instance
    {
        get{
            if(_instance == null){
                _instance = new PlayerData(0, false, false);
            }
            return _instance; 
        }
    }

    private PlayerData(int f, bool jane, bool ben){
        this.failures = f;
        this.finishedJane = jane;
        this.finishedBen = ben; 
    }

    public void SetPlayerData(int f, bool jane, bool ben){
        this.failures = f;
        this.finishedJane = jane;
        this.finishedBen = ben; 

        OnDataChange?.Invoke(f, jane, ben);
    }
}
