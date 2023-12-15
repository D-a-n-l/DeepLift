using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Image healtBarEffect;

    private float healthSpeed = 0.003f;

    private JohnWickController jonh;

    private void Start()
    {
        jonh = FindObjectOfType<JohnWickController>();
        healthBar = GetComponent<Image>();
    }

    private void Update()
    {
        healthBar.fillAmount = jonh.health;
        if (healthBar.fillAmount >= 1f)
        {
            ChoiceColor(0f, 1f, 0f, .8f);
        }
        else if (healthBar.fillAmount >= .6f && healthBar.fillAmount <= .8f)
        {
            ChoiceColor(1f, 1f, 0f, 1f);
        }
        else if (healthBar.fillAmount >= .3f && healthBar.fillAmount <= .6f)
        {
            ChoiceColor(1f, 0.5f, 0f, 1f);
        }
        else if (healthBar.fillAmount >= .1f && healthBar.fillAmount <= .3f)
        {
            ChoiceColor(1f, 0f, 0f, 1f);
        }

        if (healtBarEffect.fillAmount > healthBar.fillAmount)
        {
            healtBarEffect.fillAmount -= healthSpeed;
        }
        else
        {
            healtBarEffect.fillAmount = healthBar.fillAmount;
        }
    }

    private void ChoiceColor(float Red, float Green, float Blue, float Transparency)
    {
        healthBar.color = new Color(Red, Green, Blue, Transparency);
    }
}