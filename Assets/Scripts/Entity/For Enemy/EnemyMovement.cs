using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Rigidbody2D rigidbody;

    private Vector3 newRotation = new(0f, 180f, 0f);

    private Vector3 newLocalScale = new(1, 1, -1);

    private Vector2 direction;

    private bool isFirst = true; 

    private void Start()
    {
        Rotate();

        isFirst = false;
    }

    private void OnEnable()
    {
        if (isFirst == true)
            return;

        Rotate();
    }

    public void Rotate()
    {
        direction = Spawner.instance.Direction;

        if (direction == Vector2.left && transform.rotation.eulerAngles.y == 0)
        {
            transform.Rotate(newRotation);
            
            transform.localScale = newLocalScale;
        }

        rigidbody.velocity = direction * speed;
    }
}