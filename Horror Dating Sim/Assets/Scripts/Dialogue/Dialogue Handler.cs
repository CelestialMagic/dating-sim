using System;
using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// Module that handles the dialogue, transitions of character animations and sprites, and the speakers in the scene.
/// 
/// Author: William Min
/// </summary>
public class DialogueHandler : MonoBehaviour
{
    #region Serialized Fields

    [Header("Dialogue Handler Properties")]
    [SerializeField] private DialogueScript _script; // Dialogue script container
    [SerializeField] private int _currentLineIndex = 0; // Index of current line in dialogue script
    [SerializeField] private float _timeBetweenCharacters = 0.1f; // Default time in seconds between typing characters

    [Header("Dialogue Handler References")]
    [SerializeField] private TMP_Text _dialogueText; // Reference to text box containing current dialogue line
    [SerializeField] private TMP_Text _nameText; // Reference to text box containing current speaker name
    [SerializeField] private Transform _spriteParent; // Parent transform to house all sprites

    #endregion

    #region Private Fields

    private Coroutine _currentTextCoroutine; // Current coroutine playing current line
    private bool _hasSkimmedLine = false; // True if playr has skimmed over line
    private bool _firstTime = true; // True if this is the first time player has interacted with dialogue

    // Modes of rich text
    private enum RichText
    {
        NOT_RICH_TEXT,
        MODIFYING_TEXT,
        STAND_ALONE_TEXT
    }

    #endregion

    #region Constants

    private static readonly string[] RICH_TEXT_WITHOUT_INDEX_SPACE = new string[] { "b", "i", "size", "color" }; // 
    private static readonly string[] RICH_TEXT_WITH_INDEX_SPACE = new string[] { "sprite" }; // 

    #endregion

    #region Properties

    /// <summary>
    /// Returns the current line the dialogue handler is on.
    /// </summary>
    public DialogueLine CurrentLine { get => _script == null ? null : _script.DialogueLines[CurrentLineIndex]; }

    /// <summary>
    /// Returns the current line the dialogue handler is on as a string.
    /// </summary>
    public string CurrentLineString { get => CurrentLine == null ? "[Dialogue Line]" : CurrentLine.Line; }
    
    /// <summary>
    /// Returns the number of lines in the script.
    /// </summary>
    public int LineCount { get => _script == null ? 0 : _script.DialogueLines.Length; }

    /// <summary>
    /// Index of current line in dialogue script.
    /// Will clamp the new value within the line count.
    /// Will return -1 if no script is available.
    /// </summary>
    public int CurrentLineIndex { get => _script == null ? -1 : _currentLineIndex; set { if (_script != null) _currentLineIndex = Mathf.Clamp(value, 0, LineCount); } }

    #endregion

    #region Monobehavior Callbacks

    // Sets up current index and starts line from there
    private void Start()
    {
        CurrentLineIndex = _currentLineIndex;
        if (_currentLineIndex == LineCount) CurrentLineIndex--;
        _hasSkimmedLine = true;

        // Processes all profiles in the script
        foreach (CharacterProfile profile in _script.Characters)
        {
            profile.Process();
            CharacterSpriteSpawner.SpawnSprite(profile, _spriteParent);
        }

        _script.Process(); // Processes the script itself
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
            _dialogueText.text = CurrentLineString;
            _hasSkimmedLine = true;
        }
        else
        {
            if (!_firstTime) CurrentLineIndex++;
            _firstTime = false;
            _startLine();
        }
    }

    #endregion

    #region Private Methods

    // Types the line, triggers effects, and emit audio in timely manner
    private IEnumerator _typeLine()
    {
        _dialogueText.text = ""; // Empties dialogue box

        for (int i = 0; i < CurrentLineString.Length; i++)
        {
            string nextText = ""; // Sets up text to next be displayed
            bool continueAdding = true;
  
            // Loop uses i as the starting index of the rich text modifier
            while (continueAdding && i < CurrentLineString.Length && CurrentLineString[i] == '<')
            {
                int endingIndex = CurrentLineString.IndexOf('>', i); // Find the ending index of the rich text

                if (endingIndex < 0) // If no brackets are found, break out of loop;
                    break;
                else
                {
                    string potentialRichText = CurrentLineString.Substring(i, endingIndex + 1 - i); // Potential rich text modifier
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
            if (continueAdding && i < CurrentLineString.Length)
            {
                // Process a whitespace character as a single character
                if (i < CurrentLineString.Length - 1 && CurrentLineString[i] == '\\')
                {
                    nextText += CurrentLineString.Substring(i, 2);
                    i++;
                }
                else
                    nextText += CurrentLineString[i]; // Inserts next character
            }

            if (!_hasSkimmedLine) _dialogueText.text += nextText; // Adds next text bunch to dialogue text box

            Debug.Log("Play Gibber Sound"); // Plays gibber sound
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
        _nameText.text = CurrentLine.SpeakerNamesDisplay;
        CurrentLine.ToggleCharacterSprites(CharacterSpriteSpawner.ActiveSprites);

        _currentTextCoroutine = StartCoroutine(_typeLine()); // Starts line typing
    }

    #endregion
}
