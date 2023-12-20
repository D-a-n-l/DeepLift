using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SliderChanging : MonoBehaviour
{
    [SerializeField]
    protected AudioMixerGroup mixer;

    [SerializeField]
    private Image mainImage;

    [SerializeField]
    private Image secondaryImage;

    [SerializeField]
    protected string nameKey;

    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();

        LoadValue(nameKey, 1f);
    }

    public void ChangeVolume(float volumeM)
    {
        mixer.audioMixer.SetFloat(nameKey, Mathf.Lerp(-80, 0, volumeM));

        SaveValueF(nameKey, volumeM);
    }

    public void ChangeTransperent(float transperent)
    {
        mainImage.color = new Color(1f, 1f, 1f, slider.value);

        SaveValueF(nameKey, transperent);
    }

    public void ChangeSize(float dynamicFloat)
    {
        mainImage.rectTransform.sizeDelta = new Vector2(dynamicFloat, dynamicFloat);

        if(secondaryImage != null)
            secondaryImage.rectTransform.sizeDelta = new Vector2(dynamicFloat, dynamicFloat);

        dynamicFloat = slider.value;

        SaveValueF(nameKey, dynamicFloat);
    }

    private void LoadValue(string name, float dynamicFloat)
    {
        slider.value = PlayerPrefs.GetFloat(name, dynamicFloat);
    }

    private void SaveValueF(string name, float dynamicFloat)
    {
        PlayerPrefs.SetFloat(name, dynamicFloat);
    }
}