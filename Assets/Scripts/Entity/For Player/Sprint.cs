using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AnimationManagement))]
public class Sprint : MonoBehaviour,  IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float cooldown;

    [SerializeField]
    private PlayerMovement playerMovement;

    [SerializeField]
    private AnimationManagement animationManagement;

    [HideInInspector]
    public UnityEvent OnSprint;

    private WaitForSeconds waitForSeconds;

    private float defaultSpeed;

    private bool isPressed = false;

    private void Start()
    {
        if (playerMovement != null)
            defaultSpeed = playerMovement.Speed;

        waitForSeconds = new WaitForSeconds(cooldown);

        GameManager.Instance.OnEnteredLift.AddListener(() =>
        {
            StopAllCoroutines();

            playerMovement.SetSpeed(0);
        });
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
            
        StartCoroutine(On());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

    public void OnUpdateSelected(BaseEventData eventData)
    {
        if (isPressed == true)
            StartCoroutine(On());
    }

    private IEnumerator On()
    {
        if(animationManagement.IsReady == true)
        {
            OnSprint?.Invoke();

            playerMovement.SetSpeed(speed);
            playerMovement.animationInterpolation = 1.5f;

            yield return waitForSeconds;

            Off();
        }
    }

    public void Off()
    {
        playerMovement.SetSpeed(defaultSpeed);
        playerMovement.animationInterpolation = 1f;
    }
}