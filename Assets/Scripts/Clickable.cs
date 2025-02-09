using UnityEngine;
using UnityEngine.Events;

public class Clickable : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnMouseDowned;

    private void OnMouseDown()
    {
        OnMouseDowned?.Invoke();
    }
}