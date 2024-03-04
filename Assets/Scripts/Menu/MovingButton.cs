using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class MovingButton : MonoBehaviour, IDragHandler, ISelectHandler, IDeselectHandler
{
    [SerializeField]
    private bool isJoystick;

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

    private Button button;

    private FixedJoystick fixedJoystick;

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

        button = GetComponent<Button>();

        if (isJoystick == true)
            fixedJoystick = GetComponent<FixedJoystick>();

        UpdatePosition();
    }

    public void CheckJoystick(bool isEnable)
    {
        if (isJoystick == true)
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
        button.enabled = false;

        supportButtons.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        button.enabled = true;

        supportButtons.SetActive(false);

        PlayerPrefs.SetFloat(keyPositionX, rectTransform.anchoredPosition.x);
        PlayerPrefs.SetFloat(keyPositionY, rectTransform.anchoredPosition.y);
    }
}