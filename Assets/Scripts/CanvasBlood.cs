using UnityEngine;

[RequireComponent (typeof(Animator))]
public class CanvasBlood : MonoBehaviour
{
    [SerializeField]
    private Canvas mainCanvas;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation(int isEnable)
    {
        mainCanvas.enabled = isEnable == 0 ? false : true;

        if (isEnable == 1)
            animator.SetTrigger("Go");
    }
}