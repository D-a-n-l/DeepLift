using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class SupportMovingButton : MonoBehaviour
{
    [SerializeField]
    private Image mainImage;

    [SerializeField]
    private Vector2 defaultSizeMainImage = new Vector2(64, 64);

    public PresetButton[] supportButtons;

    [SerializeField]
    private PresetChangeColor changeColor;

    [SerializeField]
    private float multiplierSupportImages = 3f;

    [SerializeField]
    private PresetKeyMinMax size;

    [SerializeField]
    private PresetKeyMinMax transparency;

    public PresetKeyMinMax Size => size;

    public PresetKeyMinMax Transparency => transparency;

    private WaitForSecondsRealtime waitForSecondsRealtime;

    private void Awake()
    {
        waitForSecondsRealtime = new WaitForSecondsRealtime(changeColor.duration);

        UpdateSizeAndTransparency(false);
    }

    public void UpdateSizeAndTransparency(bool isDefault)
    {
        Vector2 primaryValue = new Vector2(PlayerPrefs.GetFloat(size.key, defaultSizeMainImage.x), PlayerPrefs.GetFloat(size.key, defaultSizeMainImage.y));

        mainImage.rectTransform.sizeDelta = primaryValue;

        if (supportButtons != null)
        {
            for (int i = 0; i < supportButtons.Length; i++)
            {
                if (isDefault == true)
                {
                    if (supportButtons[i].button == null)
                        supportButtons[i].image.rectTransform.sizeDelta = supportButtons[i].defaultSize;
                    else
                        supportButtons[i].button.image.rectTransform.sizeDelta = supportButtons[i].defaultSize;
                }
                else
                {
                    if (supportButtons[i].button == null)
                    {
                        supportButtons[i].image.rectTransform.sizeDelta += new Vector2((primaryValue.x - defaultSizeMainImage.x) / multiplierSupportImages,
                            (primaryValue.y - defaultSizeMainImage.y) / multiplierSupportImages);
                    }
                    else
                    {
                        int index = i;

                        supportButtons[index].button.onClick.AddListener(() => supportButtons[index].unityEvent.Invoke());

                        supportButtons[index].button.onClick.AddListener(() => StartCoroutine(Click(supportButtons[index].button.image)));

                        supportButtons[index].button.image.rectTransform.sizeDelta += new Vector2((primaryValue.x - defaultSizeMainImage.x) / multiplierSupportImages,
                            (primaryValue.y - defaultSizeMainImage.y) / multiplierSupportImages);
                    }
                }
            }
        }

        mainImage.color = new Color(mainImage.color.r, mainImage.color.g, mainImage.color.b, PlayerPrefs.GetFloat(transparency.key, 1f));
    }

    public void ChangeSize(float value)
    {
        Vector2 newValueMain = new Vector2(mainImage.rectTransform.sizeDelta.x + value, mainImage.rectTransform.sizeDelta.y + value);

        Vector2 newValueSupport = new Vector2(value / multiplierSupportImages, value / multiplierSupportImages);

        if (newValueMain.x > size.max)
        {
            newValueMain = new Vector2(size.max, size.max);

            newValueSupport = Vector2.zero;
        }

        if (newValueMain.x < size.min)
        {
            newValueMain = new Vector2(size.min, size.min);

            newValueSupport = Vector2.zero;
        }

        mainImage.rectTransform.sizeDelta = newValueMain;

        if (supportButtons != null)
        {
            for (int i = 0; i < supportButtons.Length; i++)
            {
                if (supportButtons[i].button == null)
                    supportButtons[i].image.rectTransform.sizeDelta += newValueSupport;
                else
                    supportButtons[i].button.image.rectTransform.sizeDelta += newValueSupport;
            }
        }

        PlayerPrefs.SetFloat(size.key, mainImage.rectTransform.sizeDelta.x);
    }

    public void ChangeTransparency(float value)
    {
        float output = mainImage.color.a + value;

        if (output > transparency.max)
            output = transparency.max;

        if (output < transparency.min)
            output = transparency.min;

        mainImage.color = new Color(mainImage.color.r, mainImage.color.g, mainImage.color.b, output);

        PlayerPrefs.SetFloat(transparency.key, mainImage.color.a);
    }

    private IEnumerator Click(Image image)
    {
        image.color = changeColor.pressed;

        yield return waitForSecondsRealtime;

        image.color = changeColor.normal;
    }
}

[System.Serializable]
public struct PresetButton
{
    public Image image;

    public Button button;

    public Vector2 defaultSize;

    public UnityEvent unityEvent;
}

[System.Serializable]
public struct PresetKeyMinMax
{
    public string key;

    public float min;

    public float max;
}

[System.Serializable]
public struct PresetChangeColor
{
    public Color normal;

    public Color pressed;

    public float duration;
}