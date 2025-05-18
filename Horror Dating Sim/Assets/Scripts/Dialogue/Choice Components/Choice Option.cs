using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 
/// 
/// Author: William Min
/// </summary>
[System.Serializable]
public struct ChoiceOption
{
    #region Serialized Fields

    [SerializeField] private Skit _skitFromChoice;              // 

    [Header("Choice Button Properties")]
    [TextArea] [SerializeField] private string _choicePrompt;   // 
    [SerializeField] private Vector2 _buttonLocalPosition;      // 
    [SerializeField] private Vector2 _buttonDimensions;         // 
    [SerializeField] private Vector2 _buttonLocalScale;         // 

    #endregion

    #region Public Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public DialogueLine GetLine(int index)
    {
        return _skitFromChoice.GetLine(index);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public int GetLineCount()
    {
        return _skitFromChoice.GetLineCount();
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetupChoice()
    {
        _skitFromChoice.SetupSkit();
    }

    /// <summary>
    /// 
    /// </summary>
    public void ProcessChoice()
    {
        _skitFromChoice.Process();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="choiceButton"></param>
    public void ModifyButton(Button choiceButton)
    {
        TMP_Text textBox = choiceButton.GetComponentInChildren<TMP_Text>();
        textBox.text = _choicePrompt;

        RectTransform buttonTransform = choiceButton.GetComponent<RectTransform>();
        buttonTransform.anchoredPosition = _buttonLocalPosition;
        buttonTransform.sizeDelta = _buttonDimensions;
        buttonTransform.localScale = _buttonLocalScale;
    }

    #endregion
}
