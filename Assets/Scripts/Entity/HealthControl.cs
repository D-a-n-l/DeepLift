using NTC.Pool;
using UnityEngine;
using UnityEngine.Events;

public class HealthControl : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private bool despawnWhenDead = true;

    public float MaxHealth => maxHealth;

    private float health;

    public float Health => health;

    [HideInInspector]
    public UnityEvent OnDead;

    [HideInInspector]
    public UnityEvent OnGetDamage;

    [HideInInspector]
    public UnityEvent<int> OnGetDamageInt;

    [HideInInspector]
    public UnityEvent OnGetHeal;

    private void Awake()
    {
        health = maxHealth;
    }

    public void GetDamage(float damage)
    {
        health -= damage;

        OnGetDamage?.Invoke();

        OnGetDamageInt.Invoke(1);

        if (health <= 0f)
        {
            OnDead?.Invoke();

            health = 0;

            if (despawnWhenDead == true)
                NightPool.Despawn(gameObject);
        }
    }

    public void GetHeal(float heal)
    {
        health += heal;

        OnGetHeal?.Invoke();

        if (health > maxHealth)
            health = maxHealth;
    }
}