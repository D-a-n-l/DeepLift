using UnityEngine;
using UnityEngine.Events;

public class ActionsByKeyCode : MonoBehaviour
{
    [SerializeField]
    private TypeActions _typeActions;

    [SerializeField]
    private KeyCode _keyCode = KeyCode.Escape;

    [SerializeField]
    private bool _isControl = true;

    [Space(10)]
    [SerializeField]
    private UnityEvent OnPressedFirst;

    [Space(10)]
    [SerializeField]
    private UnityEvent OnPressedSecond;

    private bool _isPressed = false;

    private void Update()
    {
        if (_isControl == true && ActionsBy.IsCan == false)
            return;

        if (_typeActions == TypeActions.ActionsByDown)
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
        else if (_typeActions == TypeActions.ActionsByDownUp)
        {
            if (Input.GetKeyDown(_keyCode))
            {
                OnPressedFirst?.Invoke();
            }
            else if (Input.GetKeyUp(_keyCode))
            {
                OnPressedSecond?.Invoke();
            }
        }
    }
}

public enum TypeActions
{
    ActionsByDown,
    ActionsByDownUp
}