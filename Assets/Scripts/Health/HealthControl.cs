using NTC.Pool;
using UnityEngine;
using UnityEngine.Events;

public class HealthControl : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private bool isHealWhenDead = true;

    [SerializeField]
    private bool isDespawnWhenDead = true;

    public float MaxHealth => maxHealth;

    private double health;

    public double Health => health;

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

    public void GetDamage(float damage, bool task = true)
    {
        health -= damage;

        if (health <= 0f)
        {
            OnDead.Invoke();

            health = 0;

            if (isHealWhenDead == true)
                GetHeal(maxHealth);

            if (isDespawnWhenDead == true)
            {
                NightPool.Despawn(gameObject);

                if (task == true)
                    TaskOnKill.Instance.Increase();
            }
            
            return;
        }

        OnGetDamage.Invoke();

        OnGetDamageInt.Invoke(1);
    }

    public void GetHeal(float heal)
    {
        health += heal;

        OnGetHeal.Invoke();

        if (health > maxHealth)
            health = maxHealth;
    }
}