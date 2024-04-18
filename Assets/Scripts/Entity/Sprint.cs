using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AnimationManagement))]
public class Sprint : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float cooldown;

    [SerializeField]
    private PlayerMovement playerMovement;

    [HideInInspector]
    public UnityEvent OnSprint;

    private WaitForSeconds waitForSeconds;

    private float defaultSpeed;

    private void Start()
    {
        if (playerMovement != null)
            defaultSpeed = playerMovement.Speed;

        waitForSeconds = new WaitForSeconds(cooldown);
    }

    public void Onn(AnimationManagement animationManagement)
    {
        StartCoroutine(On(animationManagement));
    }

    private IEnumerator On(AnimationManagement animationManagement)
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