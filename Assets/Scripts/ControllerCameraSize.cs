using UnityEngine;
using Cinemachine;

public class ControllerCameraSize : MonoBehaviour
{
    [SerializeField] private Vector2 DefaultResolution;
    [SerializeField , Range(0f, 1f)] private float WidthOrHeight = 0;

    private CinemachineVirtualCamera componentCamera;

    private float initialSize;
    private float targetAspect;

    private void Start()
    {
        componentCamera = GetComponent<CinemachineVirtualCamera>();
        initialSize = componentCamera.m_Lens.OrthographicSize;

        targetAspect = DefaultResolution.x / DefaultResolution.y;
    }

    private void Update()
    {
        float constantWidthSize = initialSize * (targetAspect / componentCamera.m_Lens.Aspect);
        componentCamera.m_Lens.OrthographicSize = Mathf.Lerp(constantWidthSize, initialSize, WidthOrHeight);
    }
}