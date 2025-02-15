using UnityEngine;
using UnityEngine.Events;

public class CheckCompleteTrainingForPC : MonoBehaviour
{
    [SerializeField]
    private int _targetKeyboard;

    [SerializeField]
    private int _targetMouse;

    [Space(10)]
    [SerializeField]
    private UnityEvent OnIncreasedKeyboard;

    [Space(10)]
    [SerializeField]
    private UnityEvent OnIncreasedMouse;

    [Space(10)]
    [SerializeField]
    private UnityEvent OnAllIncreased;

    private int _keyboard;

    private int _mouse;

    public void IncreaseKeyboard()
    {
        _keyboard++;

        if (_keyboard >= _targetKeyboard)
        {
            OnIncreasedKeyboard?.Invoke();

            if (_mouse >= _targetMouse)
            {
                OnAllIncreased?.Invoke();
            }
        }
    }

    public void IncreaseMouse()
    {
        _mouse++;

        if (_mouse >= _targetMouse)
        {
            OnIncreasedMouse?.Invoke();

            if (_keyboard >= _targetKeyboard)
            {
                OnAllIncreased?.Invoke();
            }
        }
    }
}