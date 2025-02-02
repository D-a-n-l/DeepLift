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

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;

            buttons[index].button.onClick.AddListener(() => transitionLevel.LoadLevelChoice(buttons[index].numberLevel, delayStartTransitionLevel));
        }
    }

    public void UnlockLevels(int unlockLevels)
    {
        for (int i = unlockLevels; i < buttons.Length; i++)
        {
            buttons[i].button.interactable = false;

            if (buttons[i].image != null)
                buttons[i].image.color = Colors.disabledColor;
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