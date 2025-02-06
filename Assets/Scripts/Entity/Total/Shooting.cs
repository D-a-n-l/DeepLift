using NTC.Pool;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using NaughtyAttributes;
using UnityEngine.EventSystems;

public class Shooting : MonoBehaviour, IUpdateSelectedHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private bool loopShooting = true;

    [ShowIf(nameof(loopShooting))]
    [SerializeField]
    private float distanceToPlayer;

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

    private bool isPressed = false;

    private void Awake()
    {
        if (loopShooting == true)
            wait = new WaitForSeconds(cooldown);
    }

    private void Start()
    {
        if (loopShooting == true)
            StartCoroutine(ShootLoop());
    }

    private void OnEnable()
    {
        if (loopShooting == true)
            StartCoroutine(ShootLoop());
    }

    public IEnumerator ShootLoop()
    {
        yield return wait;

        if (distanceToPlayer != 0)
        {
            if (Vector2.Distance(PlayerMovement.Instance.transform.position, transform.parent.position) < distanceToPlayer)
            {
                NightPool.Spawn(bullet, shootPosition.position, shootPosition.rotation);

                OnShot?.Invoke();
            }
        }
        else
        {
            NightPool.Spawn(bullet, shootPosition.position, shootPosition.rotation);

            OnShot?.Invoke();
        }

        StartCoroutine(ShootLoop());
    }

#if UNITY_WEBGL || UNITY_EDITOR
    private void Update()
    {
        if (ActionsBy.IsCan == false || Application.isMobilePlatform == true)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            isPressed = true;

            Shoot();
        }
        else if (Input.GetMouseButton(0))
        {
            if (loopShooting == false && isPressed == true)
                Shoot();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (loopShooting == false)
                isPressed = false;
        }
    }
#endif

    public void OnPointerDown(PointerEventData eventData)
    {
        if (loopShooting == false)
        {
            isPressed = true;

            Shoot();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (loopShooting == false)
            isPressed = false;
    }

    public void OnUpdateSelected(BaseEventData eventData)
    {
        if (loopShooting == false && isPressed == true)
            Shoot();
    }

    private void Shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + cooldown;

            NightPool.Spawn(bullet, shootPosition.position, shootPosition.rotation);

            OnShot?.Invoke();
        }
    }
}