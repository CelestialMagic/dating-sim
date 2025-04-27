using UnityEngine;

/// <summary>
/// Dialogue script filled with characters and their lines
/// 
/// Author: William Min
/// </summary>
[CreateAssetMenu(fileName = "New Dialogue Script", menuName = "Simulation Game Objects/Dialogue Script", order = 0)]
public class DialogueScript : ScriptableObject
{
    #region Serialized Fields

    [SerializeField] private CharacterProfile[] _characters; // 
    [SerializeField] private DialogueLine[] _dialogueLines; // 

    #endregion

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public CharacterProfile[] Characters { get => _characters; }

    /// <summary>
    /// 
    /// </summary>
    public DialogueLine[] DialogueLines { get => _dialogueLines; }

    #endregion

    #region Public Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public CharacterList GetSpeakersFromLineIndex(int index)
    {
        if (index < 0 || index >= _dialogueLines.Length) return null;

        return _dialogueLines[index].GetSpeakerProfiles(_characters);
    }

    #endregion
}
