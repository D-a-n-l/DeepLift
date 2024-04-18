using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RandomAnimations : MonoBehaviour
{
    [SerializeField]
    private AnimatorOverrideController[] animatorOverrideControllers;

    public AnimatorOverrideController[] AnimatorOverrideControllers => animatorOverrideControllers;

    private Animator animator;

    public Animator Animator => animator;

    private void Start() => animator = GetComponent<Animator>();

    public void SetRandomTrigger(string name, int index)
    {
        animator.runtimeAnimatorController = animatorOverrideControllers[index];

        animator.SetTrigger(name);
    }
}