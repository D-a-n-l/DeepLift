using UnityEngine;

[RequireComponent(typeof(AnimationShooting))]
public class Shooting : MonoBehaviour
{
    [SerializeField]
    private bool isAk;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Transform shootPosition;

    [SerializeField]
    public float cooldown = 0;

    private float nextFire = 0;

    private AnimationShooting animationShooting;

    private void Start()
    {
        animationShooting = GetComponent<AnimationShooting>();
    }

    public void Shoot()
    {
        if (Time.time > nextFire && animationShooting.IsReady == true)
        {
            nextFire = Time.time + cooldown;

            Instantiate(bullet, shootPosition.position, shootPosition.rotation);

            if (isAk)
            {
                animationShooting.Play("fast");
            }
            if (!isAk)
            {
                animationShooting.Play("slow");
            }
        }
    }
}