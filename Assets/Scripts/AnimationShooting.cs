using UnityEngine;

public class AnimationShooting : MonoBehaviour
{
    [SerializeField]
    private Animator animatorHand;

    [SerializeField]
    private Animator imageButtonAttack;

    [SerializeField]
    private Transform bulletEffectPosition;

    [SerializeField]
    private ParticleSystem bulletEffect;

    private bool isReady = true;

    public bool IsReady => isReady;

    private bool ChangeIsReady(int value)
    {
        return isReady = value == 0 ? false : true;
    }

    public void Play(string slowOrFast)
    {
        ChangeIsReady(0);

        animatorHand.SetTrigger("Shoot");
        imageButtonAttack.SetTrigger(slowOrFast);

        Instantiate(bulletEffect, bulletEffectPosition.position, bulletEffectPosition.rotation);
    }
}