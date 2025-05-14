using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Module that handles the dialogue, transitions of character animations and sprites, and the speakers in the scene.
/// 
/// Author: William Min
/// </summary>
public class DialogueHandler : MonoBehaviour
{
    #region Serialized Fields

    [Header("Dialogue Handler Properties")]
    [SerializeField] private DialogueSkit _script;                  // Dialogue script container
    [SerializeField] private int _currentLineIndex = 0;             // Index of current line in dialogue script
    [SerializeField] private float _timeBetweenCharacters = 0.1f;   // Default time in seconds between typing characters

    [Space]
    [SerializeField] private UnityEvent _onSkipDialogue = new UnityEvent();     // 
    [SerializeField] private UnityEvent _onMoveToNextLine = new UnityEvent();   // 
    [SerializeField] private UnityEvent _onProcessScript = new UnityEvent();    // 
    [SerializeField] private UnityEvent _onFinishScript = new UnityEvent();     // 

    #endregion

    #region Private Fields

    private Coroutine _currentTextCoroutine; // Current coroutine playing current line
    private bool _hasSkimmedLine = false;   // True if playr has skimmed over line
    private bool _firstTime = true;         // True if this is the first time player has interacted with dialogue

    private Action<string> _onChangeDialogue;   // 
    private Action<string> _onChangeName;       //
    private Action<int> _onChangeSpriteCount;   //
    private Action<int> _onChangeChoiceCount;   //

    // Modes of rich text
    private enum RichText
    {
        NOT_RICH_TEXT,
        MODIFYING_TEXT,
        STAND_ALONE_TEXT
    }

    private DialogueLine _currentLine { get => _script == null || CurrentLineIndex < 0 || CurrentLineIndex >= _script.DialogueLines.Length ? null : _script.DialogueLines[CurrentLineIndex]; } // Returns the current line the dialogue handler is on.
    private string _currentLineString { get => _currentLine == null ? "[Dialogue Line]" : _currentLine.Line; }  // Returns the current line the dialogue handler is on as a string.
    private int _lineCount { get => _script == null ? 0 : _script.DialogueLines.Length; }                       // Returns the number of lines in the script.

    // Index of current line in dialogue script.
    // Will clamp the new value within the line count.
    // Will return -1 if no script is available.
    private int CurrentLineIndex { get => _script == null ? -1 : _currentLineIndex; set { if (_script != null) _currentLineIndex = Mathf.Clamp(value, 0, _lineCount); } }

    #endregion

    #region Constants

    private static readonly string[] RICH_TEXT_WITHOUT_INDEX_SPACE = new string[] { "b", "i", "size", "color" }; // Rich text types that are not considered as individual characters
    private static readonly string[] RICH_TEXT_WITH_INDEX_SPACE = new string[] { "sprite" }; // Rich text types that are considered as individual characters

    #endregion

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public UnityEvent OnSkipDialogue { get => _onSkipDialogue; }

    /// <summary>
    /// 
    /// </summary>
    public UnityEvent OnMoveToNextLine { get => _onMoveToNextLine; }

    /// <summary>
    /// 
    /// </summary>
    public UnityEvent OnProcessScript { get => _onProcessScript; }

    /// <summary>
    /// 
    /// </summary>
    public UnityEvent OnFinishScript { get => _onFinishScript; }

    /// <summary>
    /// 
    /// </summary>
    public Action<string> OnChangeDialogue { get => _onChangeDialogue; set => _onChangeDialogue = value; }

    /// <summary>
    /// 
    /// </summary>
    public Action<string> OnChangeName { get => _onChangeName; set => _onChangeName = value; }

    #endregion

    #region Monobehavior Callbacks

    // Sets up current index and starts line from there
    private void Start()
    {
        CurrentLineIndex = _currentLineIndex;
        if (_currentLineIndex == _lineCount) CurrentLineIndex--;
        _hasSkimmedLine = true;

        _onProcessScript.AddListener(() => _script.Process());
        _onProcessScript?.Invoke();

        _onMoveToNextLine.AddListener(() => Debug.Log("Moved to Next Line"));
        _onFinishScript.AddListener(() => Debug.Log("Finished Script"));
        _onChangeDialogue += s => Debug.Log("Played Gibber Sound Before Skip.");
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Processes the skimmed line boolean and starts the next line.
    /// </summary>
    public void ProceedWithText()
    {
        if (!_hasSkimmedLine)
        {
            if (_currentTextCoroutine != null) StopCoroutine(_currentTextCoroutine);
            _onChangeDialogue(_currentLineString);
            _hasSkimmedLine = true;
            _onSkipDialogue?.Invoke();
        }
        else
        {
            // Decides if this call will increment or not
            if (_firstTime)
                _firstTime = false;
            else
                CurrentLineIndex++;

            // Either starts a new line or noves to new script
            if (CurrentLineIndex == _lineCount)
                _onFinishScript?.Invoke();
            else
            {
                _startLine();
                _onMoveToNextLine?.Invoke();
            }
        }
    }

    #endregion

    #region Private Methods

    // Types the line, triggers effects, and emit audio in timely manner
    private IEnumerator _typeLine()
    {
        string currentLineText = "";

        _onChangeDialogue(currentLineText);

        for (int i = 0; i < _currentLineString.Length; i++)
        {
            string nextText = ""; // Sets up text to next be displayed
            bool continueAdding = true;
  
            // Loop uses i as the starting index of the rich text modifier
            while (continueAdding && i < _currentLineString.Length && _currentLineString[i] == '<')
            {
                int endingIndex = _currentLineString.IndexOf('>', i); // Find the ending index of the rich text

                if (endingIndex < 0) // If no brackets are found, break out of loop;
                    break;
                else
                {
                    string potentialRichText = _currentLineString.Substring(i, endingIndex + 1 - i); // Potential rich text modifier
                    string richTextContents = potentialRichText.Substring(1, potentialRichText.Length - 2); // Contents of potential modifier

                    // Checks what type of rich text it is or if it isn't rich text at all
                    RichText textType = RichText.NOT_RICH_TEXT;
                    if (Array.IndexOf(RICH_TEXT_WITHOUT_INDEX_SPACE, richTextContents) >= 0)
                        textType = RichText.MODIFYING_TEXT;
                    else if (Array.IndexOf(RICH_TEXT_WITH_INDEX_SPACE, richTextContents) >= 0)
                        textType = RichText.STAND_ALONE_TEXT;
                    else
                    {
                        string endTest = richTextContents.Substring(1);

                        if (Array.IndexOf(RICH_TEXT_WITHOUT_INDEX_SPACE, endTest) >= 0)
                            textType = RichText.MODIFYING_TEXT;
                        else if (Array.IndexOf(RICH_TEXT_WITH_INDEX_SPACE, endTest) >= 0)
                            textType = RichText.STAND_ALONE_TEXT;
                        else
                        {
                            int equalIndex = richTextContents.IndexOf('=');

                            string startTest = richTextContents.Substring(0, equalIndex >= 0 ? equalIndex : 0).Trim();

                            if (Array.IndexOf(RICH_TEXT_WITHOUT_INDEX_SPACE, startTest) >= 0)
                                textType = RichText.MODIFYING_TEXT;
                            else if (Array.IndexOf(RICH_TEXT_WITH_INDEX_SPACE, startTest) >= 0)
                                textType = RichText.STAND_ALONE_TEXT;
                        }
                    }

                    if (textType == RichText.STAND_ALONE_TEXT)
                    {
                        nextText += potentialRichText;
                        i = endingIndex;
                        continueAdding = false;
                    }
                    else if (textType == RichText.MODIFYING_TEXT)
                    {
                        nextText += potentialRichText;
                        i = endingIndex + 1;
                    }
                    else if (textType == RichText.NOT_RICH_TEXT)
                    {
                        break;
                    }
                }
            }

            // Will add a character if previous loop permits it
            if (continueAdding && i < _currentLineString.Length)
            {
                // Process a whitespace character as a single character
                if (i < _currentLineString.Length - 1 && _currentLineString[i] == '\\')
                {
                    nextText += _currentLineString.Substring(i, 2);
                    i++;
                }
                else
                    nextText += _currentLineString[i]; // Inserts next character
            }

            currentLineText += nextText;
            _onChangeDialogue(currentLineText);

            yield return new WaitForSeconds(_timeBetweenCharacters); // Creates delay
        }

        _hasSkimmedLine = true; // Autmatically marks the line skimmed when the line finishes typing
    }

    // Starts the current line
    private void _startLine()
    {
        if (CurrentLineIndex >= _script.DialogueLines.Length) return;

        // Resets Variables
        if (_currentTextCoroutine != null) StopCoroutine(_currentTextCoroutine);
        _currentTextCoroutine = null;
        _hasSkimmedLine = false;

        // Sets up dialogue box display
        _onChangeName(_currentLine.SpeakerNamesDisplay);
        _currentLine.ToggleCharacterSprites(CharacterSpriteSpawner.Instance.SpawnedObjects);

        _currentTextCoroutine = StartCoroutine(_typeLine()); // Starts line typing
    }

    #endregion
}
