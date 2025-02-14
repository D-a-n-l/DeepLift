using UnityEngine;

public class DetectionEntityForDeep : MonoBehaviour
{
    [SerializeField]
    private float offsetPlayer;

    [SerializeField]
    private float offsetEnemy;

    [Space(10)]
    [SerializeField]
    private Collider2D innerBox;

    [SerializeField]
    private Collider2D externalBox;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.TryGetComponent(out PlayerMovement player) && player.transform.position.y > transform.position.y + offsetPlayer) ||
            (col.TryGetComponent(out EnemyMovement enemy) && enemy.transform.position.y > transform.position.y + offsetEnemy))
            ActivateCollider(true, false);
        else
            ActivateCollider(false, true);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if ((col.TryGetComponent(out PlayerMovement player) && player.transform.position.y > transform.position.y + offsetPlayer) ||
            (col.TryGetComponent(out EnemyMovement enemy) && enemy.transform.position.y > transform.position.y + offsetEnemy))
            ActivateCollider(true, false);
        else
            ActivateCollider(false, true);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        ActivateCollider(false, true);
    }

    public void ActivateCollider(bool inner, bool external)
    {
        innerBox.enabled = inner;

        externalBox.enabled = external;
    }
}