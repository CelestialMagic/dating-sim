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
    [TextArea] [SerializeField] private string _choicePrompt;   // Text prompt for choice button

    [Header("Choice Button Transform Settings")]
    [SerializeField] private ToggledField<Vector2> _localPosition;  // Local position of button in space
    [SerializeField] private ToggledField<Vector2> _dimensions;     // Dimensions of button
    [SerializeField] private ToggledField<Vector2> _localScale;     // Local scale of button in space

    [Header("Choice Button Sprite Settings")]
    [SerializeField] private ToggledField<Sprite> _buttonSprite;    // Sprite for button
    [SerializeField] private ToggledField<Color> _buttonBaseColor;  // Base color for button
    [SerializeField] private ToggledField<float> _pixelMultiplier;  // Pixel multiplier for button

    [Header("Choice Button Font Settings")]
    [SerializeField] private ToggledField<TMP_FontAsset> _font;     // Font for button text
    [SerializeField] private ToggledField<Color> _fontVectorColor;  // Base color for text
    [SerializeField] private ToggledField<float> _fontSize;         // Size of font for text

    #endregion

    #region Public Methods

    /// <summary>
    /// Returns the dialogue line from the index.
    /// </summary>
    /// <param name="index"></param>
    /// <returns>The dialogue line object</returns>
    public DialogueLine GetLine(int index)
    {
        return _skitFromChoice.GetLine(index);
    }

    /// <summary>
    /// Gets the dialogue line count in the choice.
    /// </summary>
    /// <returns></returns>
    public int GetLineCount()
    {
        return _skitFromChoice.GetLineCount();
    }

    /// <summary>
    /// Sets up all skits in the choice.
    /// </summary>
    public void SetupChoice()
    {
        _skitFromChoice.SetupSkit();
    }

    /// <summary>
    /// Processes the choices of the skits.
    /// </summary>
    public void ProcessChoice()
    {
        _skitFromChoice.Process();
    }

    /// <summary>
    /// Modifies a referenc to a button component.
    /// </summary>
    /// <param name="choiceButton">Button component reference</param>
    public void ModifyButton(Button choiceButton)
    {
        RectTransform buttonTransform = choiceButton.GetComponent<RectTransform>();

        if (buttonTransform != null)
        {
            if (_localPosition.IsEnabled) buttonTransform.anchoredPosition = _localPosition.Value;
            if (_dimensions.IsEnabled) buttonTransform.sizeDelta = _dimensions.Value;
            if (_localScale.IsEnabled) buttonTransform.localScale = _localScale.Value;
        }

        Image buttonImage = choiceButton.GetComponent<Image>();

        if (buttonImage != null)
        {
            if (_buttonSprite.IsEnabled) buttonImage.sprite = _buttonSprite.Value;
            if (_buttonBaseColor.IsEnabled) buttonImage.color = _buttonBaseColor.Value;
            if (_pixelMultiplier.IsEnabled) buttonImage.pixelsPerUnitMultiplier = _pixelMultiplier.Value;
        }

        TMP_Text textBox = choiceButton.GetComponentInChildren<TMP_Text>();

        if (textBox != null)
        {
            textBox.text = _choicePrompt;
            if (_font.IsEnabled) textBox.font = _font.Value;
            if (_fontVectorColor.IsEnabled) textBox.color = _fontVectorColor.Value;
            if (_fontSize.IsEnabled) textBox.fontSize = _fontSize.Value;
        }
    }

    #endregion
}
