using UnityEngine;
using UnityEngine.Events;

public class ActionsByKeyCode : MonoBehaviour
{
    [SerializeField]
    private KeyCode _keyCode = KeyCode.Escape;

    [Space(10)]
    [SerializeField]
    private UnityEvent OnPressedFirst;

    [Space(10)]
    [SerializeField]
    private UnityEvent OnPressedSecond;

    private bool _isPressed = false;

    private void Update()
    {
        if (Input.GetKeyDown(_keyCode))
        {
            _isPressed = !_isPressed;

            if (_isPressed == true)
                OnPressedFirst?.Invoke();
            else
                OnPressedSecond?.Invoke();
        }
    }
}