using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] 
    private float speed;

    private Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy") || col.CompareTag("Shield")) { Destroy(gameObject); }
    }
}