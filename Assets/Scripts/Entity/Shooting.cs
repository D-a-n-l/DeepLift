using NTC.Pool;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private bool loopShooting = true;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Transform shootPosition;

    [SerializeField]
    public float cooldown = 0;

    [HideInInspector]
    public UnityEvent OnShot;

    private float nextFire = 0;

    private WaitForSeconds wait;

    private void Awake()
    {
        wait = new WaitForSeconds(cooldown);
    }

    private IEnumerator Start()
    {
        while (loopShooting)
        {
            yield return StartCoroutine(ShootLoop());
        }
    }

    public IEnumerator ShootLoop()
    {
        yield return wait;

        NightPool.Spawn(bullet, shootPosition.position, shootPosition.rotation);
    }

    public void Shoot(AnimationManagement animationManagement)
    {
        if (Time.time > nextFire && animationManagement.IsReady == true)
        {
            nextFire = Time.time + cooldown;

            NightPool.Spawn(bullet, shootPosition.position, shootPosition.rotation);

            OnShot.Invoke();
        }
    }
}