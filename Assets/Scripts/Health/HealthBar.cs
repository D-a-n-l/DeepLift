using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private HealthControl healthControl;

    [SerializeField]
    private Canvas canvas;

    [SerializeField] 
    private Image healthBar;

    [SerializeField] 
    private Image healthBarEffect;

    [SerializeField]
    private float healthSpeed = 0.003f; 

    [SerializeField]
    private ColorPreset[] colors;

    private float maxHealth => healthControl.MaxHealth;

    private double health => healthControl.Health;

    private void Start()
    {
        healthControl.OnGetDamage.AddListener(ChangeHealthBar);
        healthControl.OnGetHeal.AddListener(ChangeHealthBar);

        ChangeHealthBar();
    }

    public void SetActive(bool value) => canvas.enabled = value;

    private void ChangeHealthBar()
    {
        if (health >= maxHealth)
        {
            SetActive(false);
        }
        else
        {
            if (canvas.enabled == false)
                canvas.enabled = true;

            healthBar.fillAmount = ((float)health);

            for (int i = 0; i < colors.Length; i++)
            {
                if (health >= colors[i].minRange && health <= colors[i].maxRange)
                {
                    healthBar.color = colors[i].color;
                    break;
                }
            }

            StartCoroutine(Effect());
        }
    }

    private IEnumerator Effect()
    {
        while (true)
        {
            if (healthBarEffect.fillAmount > healthBar.fillAmount)
            {
                healthBarEffect.fillAmount -= healthSpeed;
            }
            else
            {
                healthBarEffect.fillAmount = healthBar.fillAmount;
                StopAllCoroutines();
            }

            yield return null;
        }
    }
}

[System.Serializable]
public struct ColorPreset
{
    public Color color;

    [Range(0f, 1f)]
    public float maxRange;

    [Range(0f, 1f)]
    public float minRange;
}