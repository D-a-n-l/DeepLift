using UnityEngine;
using Unity.Cinemachine;

[RequireComponent (typeof(Animator))]
public class CanvasBlood : MonoBehaviour
{
    [SerializeField]
    private Canvas mainCanvas;

    [SerializeField]
    private HealthControl healthControl;

    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;

    private CinemachineBasicMultiChannelPerlin virtualCameraNoisePerlin;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        healthControl.OnGetDamageInt.AddListener(PlayAnimation);

        virtualCameraNoisePerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void PlayAnimation(int isEnable)
    {
        mainCanvas.enabled = isEnable == 0 ? false : true;

        if (isEnable == 1)
        {
            animator.SetTrigger("Go");

            StartCoroutine(ManagementVirtualCamera.Shake(virtualCameraNoisePerlin, 4f, 4f, .2f));
        }
    }
}