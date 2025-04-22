using UnityEngine;

/// <summary>
/// 
/// 
/// Author: William Min
/// </summary>
[CreateAssetMenu(fileName = "New Dialogue Script", menuName = "Dialogue Script", order = 1)]
public class DialogueScript : ScriptableObject
{
    #region Serialized Fields
    
    [SerializeField] private LineOfDialogue[] _dialogueLines; // 

    #endregion

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public LineOfDialogue[] DialogueLines { get => _dialogueLines; }

    #endregion
}

/// <summary>
/// 
/// 
/// Author: William Min
/// </summary>
[System.Serializable]
public class LineOfDialogue
{
    #region Serialized Fields

    [SerializeField] private int _speakerIndex; // 
    [SerializeField] private bool _speakerIsHidden; // 
    [TextArea(3, 20)] [SerializeField] private string _line; // 

    #endregion

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public int SpeakerIndex { get => _speakerIndex; }

    /// <summary>
    /// 
    /// </summary>
    public bool SpeakerIsHidden { get => _speakerIsHidden; }

    /// <summary>
    /// 
    /// </summary>
    public string Line { get => _line; }

    #endregion
}