using UnityEngine;

/// <summary>
/// Dialogue skit filled with various dialogue lines.
/// 
/// Author: William Min
/// </summary>
[CreateAssetMenu(fileName = "New Dialogue Skit", menuName = "Simulation Game Objects/Dialogue Skit", order = 0)]
public class DialogueSkit : Skit
{
    #region Serialized Fields

    [Header("Dialogue Skit Properties")]
    [SerializeField] private DialogueLine[] _dialogueLines; // List of lines of dialogue

    #endregion

    #region Properties

    /// <summary>
    /// Returns the list of lines of dialogue, each containing the line itself and various display properties.
    /// </summary>
    public DialogueLine[] DialogueLines { get => _dialogueLines; }

    #endregion

    #region Skit Callbacks

    /// <summary>
    /// Returns a dialogue line based on index.
    /// </summary>
    /// <param name="index">Index of the dialogue line in skit.</param>
    public override DialogueLine GetLine(int index)
    {
        return index < 0 || index >= GetLineCount() ? null : _dialogueLines[index];
    }

    /// <summary>
    /// Returns the number of lines in the skit.
    /// </summary>
    /// <returns>Number of lines in skit</returns>
    public override int GetLineCount()
    {
        return _dialogueLines.Length;
    }

    /// <summary>
    /// Processes the skit before played in dialogue handler.
    /// </summary>
    public override void Process()
    {
        base.Process();

        foreach (DialogueLine line in _dialogueLines)
            line.ProcessSpeakerNames();
    }

    #endregion
}
