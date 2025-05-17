using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BackgroundObject
{
    [SerializeField]
    private int index;

    [SerializeField]
    private Sprite background;

    [SerializeField]
    private bool hasBeenTransitioned;

    public int GetIndex()
    {
        return index;
    }

    public Sprite GetBackground()
    {
        return background;
    }

    public bool GetHasBeenTransitioned()
    {
        return hasBeenTransitioned;
    }

    public void SetHasBeenTransitioned(bool transition)
    { 
        hasBeenTransitioned = transition; 
    }


}
