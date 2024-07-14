using UnityEngine;
using NaughtyAttributes;

public class HitBox : MonoBehaviour
{
    [SerializeField]
    private bool twoMultiply = false;

    [SerializeField]
    private float firstMultiply;

    [ShowIf(nameof(twoMultiply))]
    [SerializeField]
    private float secondMultiply;

    private float multiply;

    public float Multiply => multiply;

    private void Start() => multiply = firstMultiply;

    public void ChangeMultiply(bool first)
    {
        if (first)
            multiply = firstMultiply;
        else
            multiply = secondMultiply;
    }
}