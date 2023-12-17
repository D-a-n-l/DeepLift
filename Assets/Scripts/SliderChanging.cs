using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SliderChanging : MonoBehaviour
{
    [SerializeField]
    protected AudioMixerGroup mixer;

    [SerializeField]
    private Image image;

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
        image.color = new Color(1f, 1f, 1f, slider.value);
        SaveValueF(nameKey, transperent);
    }

    private void ChangeSize(Image image1, Image image2, float dynamicFloat)
    {
        image1.rectTransform.sizeDelta = new Vector2(dynamicFloat, dynamicFloat);
        image2.rectTransform.sizeDelta = new Vector2(dynamicFloat, dynamicFloat);

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