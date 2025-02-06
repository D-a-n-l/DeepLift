using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class InvokeWithDelay : MonoBehaviour
{
    [SerializeField]
    private float _timeDelay;

    [SerializeField]
    private bool _isInvokeStart = true;

    [SerializeField]
    private UnityEvent OnInvoked;

    private WaitForSeconds _delay;

    private IEnumerator Start()
    {
        _delay = new WaitForSeconds(_timeDelay);

        if (_isInvokeStart == false)
            yield break;

        Activate();
    }

    public void Activate() => StartCoroutine(ActivateCoroutine());

    private IEnumerator ActivateCoroutine()
    {
        yield return _delay;

        OnInvoked?.Invoke();
    }
}