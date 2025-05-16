using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Movement : MonoBehaviour
{
    [SerializeField] GameObject hook;
    [SerializeField] private float minY, maxY;
    private bool limitReached = false;

    void Start()
    {

    }

    void Update()
    {
        CheckY();
        if (limitReached)
        {
            
        }

        else
        {
            transform.position = new Vector3(transform.position.x, hook.transform.position.y, transform.position.z);
        }
    }

    private void CheckY()
    {
        if (hook.transform.position.y > maxY)
        {
            limitReached = true;
        }

        else if (hook.transform.position.y < minY)
        {
            limitReached = true;
        }

        else
        {
            limitReached = false;
        }

    }
}