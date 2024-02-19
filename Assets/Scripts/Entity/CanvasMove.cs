using UnityEngine;

public class CanvasMove : MonoBehaviour
{
    [SerializeField] 
    private Transform player;

    private Canvas canvas;

    private Camera mainCamera;

    private Vector3 offset => transform.position - worldToUISpace(player.position);

    private void Start()
    {
        canvas = GetComponent<Canvas>();

        mainCamera = canvas.worldCamera;
    }

    private void Update()
    {
        transform.position = worldToUISpace(player.position) + offset;
    }

    public Vector3 worldToUISpace(Vector3 worldPos)
    {
        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldPos);

        Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPos, canvas.worldCamera, out movePos);

        return canvas.transform.TransformPoint(movePos);
    }
}