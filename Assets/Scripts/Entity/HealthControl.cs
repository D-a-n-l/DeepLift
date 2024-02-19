using UnityEngine;
using UnityEngine.Events;

public class HealthControl : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;

    public float MaxHealth => maxHealth;

    private float health;

    public float Health => health;

    [HideInInspector]
    public UnityEvent<bool> OnDead;

    [HideInInspector]
    public UnityEvent OnGetDamage;

    [HideInInspector]
    public UnityEvent<int> OnGetDamageInt;

    [HideInInspector]
    public UnityEvent OnGetHeal;

    private void Start()
    {
        health = maxHealth;
    }

    public void GetDamage(float damage)
    {
        health -= damage;

        OnGetDamage.Invoke();

        OnGetDamageInt.Invoke(1);

        if (health < 0)
        {
            health = 0;

            OnDead.Invoke(true);
        }
    }

    public void GetHeal(float heal)
    {
        health += heal;

        OnGetHeal.Invoke();

        if (health > maxHealth)
            health = maxHealth;
    }
}