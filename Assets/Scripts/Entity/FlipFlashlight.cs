using UnityEngine;

public class FlipFlashlight : MonoBehaviour
{
    [SerializeField]
    private Vector3 defaultPosition;

    [SerializeField]
    private Vector3 newPosition;

    private bool isDefaultPosition = true;

    private void Start()
    {
        PlayerMovement.Instance.OnFlip.AddListener(Flip);
    }

    public void Flip()
    {
        isDefaultPosition = !isDefaultPosition;

        if (isDefaultPosition)
            transform.localPosition = defaultPosition;
        else
            transform.localPosition = newPosition;
    }
}