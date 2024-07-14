using UnityEngine;

public class FindSide : MonoBehaviour
{
    [SerializeField]
    private HitBox hitBox;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.GetComponent<PlayerMovement>())
        {
            hitBox.ChangeMultiply(false);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        hitBox.ChangeMultiply(true);
    }
}