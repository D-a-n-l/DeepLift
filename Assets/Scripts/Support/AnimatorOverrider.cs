using UnityEngine;

public class AnimatorOverrider : MonoBehaviour
{
    public Animator animator;

    private void Start() => animator = GetComponent<Animator>();

    public void SetNew(AnimatorOverrideController controller) => animator.runtimeAnimatorController = controller;
}