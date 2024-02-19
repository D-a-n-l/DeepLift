using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseGame : MonoBehaviour
{
    [SerializeField]
    private HealthControl healthControl;

    [SerializeField]
    private TransitionLevel transitionLevel;

    [SerializeField]
    private Canvas canvas;

    private void Start()
    {
        healthControl.OnDead.AddListener(value => canvas.enabled = value);
    }

    public void Continue()
    {
        canvas.enabled = false;

        transitionLevel.LoadLevel(false);
    }

    public void SecondLife()
    {
        //showAd
        //posle reklami healing
        canvas.enabled = false;

        healthControl.GetHeal(healthControl.MaxHealth);
    }
}