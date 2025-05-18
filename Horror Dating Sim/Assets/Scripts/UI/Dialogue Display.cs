using UnityEngine;
using TMPro;

/// <summary>
/// 
/// 
/// Author: William Min
/// </summary>
public class DialogueDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _dialogueText;    // Reference to text box containing current dialogue line
    [SerializeField] private TMP_Text _nameText;        // Reference to text box containing current speaker name
    [SerializeField] private GameObject _proceedHolder; // 
    [SerializeField] private GameObject _choiceHolder;  // 

    /// <summary>
    /// 
    /// </summary>
    public string DialogueText { get => _dialogueText.text; set => _dialogueText.text = value; }

    /// <summary>
    /// 
    /// </summary>
    public string NameText { get => _nameText.text; set => _nameText.text = value; }

    //
    private void Awake()
    {
        DialogueHandler handler = FindObjectOfType<DialogueHandler>();

        handler.OnChangeDialogue += s => _dialogueText.text = s;
        handler.OnChangeName += s => _nameText.text = s;
        handler.OnSkipDialogue.AddListener(() => ToggleChoiceHolder(false));
        handler.OnBeginLine.AddListener(() => ToggleProceedHolder(true));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isActive"></param>
    public void ToggleProceedHolder(bool isActive)
    {
        _proceedHolder.SetActive(isActive);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isActive"></param>
    public void ToggleChoiceHolder(bool isActive)
    {
        _choiceHolder.SetActive(isActive);
    }
}
