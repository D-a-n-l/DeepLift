using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] 
    private Transform followingTarget;

    [Range(0, 1)]
    [SerializeField] 
    private float parallaxStrength = .1f;

    [SerializeField] 
    private bool disableVerticalParallax;

    private Vector3 targetPositon;

    private void Start()
    {
        if (!followingTarget)
            followingTarget = Camera.main.transform;

        targetPositon = followingTarget.position;
    }

    private void Update()
    {
        var delta = followingTarget.position - targetPositon;

        if (disableVerticalParallax)
            delta.y = 0;

        targetPositon = followingTarget.position;

        transform.position += delta * parallaxStrength;
    }
}