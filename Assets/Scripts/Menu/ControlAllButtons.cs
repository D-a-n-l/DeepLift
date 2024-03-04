using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ControlAllButtons : MonoBehaviour
{
    [SerializeField]
    private PresetBt[] buttons;

    private void Start()
    {
        if (buttons != null)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                int index = i;

                buttons[index].button.onClick.AddListener(() => buttons[index].unityEvent.Invoke());
            }
        }
    }
}

[System.Serializable]
public struct PresetBt
{
    public Button button;

    public UnityEvent unityEvent;
}