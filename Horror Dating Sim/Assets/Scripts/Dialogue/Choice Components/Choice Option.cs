using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 
/// 
/// Author: William Min
/// </summary>
[System.Serializable]
public class ChoiceOption
{
    #region Serialized Fields

    [SerializeField] private Skit _skitFromChoice;              // 

    [Header("Choice Button Properties")]
    [TextArea] [SerializeField] private string _choicePrompt;   // 
    [SerializeField] private ToggledField<Vector2> _localPosition;  // Local position of button in space
    [SerializeField] private ToggledField<Vector2> _dimensions;     // Dimensions of button
    [SerializeField] private ToggledField<Vector2> _localScale;     // Lacl scale of button in space

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
        if (_localPosition.IsEnabled) buttonTransform.anchoredPosition = _localPosition.Value;
        if (_dimensions.IsEnabled) buttonTransform.sizeDelta = _dimensions.Value;
        if (_localScale.IsEnabled) buttonTransform.localScale = _localScale.Value;
    }

    #endregion
}
