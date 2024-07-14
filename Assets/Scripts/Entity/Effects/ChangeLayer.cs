using UnityEngine;

public class ChangeLayer : MonoBehaviour
{
    [SerializeField]
    private float offsetPlayer;

    [SerializeField]
    private float offsetEnemy;

    [SerializeField]
    private Collider2D innerBox;

    [SerializeField]
    private Collider2D externalBox;

    [SerializeField]
    private PresetSpriteRenderer[] sprites;

    private bool isLift = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (isLift == true)
            return;

        if (col.GetComponent<GameManager>())
        {
            SetSortingLayerAndCollider(true, true, false);

            isLift = true;
        }
        else if ((col.TryGetComponent(out PlayerMovement player) && player.transform.position.y > transform.position.y + offsetPlayer) || 
            (col.TryGetComponent(out EnemyMovement enemy) && enemy.transform.position.y > transform.position.y + offsetEnemy))
            SetSortingLayerAndCollider(true, true, false);
        else
            SetSortingLayerAndCollider(false, false, true);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (isLift == true)
            return;

        if (col.GetComponent<GameManager>())
        {
            SetSortingLayerAndCollider(true, true, false);

            isLift = true;
        }
        else if ((col.TryGetComponent(out PlayerMovement player) && player.transform.position.y > transform.position.y + offsetPlayer) || 
            (col.TryGetComponent(out EnemyMovement enemy) && enemy.transform.position.y > transform.position.y + offsetEnemy))
            SetSortingLayerAndCollider(true, true, false);
        else
            SetSortingLayerAndCollider(false, false, true);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (isLift == true)
            return;

        SetSortingLayerAndCollider(false, false, true);
    }

    public void SetSortingLayerAndCollider(bool newSortingOrder, bool inner, bool external)
    {
        if (newSortingOrder == true)
        {
            for (int i = 0; i < sprites.Length; i++)
                sprites[i].spriteRenderer.sortingOrder = sprites[i].newSortingOrder;
        }
        else
        {
            for (int i = 0; i < sprites.Length; i++)
                sprites[i].spriteRenderer.sortingOrder = sprites[i].defaultSortingOrder;
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