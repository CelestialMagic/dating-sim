using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcher : MonoBehaviour
{

    [SerializeField]
    private int caughtItems;

    [SerializeField]
    private string targetTag;

    [SerializeField]
    private bool isFirstMinigame;

    [SerializeField]
    private bool caughtTarget;

    public bool GetCaughtTarget()
    {
        return caughtTarget; 
    }

    public bool GetIsFirstMinigame()
    {
        return isFirstMinigame; 
    }

    public int GetCaughtItems()
    {
        return caughtItems;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            caughtTarget = true;
        }
        else if (!collision.CompareTag("Player"))
        {
            caughtItems++;

        }

        
    }
}
