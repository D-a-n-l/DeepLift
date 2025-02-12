using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ControlAllButtons : MonoBehaviour
{
    [SerializeField]
    private PresetBt[] buttons;

    private void Start()//Invoke Sound bugs, when Init SliderChanging
    {
        if (buttons != null)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                int index = i;

                if (buttons[i].button != null)
                    buttons[index].button.onClick.AddListener(buttons[index].unityEvent.Invoke);
                else if (buttons[i].slider != null)
                    buttons[index].slider.onValueChanged.AddListener((_) => buttons[index].unityEvent.Invoke());
                else if (buttons[i].toggle != null)
                    buttons[index].toggle.onValueChanged.AddListener((_) => buttons[index].unityEvent.Invoke());
                else if (buttons[i].tmpDropdown != null)
                    buttons[index].tmpDropdown.onValueChanged.AddListener((_) => buttons[index].unityEvent.Invoke());
            }
        }
    }
}

[System.Serializable]
public struct PresetBt
{
    public Button button;

    public Slider slider;

    public Toggle toggle;

    public TMP_Dropdown tmpDropdown;

    public UnityEvent unityEvent;
}