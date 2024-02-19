using UnityEngine;

public class ChangeLayer : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D innerBox;

    [SerializeField]
    private EdgeCollider2D externalBox;

    [SerializeField]
    private int newSortingOrder;

    private SpriteRenderer spriteRenderer;

    private PlayerMovement player;

    private int defaultSortingOrder;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        defaultSortingOrder = spriteRenderer.sortingOrder;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (player == null && col.GetComponent<PlayerMovement>())
            player = col.GetComponent<PlayerMovement>();

        if (player != null && player.transform.position.y > transform.position.y)
            SetSortingLayerAndCollider(newSortingOrder, true, false);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (player != null && player.transform.position.y > transform.position.y)
            SetSortingLayerAndCollider(newSortingOrder, true, false);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (player != null)
            SetSortingLayerAndCollider(defaultSortingOrder, false, true);
    }

    private void SetSortingLayerAndCollider(int sortingOrder, bool inner, bool external)
    {
        spriteRenderer.sortingOrder = sortingOrder;

        innerBox.enabled = inner;
        externalBox.enabled = external;
    }
}