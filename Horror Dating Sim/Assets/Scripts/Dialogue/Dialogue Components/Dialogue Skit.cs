using UnityEngine;

/// <summary>
/// Dialogue script filled with characters and their lines.
/// 
/// Author: William Min
/// </summary>
[CreateAssetMenu(fileName = "New Dialogue Skit", menuName = "Simulation Game Objects/Dialogue Skit", order = 0)]
public class DialogueSkit : ScriptableObject
{
    #region Serialized Fields

    [Header("Script Properties")]
    [SerializeField] private int _spriteCount; // 
    [Space]
    [SerializeField] private DialogueLine[] _dialogueLines; // List of lines of dialogue

    #endregion

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public int SpriteCount { get => _spriteCount; }

    /// <summary>
    /// Returns the list of lines of dialogue, each containing the line itself and various display properties.
    /// </summary>
    public DialogueLine[] DialogueLines { get => _dialogueLines; }

    #endregion

    #region Public Methods

    /// <summary>
    /// Processes the dialogue script.
    /// </summary>
    public void Process()
    {
        foreach (DialogueLine line in _dialogueLines)
        {
            line.ProcessSpeakerNames();
            line.ProcessSpeakerNamesDisplay();
        }
    }

    #endregion
}
