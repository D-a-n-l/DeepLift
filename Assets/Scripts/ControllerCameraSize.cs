using UnityEngine;
using Cinemachine;

public class ControllerCameraSize : MonoBehaviour
{
    [SerializeField] 
    private Vector2 defaultResolution = new(1920, 1080);

    [SerializeField, Range(0, 1)]
    private float widthOrHeight = .5f;

    private Camera mainCamera;

    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private float initialSize;

    private float targetAspect;

    private void Awake()
    {
        if (GetComponent<CinemachineVirtualCamera>())
            cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

        mainCamera = Camera.main;

        initialSize = mainCamera.orthographicSize;

        targetAspect = defaultResolution.x / defaultResolution.y;

        float constantWidthSize = initialSize * (targetAspect / mainCamera.aspect);

        if (cinemachineVirtualCamera == null)
            mainCamera.orthographicSize = Mathf.Lerp(constantWidthSize, initialSize, widthOrHeight);
        else
            cinemachineVirtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(constantWidthSize, initialSize, widthOrHeight);
    }
}