using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] float damage;

    private Rigidbody2D rb;

    private JohnWickController john;
    private void Start()
    {
        john = FindObjectOfType<JohnWickController>();

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) { john.health -= damage; Destroy(gameObject); }
    }
}