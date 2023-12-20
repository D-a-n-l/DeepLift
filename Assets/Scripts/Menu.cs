using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Header("Mixer managment volume")]
    [SerializeField] 
    private AudioMixerGroup mixerMusic;

    [SerializeField] 
    private AudioMixerGroup mixerSounds;

    [SerializeField] 
    private AudioMixerGroup mixerSoundsUI;

    [Header("Image для работы с ними")]
    [SerializeField] 
    private Image bgJoyStick;

    [SerializeField] 
    private Image pointJoyStick;

    [SerializeField] 
    private Image shootBtBg;

    [SerializeField] 
    private Image bulletBt;

    [SerializeField] 
    private Image lungeBtBg;

    [SerializeField] 
    private Image lungeBt;

    [Header("Изменение чего-либо")]
    [SerializeField] 
    private Slider changeMaster;

    [SerializeField] 
    private Slider changeSounds;

    [Space(20)]
    [SerializeField] 
    private Toggle changeSoundsUI;

    [Space(20)]
    [SerializeField] 
    private Slider changeTransperentBg;

    [SerializeField] 
    private Slider changeTransperentPoint;

    [Space(20)]
    [SerializeField] 
    private Slider changeTransperentShootBtBg;

    [SerializeField] 
    private Slider changeTransperentShootBt;

    [Space(20)]
    [SerializeField] 
    private Slider changeTransperentLungeBtBg;

    [SerializeField] 
    private Slider changeTransperentLungeBt;

    [Space(20)]
    [SerializeField] 
    private Slider changeSizeJoystick;

    [SerializeField] 
    private Slider changeSizeShootBt;

    [SerializeField] 
    private Slider changeSizeLungeBt;

    [Header("Начльная анимация")]
    [SerializeField] 
    private Animator shotButtons;

    [SerializeField] 
    private Animator shotButtons2;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Menu") { shotButtons.SetTrigger("Go"); shotButtons2.SetTrigger("Go2"); }

        //LoadSlider(changeMaster, "MusicVolume", 1f);
        //LoadSlider(changeSounds, "SoundsVolume", 1f);

        //LoadToggle(changeSoundsUI, "SoundUI");

        //LoadSlider(changeTransperentBg, "TransperentBg", 1f);
        //LoadSlider(changeTransperentPoint, "TransperentPoint", 1f);

        //LoadSlider(changeTransperentShootBtBg, "TransperentShootBtBg", 1f);
        //LoadSlider(changeTransperentShootBt, "TransperentShootBt", 1f);

        //LoadSlider(changeTransperentLungeBtBg, "TransperentLungeBtBg", 1f);
        //LoadSlider(changeTransperentLungeBt, "TransperentLungeBt", 1f);

        //LoadSlider(changeSizeJoystick, "JoystickSize", 350f);
        //LoadSlider(changeSizeShootBt, "ShootSize", 225f);
        //LoadSlider(changeSizeLungeBt, "LungeSize", 166f);
    }

    public void ChangeTimeScale(int value) => Time.timeScale = value;

    //Change volume
    public void SliderMaster(float volumeM)
    {
        mixerMusic.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, volumeM));
        SaveFloat("MusicVolume", volumeM);
    }

    public void SliderSounds(float volumeS)
    {
        mixerSounds.audioMixer.SetFloat("SoundsVolume", Mathf.Lerp(-80, 0, volumeS));
        SaveFloat("SoundsVolume", volumeS);
    }

    public void ToggleSounds(bool enabled)
    {
        if(enabled)
            mixerSoundsUI.audioMixer.SetFloat("SoundUI", 0);
        else
            mixerSoundsUI.audioMixer.SetFloat("SoundUI", -80);

        SaveInt("SoundUI", enabled ? 1 : 0);
    }

    //Transperent button
    public void SliderJoyBg(float transperent)
    {
        TransperentBt(bgJoyStick, changeTransperentBg);
        SaveFloat("TransperentBg", transperent);
    }

    public void SliderJoyPoint(float transperent)
    {
        TransperentBt(pointJoyStick, changeTransperentPoint);
        SaveFloat("TransperentPoint", transperent);
    }

    public void SliderShootBtBg(float transperent)
    {
        TransperentBt(shootBtBg, changeTransperentShootBtBg);
        SaveFloat("TransperentShootBtBg", transperent);
    }

    public void SliderShootBt(float transperent)
    {
        TransperentBt(bulletBt, changeTransperentShootBt);
        SaveFloat("TransperentShootBt", transperent);
    }

    public void SliderLungeBtBg(float transperent)
    {
        TransperentBt(lungeBtBg, changeTransperentLungeBtBg);
        SaveFloat("TransperentLungeBtBg", transperent);
    }

    public void SliderLungeBt(float transperent)
    {
        TransperentBt(lungeBt, changeTransperentLungeBt);
        SaveFloat("TransperentLungeBt", transperent);
    }

    //Change size button
    public void SliderShootSize(float size)
    {
        SizeBt(shootBtBg, bulletBt, changeSizeShootBt, size);
        SaveFloat("ShootSize", size);
    }

    public void SliderJoystickSize(float size)
    {
        SizeBt(bgJoyStick, bgJoyStick, changeSizeJoystick, size);
        SaveFloat("JoystickSize", size);
    }

    public void SliderLungeSize(float size)
    {
        SizeBt(lungeBtBg, lungeBt, changeSizeLungeBt, size);
        SaveFloat("LungeSize", size);
    }

    //Refactoring
    public void SaveFloat(string name, float dynamicFloat)
    {
        PlayerPrefs.SetFloat(name, dynamicFloat);
    }

    private void SaveInt(string name, int dynamicInt)
    {
        PlayerPrefs.SetInt(name, dynamicInt);
    }

    public void LoadSlider(Slider slider, string name, float dynamicFloat)
    {
        slider.value = PlayerPrefs.GetFloat(name, dynamicFloat);
    }

    private void LoadToggle(Toggle toggle, string name)
    {
        toggle.isOn = PlayerPrefs.GetInt(name) == 1;
    }

    public void TransperentBt(Image image, Slider slider)
    {
        image.color = new Color(1f, 1f, 1f, slider.value);
    }

    private void SizeBt(Image image1, Image image2, Slider slider, float dynamicFloat)
    {
        image1.rectTransform.sizeDelta = new Vector2(dynamicFloat, dynamicFloat);
        image2.rectTransform.sizeDelta = new Vector2(dynamicFloat, dynamicFloat);
        dynamicFloat = slider.value;
    }
}