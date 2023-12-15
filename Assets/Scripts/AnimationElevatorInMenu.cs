using System.Collections;
using UnityEngine;

public class AnimationElevatorInMenu : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [Space(10)]
    [SerializeField] private GameObject elevatorMain;
    [Space(10)]
    [SerializeField] private GameObject elevatortOff;
    [SerializeField] private GameObject elevatortOn;

    private void Start()
    {
        StartCoroutine(elevatorOpen()); 
    }

    private IEnumerator elevatorOpen()
    {
        yield return new WaitForSeconds(10f);
        elevatortOn.SetActive(true);
        elevatorMain.SetActive(true);
        anim.SetTrigger("LiftOpen");
        elevatortOff.SetActive(false);
        StartCoroutine(elevatorClose());
    }

    private IEnumerator elevatorClose()
    {
        yield return new WaitForSecondsRealtime(10f);
        elevatortOn.SetActive(true);
        anim.SetTrigger("LiftClose");
        yield return new WaitForSecondsRealtime(10f);
        anim.SetTrigger("Close");
        yield return new WaitForSecondsRealtime(2f);
        elevatortOff.SetActive(true);
        elevatorMain.SetActive(false);
        StartCoroutine(elevatorOpen());
    }
}