using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class Learning : MonoBehaviour
{
    [SerializeField]
    private Image background;

    [SerializeField]
    private TextTranslator text;

    [SerializeField]
    private PresetLearning[] presetLearning;

    private int amountClick = 0;

    private void Start()
    {
        Time.timeScale = 0;
    }

    public void NextPresetLearning()
    {
        if (amountClick >= presetLearning.Length)
        {
            GetComponent<Canvas>().enabled = false;

            Time.timeScale = 1;
        }
        else
        {
            text.key = presetLearning[amountClick].key;
            text.ReTranslate();

            background.sprite = presetLearning[amountClick].sprite;
        }

        amountClick++;
    }
}

[System.Serializable]
public struct PresetLearning
{
    public string key;

    public Sprite sprite;
}