using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using NaughtyAttributes;

[RequireComponent(typeof(Slider))]
public class SliderChanging : MonoBehaviour
{
    [SerializeField]
    protected Enums.TypeSliderChanging type;

    [ShowIf(nameof(type), Enums.TypeSliderChanging.Music)]
    [SerializeField]
    protected AudioMixerGroup mixer;

    [ShowIf(nameof(type), Enums.TypeSliderChanging.Music)]
    [SerializeField]
    protected float maxValue = 0;

    [ShowIf(nameof(type), Enums.TypeSliderChanging.Music)]
    [SerializeField]
    protected float minValue = -62;

    [ShowIf(nameof(type), Enums.TypeSliderChanging.Image)]
    [SerializeField]
    private Image mainImage;

    [SerializeField]
    protected string nameKey;

    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();

        LoadValue(nameKey, slider.value / 2);
    }

    public void ChangeVolume(float volumeM)
    {
        if (type == Enums.TypeSliderChanging.Music)
            mixer.audioMixer.SetFloat(nameKey, Mathf.Lerp(minValue, maxValue, volumeM));

        SaveValueF(nameKey, volumeM);
    }

    public void ChangeTransperent(float transperent)
    {
        if(type == Enums.TypeSliderChanging.Image)
            mainImage.color = new Color(1f, 1f, 1f, slider.value);

        SaveValueF(nameKey, transperent);
    }

    public void ChangeSize(float dynamicFloat)
    {
        if(type == Enums.TypeSliderChanging.Image)
            mainImage.rectTransform.sizeDelta = new Vector2(dynamicFloat, dynamicFloat);

        dynamicFloat = slider.value;

        SaveValueF(nameKey, dynamicFloat);
    }

    private void LoadValue(string name, float value)
    {
        slider.value = PlayerPrefs.GetFloat(name, value);
    }

    protected void SaveValueF(string name, float value)
    {
        PlayerPrefs.SetFloat(name, value);
    }

    protected void SaveValueI(string name, int value)
    {
        PlayerPrefs.SetInt(name, value);
    }
}