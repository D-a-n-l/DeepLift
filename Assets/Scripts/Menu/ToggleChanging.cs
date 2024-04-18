using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleChanging : SliderChanging
{
    private Toggle toggle;

    private void Start()
    {
        toggle = GetComponent<Toggle>();

        LoadValue(nameKey);
    }

    public void ChangeSound(bool enabled)
    {
        if (type == Enums.TypeSliderChanging.Music)
        {
            if (enabled)
                mixer.audioMixer.SetFloat(nameKey, maxValue);
            else
                mixer.audioMixer.SetFloat(nameKey, minValue);
        }

        SaveValueF(nameKey, enabled ? 1 : 0);
    }

    public void SaveValue(bool enabled)
    {
        SaveValueI(nameKey, enabled ? 1 : 0);
    }

    private void LoadValue(string name)
    {
        toggle.isOn = PlayerPrefs.GetInt(name) == 1;
    }
}