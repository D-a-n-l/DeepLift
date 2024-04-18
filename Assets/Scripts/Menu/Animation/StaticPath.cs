using DG.Tweening;
using UnityEngine;

public class StaticPath : MonoBehaviour
{
    [SerializeField]
    private Vector3[] positions;

    [SerializeField] 
    private float duration;

    [SerializeField]
    private Ease ease;

    [SerializeField]
    private PathType pathType;

    private void Start()
    {
        DOTween.Sequence()
            .Append(transform.DOPath(positions, duration, pathType)
            .SetEase(ease))
            .SetLoops(-1);
    }

    private void OnDestroy()
    {
        DOTween.KillAll();
    }
}