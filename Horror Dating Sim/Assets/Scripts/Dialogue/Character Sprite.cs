using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;

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

    [Header("Character Sprite References")]
    [SerializeField] private CharacterProfile _profile; // 

    #endregion

    #region Private Fields

    private float _currentScale = 1; // 
    private ObjectPool<CharacterSprite> _pool;

    #endregion

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
    private void Start()
    {
        _isSubject.ActionsOnChangedValue += b => StartCoroutine(_toggleTransition(b, SUBJECT_TRANSITION_TIME, t => { _scaleInLayoutGroup(Vector3.one * Mathf.SmoothStep(1f, 1.2f, t)); _currentScale = transform.localScale.x; }));
        _isRevealed.ActionsOnChangedValue += b => StartCoroutine(_toggleTransition(b, REVEALED_TRANSITION_TIME, t => _changeColor(Color.Lerp(Color.black, Color.white, t), _spriteImage.color.a)));
        _hasArrived.ActionsOnChangedValue += b => StartCoroutine(b ? _toggleTransition(b, INTRODUCED_TRANSITION_TIME, t => _scaleInLayoutGroup(Vector3.one * Mathf.SmoothStep(0f, _currentScale, t)), _toggleTransition(b, ARRIVING_TRANSITION_TIME, t => _changeColor(_spriteImage.color, Mathf.SmoothStep(0, 1f, t)))) : 
                                                                     _toggleTransition(b, ARRIVING_TRANSITION_TIME, t => _changeColor(_spriteImage.color, Mathf.SmoothStep(0, 1f, t)), _toggleTransition(b, INTRODUCED_TRANSITION_TIME, t => _scaleInLayoutGroup(Vector3.one * Mathf.SmoothStep(0f, _currentScale, t)))));
    }

    // 
    private void OnValidate()
    {
        if (!Application.isPlaying) return;

        _isSubject.ActOnValue();
        _isRevealed.ActOnValue();
        _hasArrived.ActOnValue();
    }

    // Sets sprite toggles
    private void OnEnable()
    {
        IsSubject = false;
        IsRevealed = false;
        HasArrived = false;
    }

    #endregion

    #region Public Methods

    public void SetPool(ObjectPool<CharacterSprite> pool)
    {
        _pool = pool;
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

    // Scales the character sprite and updates the display in the layout group
    private void _scaleInLayoutGroup(Vector3 newScale)
    {
        transform.localScale = newScale;

        LayoutGroup group = transform.GetComponentInParent<LayoutGroup>();
        if (group != null)
        {
            group.CalculateLayoutInputVertical();
            group.CalculateLayoutInputHorizontal();
            group.SetLayoutVertical();
            group.SetLayoutHorizontal();
        }
    }

    #endregion
}

/// <summary>
/// 
/// 
/// Author: William Min
/// </summary>
[Serializable]
public struct CharacterSpriteSettingsSheet
{
    #region Public Fields

    /// <summary>
    /// 
    /// </summary>
    public bool IsSubject;

    /// <summary>
    /// 
    /// </summary>
    public bool IsRevealed;

    /// <summary>
    /// 
    /// </summary>
    public bool HasArrived;

    #endregion

    #region Public Methods

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sprite"></param>
    public void ToggleCharacterSprite(CharacterSprite sprite)
    {
        sprite.HasArrived = HasArrived;
        sprite.IsRevealed = IsRevealed;
        sprite.IsSubject = IsSubject;
    }

    #endregion
}
