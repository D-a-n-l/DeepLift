using UnityEngine;
using NaughtyAttributes;

public class FlipObject : MonoBehaviour
{
    [SerializeField]
    private bool isChangePosition = true;

    [ShowIf(nameof(isChangePosition))]
    [SerializeField]
    private Vector3 defaultPosition;

    [ShowIf(nameof(isChangePosition))]
    [SerializeField]
    private Vector3 newPosition;

    [HideIf(nameof(isChangePosition))]
    [SerializeField]
    private Quaternion defaultRotation;

    [HideIf(nameof(isChangePosition))]
    [SerializeField]
    private Quaternion newRotation;

    private bool isDefault = true;

    private void Start()
    {
        PlayerMovement.Instance.OnFlip.AddListener(Flip);
    }

    public void Flip()
    {
        isDefault = !isDefault;

        if (isChangePosition == true)
        {
            if (isDefault == true)
                transform.localPosition = defaultPosition;
            else
                transform.localPosition = newPosition;
        }
        else
        {
            if (isDefault == true)
                transform.localRotation = defaultRotation;
            else
                transform.localRotation = newRotation;
        }
    }
}