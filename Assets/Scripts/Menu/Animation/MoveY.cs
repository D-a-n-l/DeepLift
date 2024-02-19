using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MoveY : MonoBehaviour
{
    [SerializeField]
    private Button buttonAddListener;

    [SerializeField]
    private float newPosition;

    [SerializeField]
    private float duration;

    private void Start() => buttonAddListener.onClick.AddListener(() => Go());

    private void Go() => transform.DOMoveY(newPosition, duration);
}