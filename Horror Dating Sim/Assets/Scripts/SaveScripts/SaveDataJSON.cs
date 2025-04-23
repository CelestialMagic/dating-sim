using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveDataJSON : MonoBehaviour
{
    private PlayerData playerData;


    // Start is called before the first frame update
    void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
    }

    public void SaveData(){
        string json = JsonUtility.ToJson(playerData);
        Debug.Log(json);

        using(StreamWriter writer = new StreamWriter(Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json"))
        {
            writer.Write(json);
        }

    }

    public void LoadData(){

        string json = string.Empty;

        using(StreamReader reader = new StreamReader(Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json")){
            json = reader.ReadToEnd();
        }
        PlayerData data = JsonUtility.FromJson<PlayerData>(json);
        playerData.SetAllData(data.CurrentRoute, data.CurrentScene, data.FinishedJane, data.FinishedBen, data.FullyCorrupted, data.PlayerName);

    }
}
