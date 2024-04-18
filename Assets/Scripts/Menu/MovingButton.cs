using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MovingButton : MonoBehaviour, IDragHandler, ISelectHandler, IDeselectHandler
{
    public Enums.TypeMovingButton type;

    [SerializeField]
    private GameObject supportButtons;

    [ReadOnly]
    [SerializeField]
    private Vector2 defaultPosition;

    [SerializeField]
    private string keyPositionX;

    public string KeyPositionX => keyPositionX;

    [SerializeField] 
    private string keyPositionY;

    public string KeyPositionY => keyPositionY;

    [HideInInspector]
    public FixedJoystick fixedJoystick;

    [HideInInspector]
    public Button button;

    [HideInInspector]
    public Shooting shooting;

    [HideInInspector]
    public Sprint sprint;

    private Camera mainCamera;

    private RectTransform rectTransform;

    [Button("Set current position as default")]
    private void SetPosition()
    {
        defaultPosition = rectTransform.anchoredPosition;
    }

    private void Awake()
    {
        mainCamera = Camera.main;

        rectTransform = GetComponent<RectTransform>();

        switch (type)
        {
            case Enums.TypeMovingButton.Joystick:
                button = GetComponent<Button>();
                fixedJoystick = GetComponent<FixedJoystick>();
                break;
            case Enums.TypeMovingButton.Shoot:
                shooting = GetComponent<Shooting>();
                break;
            case Enums.TypeMovingButton.Sprint:
                sprint = GetComponent<Sprint>();
                break;
            default:
                button = GetComponent<Button>();
                fixedJoystick = GetComponent<FixedJoystick>();
                break;
        }

        UpdatePosition();
    }

    public void CheckJoystick(bool isEnable)
    {
        if (type == Enums.TypeMovingButton.Joystick)
        {
            button.enabled = !isEnable;

            fixedJoystick.enabled = isEnable;
        }
    }

    public void UpdatePosition()
    {
        rectTransform.anchoredPosition = new Vector2(PlayerPrefs.GetFloat(keyPositionX, defaultPosition.x), (PlayerPrefs.GetFloat(keyPositionY, defaultPosition.y)));
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 normalPosition = mainCamera.ScreenPointToRay(eventData.position).origin;

        transform.position = normalPosition;
    }

    public void OnSelect(BaseEventData eventData)
    {
        supportButtons.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        supportButtons.SetActive(false);

        PlayerPrefs.SetFloat(keyPositionX, rectTransform.anchoredPosition.x);
        PlayerPrefs.SetFloat(keyPositionY, rectTransform.anchoredPosition.y);
    }
}