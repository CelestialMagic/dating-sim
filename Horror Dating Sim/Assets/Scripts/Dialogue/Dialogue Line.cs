using UnityEngine;

public struct DialogueLine
{
    #region Serialized Fields

    [SerializeField] private BossDialogueSpeaker _speaker;
    [SerializeField] private bool _speakerHidden;
    [TextArea(3, 20)] [SerializeField] private string _line;

    #endregion

    #region Properties

    public BossDialogueSpeaker Speaker { get => _speaker; }
    public bool SpeakerHidden { get => _speakerHidden; }
    public string Line { get => _line; }

    #endregion
}
