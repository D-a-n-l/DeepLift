using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using NaughtyAttributes;

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

    [ShowIf(nameof(type), Enums.TypeSliderChanging.FrameRate)]
    [SerializeField]
    private TMP_Text counterText;

    [SerializeField]
    protected string nameKey;

    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();

        if (type == Enums.TypeSliderChanging.FrameRate)
        {
            slider.maxValue = Screen.currentResolution.refreshRate;

            LoadValueI(nameKey, (int)slider.maxValue);
        }
        else
            LoadValueF(nameKey, slider.value / 2);
    }

    public void ChangeVolume(float volume)
    {
        if (type == Enums.TypeSliderChanging.Music)
            mixer.audioMixer.SetFloat(nameKey, Mathf.Log10(volume) * 20);//this nameKey - change in settings Mixer

        SaveValueF(nameKey, volume);
    }

    public void ChangeTransperent(float transperent)
    {
        if(type == Enums.TypeSliderChanging.Image)
            mainImage.color = new Color(1f, 1f, 1f, transperent);

        SaveValueF(nameKey, transperent);
    }

    public void ChangeSize(float size)
    {
        if(type == Enums.TypeSliderChanging.Image)
            mainImage.rectTransform.sizeDelta = new Vector2(size, size);

        SaveValueF(nameKey, size);
    }

    public void ChangeFrameRate(float value)
    {
        if (type == Enums.TypeSliderChanging.FrameRate)
        {
            Application.targetFrameRate = (int)value;

            counterText.text = value.ToString();
        }

        SaveValueI(nameKey, (int)value);
    }

    private void LoadValueF(string name, float value)
    {
        slider.value = PlayerPrefs.GetFloat(name, value);
    }

    private void LoadValueI(string name, int value)
    {
        slider.value = PlayerPrefs.GetInt(name, value);
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