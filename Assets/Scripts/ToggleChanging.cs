using UnityEngine;
using UnityEngine.UI;

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
        if (enabled)
            mixer.audioMixer.SetFloat(nameKey, 0);
        else
            mixer.audioMixer.SetFloat(nameKey, -80);

        SaveValueI(nameKey, enabled ? 1 : 0);
    }

    private void LoadValue(string name)
    {
        toggle.isOn = PlayerPrefs.GetInt(name) == 1;
    }

    private void SaveValueI(string name, int dynamicInt)
    {
        PlayerPrefs.SetInt(name, dynamicInt);
    }
}