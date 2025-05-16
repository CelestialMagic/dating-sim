using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish_Spawner : MonoBehaviour
{
    [SerializeField]private GameObject fishPrefab;
    [SerializeField]private float spawnTime;
    private Vector2 screenBounds;
    private float randY;
    private float randX;


    void Start()
    {
        StartCoroutine(StartSpawning());
    }

    void Update()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

    }

    private void Spawn()
    {
        RandomY();
        LorR();
        GameObject fish = Instantiate(fishPrefab) as GameObject;
        fish.transform.position = new Vector2(randX, randY);
    }

    private void RandomY()
    {
        if(screenBounds.y > 0)
        {
            randY = Random.Range(screenBounds.y - screenBounds.y * 2, 0);
            Debug.Log("Greater than 0");
        }

        if(screenBounds.y < 0)
        {
            randY = Random.Range(screenBounds.y + screenBounds.y * 2, screenBounds.y);
            Debug.Log("Lesser than 0");
        }
    }

    private void LorR()
    {
        randX = Random.Range(0, 2);
        if (randX == 0)
        {
            randX = screenBounds.x;
        }

        else
        {
            randX = -screenBounds.x;
        }
    }

    IEnumerator StartSpawning()
    {
        while (true)
        {
             yield return new WaitForSeconds(spawnTime);
             Spawn();
        }
       
    }
}
