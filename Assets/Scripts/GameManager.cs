using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float timeLevel = 0;

    [Space(10)]
    [SerializeField]
    private AudioSource audioSourceLift;

    [SerializeField]
    private TransitionLevel transitionLevel;

    [Header("Player")]
    [SerializeField]
    private Animator JohnWick;

    [SerializeField]
    private GameObject LegsJohnWick;

    [SerializeField]
    private GameObject HandJohnWick;

    [Space(10)]
    [SerializeField]
    private Canvas buttons;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        StartCoroutine(Open());
    }

    private void OnTriggerEnter2D(Collider2D col) { 
        if (col.GetComponent<JohnWickController>()) animator.SetTrigger("Close"); }

    private IEnumerator Open()
    {
        yield return new WaitForSeconds(timeLevel);

        animator.SetTrigger("Open");
    }

    public void Close()
    {
        buttons.enabled = false;

        HandJohnWick.SetActive(false);
        LegsJohnWick.SetActive(false);

        JohnWick.SetTrigger("idleLift");
    }

    public void PlaySound(AudioClip clip) => audioSourceLift.PlayOneShot(clip);

    public void TransitionLevel() => transitionLevel.Enable();
}