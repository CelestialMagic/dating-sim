using System.Collections;
using UnityEngine;

/// <summary>
/// Handles the animations playing in an animator.
/// 
/// Author: Small Hedge Games, William Min
/// </summary>
public class AnimationHandler : MonoBehaviour
{
    #region Private Fields

    private Animator _currentAnimator; // 
    private int _currentLayer; // 
    private string _currentAnimation; // 

    #endregion

    #region Singleton Structure

    /// <summary>
    /// Reference to single instance of Animation Handler singleton.
    /// </summary>
    public static AnimationHandler Instance { get; private set; }

    // Initializes singleton and sets up variables
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    #endregion

    #region Public Methods

    public void Play(Animator animator, string animationName, int layer, float speed = 1f, float crossfade = .2f, float time = 0, float normalizedTimeOffset = 0f, float normalizedTransitionTime = 0f)
    {
        if (time > 0) StartCoroutine(Wait());
        else Validate();

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(time - crossfade);
            Validate();
        }

        void Validate()
        {
            if (animator == null || animationName == null || animator == _currentAnimator && layer == _currentLayer && animationName == _currentAnimation) return;

            animator.speed = speed;
            animator.Play(animationName, layer, normalizedTimeOffset);
            //animator.CrossFade(animationName, crossfade, layer, normalizedTimeOffset, normalizedTransitionTime);

            _currentAnimator = animator;
            _currentLayer = layer;
            _currentAnimation = animationName;
        }
    }

    #endregion
}
