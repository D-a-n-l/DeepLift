using UnityEngine;
using NaughtyAttributes;

public class AnimationManagement : MonoBehaviour
{
    [SerializeField]
    private Enums.TypeAnimationManagement typeAnimation;

    [HideIf(nameof(typeAnimation), Enums.TypeAnimationManagement.button)]
    [SerializeField]
    private PresetAnimator player;

    [HideIf(nameof(typeAnimation), Enums.TypeAnimationManagement.player)]
    [SerializeField]
    private PresetAnimator button;

    [Space(10)]
    [SerializeField]
    private PresetActions actions;

    private bool isReady = true;

    public bool IsReady => isReady;

    private bool ChangeIsReady(int value)
    {
        return isReady = value == 0 ? false : true;
    }

    private void Start()
    {
        if (actions.healthControl != null)
            actions.healthControl.OnGetDamage.AddListener(Play);

        if (actions.shooting != null)
            actions.shooting.OnShot.AddListener(Play);

        if (actions.sprint != null)
            actions.sprint.OnSprint.AddListener(Play);
    }

    public void Play()
    {
        ChangeIsReady(0);

        if (player.animator != null)
            player.animator.SetTrigger(player.nameTrigger);

        if (button.animator != null)
            button.animator.SetTrigger(button.nameTrigger);
    }
}

[System.Serializable]
public struct PresetAnimator
{
    public Animator animator;

    public string nameTrigger;
}

[System.Serializable]
public struct PresetActions
{
    public HealthControl healthControl;

    public Shooting shooting;

    public Sprint sprint;
}