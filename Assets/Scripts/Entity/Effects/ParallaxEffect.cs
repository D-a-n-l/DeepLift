using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] 
    private Transform followingTarget;

    [Range(0, 1)]
    [SerializeField] 
    private float parallaxStrength = .1f;

    [SerializeField]
    private bool disableHorizontalParallax = false;

    [SerializeField] 
    private bool disableVerticalParallax = false;

    private Vector3 targetPositon;

    private void Start()
    {
        if (!followingTarget)
            followingTarget = Camera.main.transform;

        targetPositon = followingTarget.position;
    }

    private void LateUpdate()
    {
        if (disableHorizontalParallax == true && disableVerticalParallax == true)
            return;

        Vector3 delta = followingTarget.position - targetPositon;

        if (disableHorizontalParallax == true)
            delta.x = 0;

        if (disableVerticalParallax == true)
            delta.y = 0;

        targetPositon = followingTarget.position;

        transform.position += delta * parallaxStrength;
    }
}