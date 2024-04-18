using UnityEngine;

public class CheckLift : MonoBehaviour
{
    [SerializeField]
    private ChangeLayer changeLayer;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<GameManager>())
        {
            for (int i = 0; i < changeLayer.sprites.Length; i++)
            {
                changeLayer.sprites[i].defaultSortingOrder += 2;
            }
        }
    }
}