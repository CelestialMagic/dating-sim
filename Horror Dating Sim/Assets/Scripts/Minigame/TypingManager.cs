using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypingManager : MonoBehaviour
{

    [SerializeField]
    private TMP_InputField inputField;
    // Start is called before the first frame update
    void Start()
    {
        inputField.ActivateInputField();
    }

    // Update is called once per frame
    void Update()
    {
        inputField.Select(); 
        
    }
}
