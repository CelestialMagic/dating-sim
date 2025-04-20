using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 
/// 
/// Author: William Min
/// </summary>
public class CharacterSprite : MonoBehaviour
{
    #region Serialized Fields

    [Header("Character Sprite Properties")]
    [SerializeField] private Image _spriteImage; //
    [Space]
    [SerializeField] private ActOnChangeValue<bool> _isSubject; // 
    [SerializeField] private ActOnChangeValue<bool> _isRevealed; // 
    [SerializeField] private ActOnChangeValue<bool> _hasArrived; // 

    #endregion

    private LayoutGroup _group; // 

    #region Constants

    private static readonly float SUBJECT_TRANSITION_TIME = .2f; // 
    private static readonly float REVEALED_TRANSITION_TIME = .2f; // 
    private static readonly float ARRIVING_TRANSITION_TIME = .4f; // 
    private static readonly float INTRODUCED_TRANSITION_TIME = .15f; // 

    #endregion

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public bool IsSubject { get => _isSubject.Value; set => _isSubject.Value = value; }

    /// <summary>
    /// 
    /// </summary>
    public bool IsRevealed { get => _isRevealed.Value; set => _isRevealed.Value = value; }

    /// <summary>
    /// 
    /// </summary>
    public bool HasArrived { get => _hasArrived.Value; set => _hasArrived.Value = value; }

    #endregion

    #region Monobehavior Callbacks

    // 
    private void OnValidate()
    {
        if (!Application.isPlaying) return;

        SetUp();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Sets up the character sprite values, events, and references
    /// </summary>
    public void SetUp()
    {
        if (_group == null) _group = GetComponentInParent<HorizontalLayoutGroup>();

        if (_isSubject.ActionsOnChangedValue == null) _isSubject.ActionsOnChangedValue += b => StartCoroutine(_toggleTransition(b, SUBJECT_TRANSITION_TIME, t => _scaleInLayoutGroup(Vector3.one * Mathf.SmoothStep(1f, 1.2f, t), transform, _group)));
        if (_isRevealed.ActionsOnChangedValue == null) _isRevealed.ActionsOnChangedValue += b => StartCoroutine(_toggleTransition(b, REVEALED_TRANSITION_TIME, t => _changeColor(Color.Lerp(Color.black, Color.white, t), _spriteImage.color.a)));
        if (_hasArrived.ActionsOnChangedValue == null) _hasArrived.ActionsOnChangedValue += b => {
            if (b)
                StartCoroutine(_toggleTransition(b, INTRODUCED_TRANSITION_TIME, t => _scaleInLayoutGroup(Vector3.one * Mathf.SmoothStep(0f, 1f, t), transform, _group), _toggleTransition(b, ARRIVING_TRANSITION_TIME, t => _changeColor(_spriteImage.color, Mathf.SmoothStep(0, 1f, t)))));
            else
                StartCoroutine(_toggleTransition(b, ARRIVING_TRANSITION_TIME, t => _changeColor(_spriteImage.color, Mathf.SmoothStep(0, 1f, t)), _toggleTransition(b, INTRODUCED_TRANSITION_TIME, t => _scaleInLayoutGroup(Vector3.one * Mathf.SmoothStep(0f, 1f, t), transform, _group))));
        };

        _isSubject.ActOnValue();
        _isRevealed.ActOnValue();
        _hasArrived.ActOnValue();
    }

    #endregion

    #region Private Methods

    // Changes the color of the sprite based on a RGB color and an alpha value
    private void _changeColor(Color newColor, float newAlpha)
    {
        newColor.a = newAlpha;
        _spriteImage.color = newColor;
    }

    // Triggers a toggle transition animation 
    private IEnumerator _toggleTransition(bool turnOn, float transitionTime, Action<float> transitionProcess, IEnumerator nextCoroutine = null)
    {
        float t = turnOn ? 0 : 1;
        float rate = (turnOn ? 1 : -1) / transitionTime;

        while (t >= 0 && t <= 1)
        {
            transitionProcess(t);
            t += rate * Time.deltaTime;
            yield return null;
        }

        transitionProcess(t);

        if (nextCoroutine != null) StartCoroutine(nextCoroutine);
    }

    private void _scaleInLayoutGroup(Vector3 newScale, Transform target, LayoutGroup group)
    {
        target.localScale = newScale;
        group.CalculateLayoutInputVertical();
        group.CalculateLayoutInputHorizontal();
        group.SetLayoutVertical();
        group.SetLayoutHorizontal();
    }

    #endregion
}
