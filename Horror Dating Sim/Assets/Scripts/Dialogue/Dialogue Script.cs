using UnityEngine;

/// <summary>
/// Dialogue script filled with characters and their lines.
/// 
/// Author: William Min
/// </summary>
[CreateAssetMenu(fileName = "New Dialogue Script", menuName = "Simulation Game Objects/Dialogue Script", order = 0)]
public class DialogueScript : ScriptableObject
{
    #region Serialized Fields

    [Header("Character Properties")]
    [Space]
    [SerializeField] private string _narratorPlaceholder = "Narrator"; // Placeholder string for the narrator name
    [SerializeField] private string _hiddenPlaceholder = "???"; // Placeholder string for any hidden names
    [Space]
    [SerializeField] private CharacterProfile[] _characters; // List of character profiles featured in the script

    [Header ("Script Properties")]
    [Space]
    [SerializeField] private DialogueLine[] _dialogueLines; // List of lines of dialogue

    #endregion

    #region Properties

    /// <summary>
    /// Returns the character profiles featured in the dialogue script.
    /// </summary>
    public CharacterProfile[] Characters { get => _characters; }

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
            line.ProcessSpeakerNames(_characters, _narratorPlaceholder, PlayerData.Instance == null ? "Player" : PlayerData.Instance.PlayerName, _hiddenPlaceholder);
            line.ProcessSpeakerNamesDisplay();
        }
    }

    #endregion
}
