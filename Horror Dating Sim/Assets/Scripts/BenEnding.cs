using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenEnding : MonoBehaviour
{
    private PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        playerData.FullyCorrupted = true;
        playerData.FinishedBen = true; 
    }
}
