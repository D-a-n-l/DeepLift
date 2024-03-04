using UnityEngine;

public class ControlMovingButton : MonoBehaviour
{
    [SerializeField]
    private MovingButton[] movingButtons;

    [SerializeField]
    private SupportMovingButton[] controlMovingButton;

    private void Awake()
    {
        Enable(false);
    }

    public void Enable(bool isEnable)
    {
        if (movingButtons != null)
        {
            for (int i = 0; i < movingButtons.Length; i++)
            {
                movingButtons[i].enabled = isEnable;

                movingButtons[i].CheckJoystick(!isEnable);
            }
        }
    }

    public void ResetAll()
    {
        if (movingButtons != null && controlMovingButton != null)
        {
            for (int i = 0; i < movingButtons.Length; i++)
            {
                PlayerPrefs.DeleteKey(movingButtons[i].KeyPositionX);
                PlayerPrefs.DeleteKey(movingButtons[i].KeyPositionY);

                movingButtons[i].UpdatePosition();
            }

            for (int i = 0; i < controlMovingButton.Length; i++)
            {
                PlayerPrefs.DeleteKey(controlMovingButton[i].Size.key);
                PlayerPrefs.DeleteKey(controlMovingButton[i].Transparency.key);

                controlMovingButton[i].UpdateSizeAndTransparency(true);
            }
        }
    }
}