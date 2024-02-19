using NTC.Pool;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rigidbod;

    [SerializeField] 
    private float speed;

    [SerializeField]
    private float damage;

    private void Start()
    {
        rigidbod.velocity = transform.right * speed;
    }

    private void OnEnable()
    {
        rigidbod.velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out HealthControl enemy) && col.gameObject.TryGetComponent(out ParticleManagement particle))
        {
            particle.SetPositionAndRotation(DetectionContactPoint.Position(col), DetectionContactPoint.Rotation(rigidbod));
            enemy.GetDamage(damage);
        }

        NightPool.Despawn(gameObject);
    }
}