using UnityEngine;
using UnityEngine.UI;

public class ChangeLevelManager : MonoBehaviour
{
    [SerializeField]
    private ButtonPreset[] buttons;

    [SerializeField]
    private TransitionLevel transitionLevel;

    [SerializeField]
    private float delayStartTransitionLevel;

    private int unlockLevels;

    private void Start()
    {
        unlockLevels = PlayerPrefs.GetInt("Level", 1);

        for (int i = unlockLevels; i < buttons.Length; i++)
        {
            buttons[i].button.interactable = false;

            if (buttons[i].image != null)
                buttons[i].image.color = Colors.disabledColor;
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;

            buttons[index].button.onClick.AddListener(() => transitionLevel.LoadLevelChoice(buttons[index].numberLevel, delayStartTransitionLevel));
        }
    }
}

[System.Serializable]
public struct ButtonPreset
{
    public Button button;

    public Image image;

    public int numberLevel;
}