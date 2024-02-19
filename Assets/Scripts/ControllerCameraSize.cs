using UnityEngine;
using Cinemachine;

public class ControllerCameraSize : MonoBehaviour
{
    [SerializeField]
    private Enums.TypeCamera typeCamera;

    [SerializeField] 
    private Vector2 defaultResolution = new(1920, 1080);

    [SerializeField, Range(0, 1)]
    private float widthOrHeight = .5f;

    private Camera mainCamera;

    private CinemachineVirtualCamera cinemachineVirtualCamera;

    private float initialSize;

    private float targetAspect;

    private float aspectRatio;

    private void Awake()
    {
        if (typeCamera == Enums.TypeCamera.Camera)
        {
            mainCamera = GetComponent<Camera>();

            SetSizeAndAspect(mainCamera.orthographicSize, mainCamera.aspect);
        }
        else
        {
            cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

            SetSizeAndAspect(cinemachineVirtualCamera.m_Lens.OrthographicSize, cinemachineVirtualCamera.m_Lens.Aspect);
        }

        targetAspect = defaultResolution.x / defaultResolution.y;

        float constantWidthSize = initialSize * (targetAspect / aspectRatio);

        if (typeCamera == Enums.TypeCamera.Camera)
            mainCamera.orthographicSize = Mathf.Lerp(constantWidthSize, initialSize, widthOrHeight);
        else
            cinemachineVirtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(constantWidthSize, initialSize, widthOrHeight);
    }

    private void SetSizeAndAspect(float size, float aspect)
    {
        initialSize = size;

        aspectRatio = aspect;
    }
}