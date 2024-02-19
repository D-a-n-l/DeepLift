using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AnimationManagement))]
public class Sprint : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private PlayerMovement playerMovement;

    [HideInInspector]
    public UnityEvent OnSprint;

    private float defaultSpeed;

    private void Start()
    {
        defaultSpeed = playerMovement.Speed;
    }

    public void On(AnimationManagement animationManagement)
    {
        if(animationManagement.IsReady == true)
        {
            playerMovement.SetSpeed(speed);
            playerMovement.animationInterpolation = 1.5f;

            OnSprint.Invoke();
        }
    }

    public void Off()
    {
        playerMovement.SetSpeed(defaultSpeed);
        playerMovement.animationInterpolation = 1f;
    }
}