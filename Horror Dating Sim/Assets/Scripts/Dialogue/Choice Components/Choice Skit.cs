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
    [Space]
    [SerializeField] private ChoiceOption[] _choiceOptions; // 

    #endregion

    #region Private Fields

    private ChoiceOption _selectedOption;

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
        //return index == 0 ? _questionLine : _selectedOption == null ? null : _selectedOption.GetLine(index - 1);
        if (index == 0)
            return _questionLine;
        else if (_selectedOption == null)
            return null;
        else
            return _selectedOption.GetLine(index - 1);
    }

    /// <summary>
    /// Returns the number of lines in the skit.
    /// </summary>
    /// <returns>Number of lines in skit</returns>
    public override int GetLineCount()
    {
        return 1 + (_selectedOption == null ? 0 : _selectedOption.GetLineCount());
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

        _selectedOption = null;

        int choiceCount = _choiceOptions.Length;
        CHOICE_SPAWNER.SetSpawnedCount(choiceCount);
        for (int i = 0; i < choiceCount; i++)
        {
            Button choiceButton = CHOICE_SPAWNER.GetSpawned(i);
            _choiceOptions[i].ModifyButton(choiceButton);
            choiceButton.onClick.RemoveAllListeners();

            int buttonIndex = i;
            choiceButton.onClick.AddListener(() => _selectedOption = _choiceOptions[buttonIndex]);
            choiceButton.onClick.AddListener(() => _selectedOption.ProcessChoice());
            choiceButton.onClick.AddListener(() => DIALOGUE_DISPLAY.ToggleChoiceHolder(false));
            choiceButton.onClick.AddListener(() => DIALOGUE_DISPLAY.ToggleProceedHolder(true));
            choiceButton.onClick.AddListener(() => DIALOGUE_HANDLER.ProceedWithText());
        }

        DIALOGUE_HANDLER.OnEndLine.AddListener(() => DIALOGUE_DISPLAY.ToggleProceedHolder(false));
        DIALOGUE_HANDLER.OnEndLine.AddListener(() => DIALOGUE_DISPLAY.ToggleChoiceHolder(true));

        _questionLine.ProcessSpeakerNames();
    }

    #endregion
}
