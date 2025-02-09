using UnityEngine;
using UnityEngine.Events;

public class InvokeAction : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnStarted;

    [Space(10)]
    [SerializeField]
    private UnityEvent OnInvoked;

    private void Awake() => OnStarted?.Invoke();

    public void Activate() => OnInvoked?.Invoke();
}