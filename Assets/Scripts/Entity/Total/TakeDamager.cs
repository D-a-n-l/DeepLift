using UnityEngine;

public class TakeDamager : MonoBehaviour
{
    [SerializeField]
    private float damage = 10f;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out EnemyMovement enemy))
            enemy.GetComponent<HealthControl>().GetDamage(damage, false);
    }
}