using UnityEngine;

public class EnemyBehind : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject effectDie;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("BulletPistol") || col.CompareTag("BulletAk"))
        {
            Destroy(enemy);
            Instantiate(effectDie, transform.position, Quaternion.identity);
        }
    }
}