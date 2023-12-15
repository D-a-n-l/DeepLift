using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Стреляем")]
    [SerializeField] private Transform shotPosition;
    [SerializeField] private GameObject bullet;

    [SerializeField] private float reload;

    [Header("Хыпэ")]
    [SerializeField] private float health;

    [Header("Нанесение урона")]
    [SerializeField] private float minusHealthPistol;
    [SerializeField] private float minusHealthAk;

    [Header("Партиклы")]
    [SerializeField] private GameObject effectBlood;
    [SerializeField] private GameObject effectSparks;
    [SerializeField] private GameObject effectDie;

    [Header("Настройки противника")]
    [SerializeField, Tooltip("Стреляет/Не стреляет")] private bool loop;

    [SerializeField, Tooltip("Щит/Не щит")] private bool isShield;

    [HideInInspector] public Vector2 movement;

    private Rigidbody2D rb;

    private float moveSpeed = 10f;

    private IEnumerator Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (loop)
        {
            do
            {
                yield return StartCoroutine(ShootEnemy());
            }
            while (loop);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("BulletPistol"))
        {
            DamageApplication(isShield, minusHealthPistol);
        }

        if (col.CompareTag("BulletAk"))
        {
            DamageApplication(isShield, minusHealthAk);
        }

        if (col.CompareTag("Player"))
        {
            Destroy(gameObject);
            Instantiate(effectDie, transform.position, Quaternion.identity);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
            Instantiate(effectDie, transform.position, Quaternion.identity);
        }
    }

    private void DamageApplication(bool condition, float minusHealth)
    {
        if (!condition)
        {
            health -= minusHealth;

            Instantiate(effectBlood, transform.position, Quaternion.identity);
        }

        if (condition)
        {
            health -= minusHealth;

            if (movement.x > 0)
                Instantiate(effectSparks, transform.GetChild(0).position, Quaternion.Euler(0, 90, 0));
            else if (movement.x < 0)
                Instantiate(effectSparks, transform.GetChild(0).position, Quaternion.Euler(0, -90, 0));
        }
    }

    private IEnumerator ShootEnemy()
    {
        yield return new WaitForSeconds(reload);
        Instantiate(bullet, shotPosition.position, transform.rotation);
    }
}