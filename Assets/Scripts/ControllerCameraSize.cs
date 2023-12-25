using UnityEngine;
using Cinemachine;

public class ControllerCameraSize : MonoBehaviour
{
    [SerializeField] 
    private Vector2 defaultResolution = new(1920, 1080);

    [SerializeField, Range(0, 1)]
    private float widthOrHeight = .5f;

    private CinemachineVirtualCamera componentCamera;

    private float initialSize;
    private float targetAspect;

    private void Start()
    {
        componentCamera = GetComponent<CinemachineVirtualCamera>();
        initialSize = componentCamera.m_Lens.OrthographicSize;

        targetAspect = defaultResolution.x / defaultResolution.y;

        float constantWidthSize = initialSize * (targetAspect / componentCamera.m_Lens.Aspect);
        componentCamera.m_Lens.OrthographicSize = Mathf.Lerp(constantWidthSize, initialSize, widthOrHeight);
    }
}