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

    [SerializeField] private BackgroundHandler _backgroundHandler; //The scene's background handler (Jessie)
    [SerializeField] private AudioSource _sfxAudioSource; //An audio source dedicated to SFX (Jessie)
    [SerializeField] private Skit[] _skits;                         // List of skits to play in scene
    [SerializeField] private int _currentLineIndex = 0;             // Index of current line in skit
    [SerializeField] private int _currentSkitIndex = 0;             // Index of current skit
    [SerializeField] private float _timeBetweenCharacters = 0.1f;   // Default time in seconds between typing characters

    [Header("Dialogue Handler Events")]
    [SerializeField] private UnityEvent _onSkipDialogue = new UnityEvent();     // 
    [SerializeField] private UnityEvent _onBeginLine = new UnityEvent();        // 
    [SerializeField] private UnityEvent _onEndLine = new UnityEvent();          // 
    [SerializeField] private UnityEvent _onBeginSkit = new UnityEvent();        //
    [SerializeField] private UnityEvent _onEndSkit = new UnityEvent();          // 
    [SerializeField] private UnityEvent _onFinishAllSkits = new UnityEvent();   //

    #endregion

    #region Private Fields

    private Skit _currentSkit { get => _skits[_currentSkitIndex]; } // 
    private DialogueLine _currentLine { get => _currentSkit.GetLine(_currentLineIndex); } // 
    private string _currentLineString { get => _currentLine.Line; } //
    private int _skitCount { get => _skits.Length; } // 
    private int _currentLineCount { get => _currentSkit.GetLineCount(); } // 

    private Coroutine _currentTextCoroutine;    // Current coroutine playing current line
    private bool _hasSkimmedLine = false;       // True if player has skimmed over line
    private bool _firstTime;                    // True if this is the first time player has interacted with dialogue

    private Action<string> _onChangeDialogue;   // 
    private Action<string> _onChangeName;       //

    // Modes of rich text
    private enum RichText
    {
        NOT_RICH_TEXT,
        MODIFYING_TEXT,
        STAND_ALONE_TEXT
    }

    #endregion

    public int GetCurrentIndex()
    {
        return _currentLineIndex;
    }

    #region Constants

    private static readonly string[] RICH_TEXT_WITHOUT_INDEX_SPACE = new string[] { "b", "i", "size", "color" };    // Rich text types that are not considered as individual characters
    private static readonly string[] RICH_TEXT_WITH_INDEX_SPACE = new string[] { "sprite" };                        // Rich text types that are considered as individual characters

    #endregion

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public UnityEvent OnSkipDialogue { get => _onSkipDialogue; }

    /// <summary>
    /// 
    /// </summary>
    public UnityEvent OnBeginLine { get => _onBeginLine; }

    /// <summary>
    /// 
    /// </summary>
    public UnityEvent OnEndLine { get => _onEndLine; }

    /// <summary>
    /// 
    /// </summary>
    public UnityEvent OnBeginSkit { get => _onBeginSkit; }

    /// <summary>
    /// 
    /// </summary>
    public UnityEvent OnEndSkit { get => _onEndSkit; }

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
        foreach (Skit skit in _skits)
            skit.SetupSkit();

        _firstTime = true;
        _hasSkimmedLine = true;

        _currentSkit.Process();
        _onBeginSkit?.Invoke();

        //_onChangeDialogue += s => Debug.Log("Played Gibber Sound Before Skip.");

        _onEndLine.AddListener(() => Debug.Log("Ended Line"));
        _onBeginLine.AddListener(() => Debug.Log("Started Line"));
        _onEndSkit.AddListener(() => Debug.Log("Ended Skit"));
        _onFinishAllSkits.AddListener(() => Debug.Log("Finished All Skits"));
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
            _onEndLine?.Invoke();
        }
        else
        {
            // Check if handler has finished all skits
            if (_currentSkitIndex >= _skitCount)
                _onFinishAllSkits?.Invoke();
            else
            {
                // Decides if this call will increment or not
                if (_firstTime)
                    _firstTime = false;
                else
                    _currentLineIndex++;

                // Check if handler is at last line of current skit
                if (_currentLineIndex >= _currentLineCount)
                {
                    _onEndSkit?.Invoke();

                    _currentSkitIndex++;

                    // Check if handler has finished all skits
                    if (_currentSkitIndex >= _skitCount)
                        _onFinishAllSkits?.Invoke();

                    // Provess new skit, reset variables, and play line
                    else
                    {
                        _currentSkit.Process();
                        _currentLineIndex = 0;
                        _onBeginSkit?.Invoke();
                        _startLine();
                    }
                }
                else
                    _startLine();
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

        // Initialization of loop and storage variables
        int i = 0;
        int lineLength = _currentLineString.Length;
        string nextText = "";

        void _addNormalCharacter()
        {
            nextText += _currentLineString[i]; // Inserts next character
            i++;
        }

        while (i < lineLength)
        {
            nextText = ""; // Sets up text to next be displayed

            // Adds character if current character is not potential beginning of rich text
            if (_currentLineString[i] != '<')
                _addNormalCharacter();

            // Checks for rich text
            else
            {
                int endingIndex = _currentLineString.IndexOf('>', i);

                // Adds character if rich text is not complete
                if (endingIndex < 0)
                    _addNormalCharacter();

                // Checks for valid rich text
                else
                {
                    RichText textType = RichText.NOT_RICH_TEXT; // Records type of text analyzed

                    while (i < lineLength && endingIndex >= 0 && _currentLineString[i] == '<')
                    {
                        string potentialRichText = _currentLineString.Substring(i, endingIndex + 1 - i); // Potential rich text modifier
                        string richTextContents = potentialRichText.Substring(1, potentialRichText.Length - 2); // Contents of potential modifier

                        // Finds out what type of text is being read
                        textType = RichText.NOT_RICH_TEXT;

                        bool _analyzeTextType(string textContents)
                        {
                            if (Array.IndexOf(RICH_TEXT_WITHOUT_INDEX_SPACE, textContents) >= 0)
                            {
                                textType = RichText.MODIFYING_TEXT;
                                return true;
                            }
                            else if (Array.IndexOf(RICH_TEXT_WITH_INDEX_SPACE, textContents) >= 0)
                            {
                                textType = RichText.STAND_ALONE_TEXT;
                                return true;
                            }
                            else
                                return false;
                        }

                        if (!_analyzeTextType(richTextContents) && !_analyzeTextType(richTextContents.Substring(1)))
                        {
                            int equalIndex = richTextContents.IndexOf('=');
                            _analyzeTextType(richTextContents.Substring(0, equalIndex >= 0 ? equalIndex : 0).Trim());
                        }

                        // Adds to nextText based on text type
                        if (textType == RichText.STAND_ALONE_TEXT)
                        {
                            nextText += potentialRichText;
                            i = endingIndex + 1;
                            endingIndex = -1;
                        }
                        else if (textType == RichText.MODIFYING_TEXT)
                        {
                            nextText += potentialRichText;
                            i = endingIndex + 1;
                            endingIndex = i >= lineLength ? -1 : _currentLineString.IndexOf('>', i);
                        }
                        else if (textType == RichText.NOT_RICH_TEXT)
                            _addNormalCharacter();
                    }

                    // Checks if previous text type was a modifier
                    if (textType == RichText.MODIFYING_TEXT && i < lineLength)
                        _addNormalCharacter();
                }
            }

            currentLineText += nextText;
            _onChangeDialogue(currentLineText);

            yield return new WaitForSeconds(_timeBetweenCharacters); // Creates delay
        }

        _hasSkimmedLine = true; // Automatically marks the line skimmed when the line finishes typing
        _onEndLine?.Invoke();
    }

    // Starts the current line
    private void _startLine()
    {
        // Resets Variables
        if (_currentTextCoroutine != null) StopCoroutine(_currentTextCoroutine);
        _currentTextCoroutine = null;
        _hasSkimmedLine = false;

        // Sets up dialogue box display
        _onChangeName(_currentLine.SpeakerNamesDisplay);
        _currentLine.ToggleCharacterSprites(Skit.SPRITE_SPAWNER.GetAllSpawned());

//Possible logic for loading a background into handler (Jessie)

        if (_currentLine.GetHasBackground())
        {
            _backgroundHandler.SetSelectedBackdrop(_currentLine.GetBackground());
            _backgroundHandler.SetCanFadeAway(true);
        }
 //Possible logic for loading a SFX for certain lines (Jessie)
        if (_currentLine.GetHasSFX())
        {
            _sfxAudioSource.PlayOneShot(_currentLine.GetSoundEffect());
        }


        _currentTextCoroutine = StartCoroutine(_typeLine()); // Starts line typing
        _onBeginLine?.Invoke();
    }

    #endregion
}
