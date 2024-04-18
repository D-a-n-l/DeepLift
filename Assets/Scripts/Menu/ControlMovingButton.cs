using UnityEngine;

public class ControlMovingButton : MonoBehaviour
{
    [SerializeField]
    private MovingButton[] movingButtons;

    [SerializeField]
    private SupportMovingButton[] controlMovingButton;

    private void Start()
    {
        Enable(false);
    }

    public void Enable(bool isEnable)
    {
        if (movingButtons != null)
        {
            for (int i = 0; i < movingButtons.Length; i++)
            {
                switch (movingButtons[i].type)
                {
                    case Enums.TypeMovingButton.Joystick:
                        movingButtons[i].fixedJoystick.enabled = !isEnable;
                        break;
                    case Enums.TypeMovingButton.Shoot:
                        movingButtons[i].shooting.enabled = !isEnable;
                        break;
                    case Enums.TypeMovingButton.Sprint:
                        movingButtons[i].sprint.enabled = !isEnable;
                        break;
                    default:
                        movingButtons[i].fixedJoystick.enabled = !isEnable;
                        break;
                }

                movingButtons[i].enabled = isEnable;
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