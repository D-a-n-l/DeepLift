using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D rigidbody;

    private Vector2 direction;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        direction = Spawner.instance.Direction;

        if (direction == Vector2.right)
        {
            transform.Rotate(0f, 180f, 0f);
        }

        rigidbody.velocity = direction * speed;
    }
}