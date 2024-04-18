using UnityEngine;

public class ChangeLayer : MonoBehaviour
{
    [SerializeField]
    private Collider2D innerBox;

    [SerializeField]
    private Collider2D externalBox;

    public PresetSpriteRenderer[] sprites;

    private PlayerMovement player;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (player == null && col.GetComponent<PlayerMovement>())
            player = col.GetComponent<PlayerMovement>();

        if (player != null && player.transform.position.y > transform.position.y)
            SetSortingLayerAndCollider(true, true, false);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (player != null && player.transform.position.y > transform.position.y)
            SetSortingLayerAndCollider(true, true, false);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (player != null)
            SetSortingLayerAndCollider(false, false, true);
    }

    private void SetSortingLayerAndCollider(bool newSortingOrder, bool inner, bool external)
    {
        if (newSortingOrder == true)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].spriteRenderer.sortingOrder = sprites[i].newSortingOrder;
            }
        }
        else
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].spriteRenderer.sortingOrder = sprites[i].defaultSortingOrder;
            }
        }

        if (innerBox != null)
            innerBox.enabled = inner;

        if (externalBox != null)
            externalBox.enabled = external;
    }
}

[System.Serializable]
public struct PresetSpriteRenderer
{
    public SpriteRenderer spriteRenderer;

    public int newSortingOrder;

    public int defaultSortingOrder;
}