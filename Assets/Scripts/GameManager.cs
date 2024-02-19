using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float timeLevel = 5f;

    [Space(10)]
    [SerializeField]
    private AudioSource lift;

    [SerializeField]
    private TransitionLevel transitionLevel;

    [Header("Triggers")]
    [SerializeField]
    private Animator player;

    [SerializeField]
    private string namePlayerLift;

    [SerializeField]
    private string nameOpen;

    [SerializeField]
    private string nameClose;

    [Space(10)]
    [SerializeField]
    private Canvas buttons;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        StartCoroutine(Open());
    }

    private IEnumerator Open()
    {
        yield return new WaitForSeconds(timeLevel);

        animator.SetTrigger(nameOpen);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<PlayerMovement>())
        {
            animator.SetTrigger(nameClose);

            player.SetTrigger(namePlayerLift);

            buttons.enabled = false;
        }
    }

    public void Close()
    {
        transitionLevel.LoadLevel(true);
    }

    public void PlaySound(AudioClip clip) => lift.PlayOneShot(clip);
}