using UnityEngine;
using UnityEngine.UI;

public class DialogueSceneAnimator : MonoBehaviour
{
    [Header("Scene Animators")]
    [SerializeField] private Animator _sceneAnimator;
    [SerializeField] private Animator _dialogueAnimator;

    [Header("Background References")]
    [SerializeField] private Image _transitionImage;
    [SerializeField] private Image _backgroundImage;

    private void Start()
    {
        AnimationHandler.Instance.Play(_dialogueAnimator, "Open Dialogue", 0);
        AnimationHandler.Instance.Play(_dialogueAnimator, "Backdrop Transition", 0);
    }


}
