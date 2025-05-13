using UnityEngine;
using UnityEngine.UI;
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
    [Space]
    [SerializeField] private int _spriteCount = 5;                              // 
    [SerializeField] private SpawnerWithParent<CharacterSprite> _spriteSpawner; //
    [Space]
    [SerializeField] private int _buttonCount = 30;                             // 
    [SerializeField] private SpawnerWithParent<Button> _buttonSpawner;          // 

    /// <summary>
    /// 
    /// </summary>
    public string DialogueText { get => _dialogueText.text; set => _dialogueText.text = value; }

    /// <summary>
    /// 
    /// </summary>
    public string NameText { get => _nameText.text; set => _nameText.text = value; }

    /// <summary>
    /// 
    /// </summary>
    public SpawnerWithParent<CharacterSprite> SpriteSpawner { get => _spriteSpawner; }

    /// <summary>
    /// 
    /// </summary>
    public SpawnerWithParent<Button> ButtonSpawner { get => _buttonSpawner; }

    //
    private void Awake()
    {
        DialogueHandler handler = GameObject.FindObjectOfType<DialogueHandler>();

        handler.OnChangeDialogue += s => _dialogueText.text = s;
        handler.OnChangeName += s => _nameText.text = s;
        handler.OnProcessScript.AddListener(() => _spriteSpawner.SetSpawnedCount(_spriteCount));
        handler.OnProcessScript.AddListener(() => _buttonSpawner.SetSpawnedCount(_buttonCount));
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
