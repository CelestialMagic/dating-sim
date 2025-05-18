using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Dialogue script filled with characters and their lines.
/// 
/// Author: William Min
/// </summary>
[CreateAssetMenu(fileName = "New Choice Skit", menuName = "Simulation Game Objects/Choice Skit", order = 1)]
public class ChoiceSkit : Skit
{
    #region Serialized Field

    [Header("Choice Skit Properties")]
    [SerializeField] private DialogueLine _questionLine;    // 
    [SerializeField] private ChoiceOption[] _choiceOptions; // 

    #endregion

    #region Private Fields

    private int _choiceIndex = -1;  //

    #endregion

    #region Static Fields

    /// <summary>
    /// 
    /// </summary>
    public static SpawnInParent<Button> CHOICE_SPAWNER;

    #endregion

    #region Skit Callbacks

    /// <summary>
    /// Returns a dialogue line based on index.
    /// </summary>
    /// <param name="index">Index of the dialogue line in skit.</param>
    public override DialogueLine GetLine(int index)
    {
        if (index == 0)
            return _questionLine;
        else if (_choiceIndex >= 0 && _choiceIndex < _choiceOptions.Length)
            return _choiceOptions[_choiceIndex].GetLine(index - 1);
        else
            return null;
    }

    /// <summary>
    /// Returns the number of lines in the skit.
    /// </summary>
    /// <returns>Number of lines in skit</returns>
    public override int GetLineCount()
    {
        if (_choiceIndex < 0 || _choiceIndex >= _choiceOptions.Length)
            return 1;
        else
            return _choiceOptions[_choiceIndex].GetLineCount();
    }

    /// <summary>
    /// Sets up the skit before playing the game.
    /// Call this first during game.
    /// </summary>
    public override void SetupSkit()
    {
        base.SetupSkit();
        foreach (ChoiceOption option in _choiceOptions)
            option.SetupChoice();

        if (CHOICE_SPAWNER == null)
            CHOICE_SPAWNER = FindObjectOfType<SpawnInParent<Button>>();
    }

    /// <summary>
    /// Processes the skit.
    /// </summary>
    public override void Process()
    {
        base.Process();

        _choiceIndex = -1;

        int choiceCount = _choiceOptions.Length;
        CHOICE_SPAWNER.SetSpawnedCount(choiceCount);
        for (int i = 0; i < choiceCount; i++)
        {
            Button choiceButton = CHOICE_SPAWNER.GetSpawned(i);
            _choiceOptions[i].ModifyButton(choiceButton);
            choiceButton.onClick.RemoveAllListeners();
            choiceButton.onClick.AddListener(() => ChooseChoice(i));
        }

        _questionLine.ProcessSpeakerNames();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="newIndex"></param>
    public void ChooseChoice(int newIndex)
    {
        if (newIndex >= 0 && newIndex < _choiceOptions.Length)
        {
            _choiceIndex = newIndex;
            _choiceOptions[newIndex].ProcessChoice();
        }
    }

    #endregion
}
