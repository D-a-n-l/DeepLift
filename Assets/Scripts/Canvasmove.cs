using UnityEngine;

public class Canvasmove : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Canvas canvas;

    private Vector3 offset = Vector3.zero;

    private void Start()
    {
        offset = transform.position - worldToUISpace(canvas, player.transform.position);
    }
    private void Update()
    {
        transform.position = worldToUISpace(canvas, player.transform.position) + offset;
    }

    public Vector3 worldToUISpace(Canvas parentCanvas, Vector3 worldPos)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out movePos);

        return parentCanvas.transform.TransformPoint(movePos);
    }
}