using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    [SerializeField]
    private float speed;

    public float Speed => speed;

    [SerializeField]
    private FixedJoystick joystick;

    private Animator animator;

    private Rigidbody2D rigidbod;

    private float moveX;

    private float moveY;

    private bool facingRight = true;

    public float animationInterpolation { get; set; }

    [HideInInspector]
    public UnityEvent OnFlip;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one Player");
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        rigidbod = GetComponent<Rigidbody2D>();

        animationInterpolation = 1f;
    }

    private void FixedUpdate()
    {
        rigidbod.linearVelocity = new Vector2(moveX * speed * Time.fixedDeltaTime, moveY * speed * Time.fixedDeltaTime);

        if (moveX > 0 && !facingRight) Flip();
        else if (moveX < 0 && facingRight) Flip();
    }

    private void Update()
    {
#if UNITY_WEBGL || UNITY_EDITOR
        if (Application.isMobilePlatform == true)
        {
            moveX = joystick.Horizontal;
            moveY = joystick.Vertical;
        }
        else
        {
            moveX = Input.GetAxis("Horizontal");
            moveY = Input.GetAxis("Vertical");
        }
#endif

#if UNITY_ANDROID
        moveX = joystick.Horizontal;
        moveY = joystick.Vertical;
#endif
        animator.SetFloat("x", moveX * animationInterpolation);
        animator.SetFloat("y", moveY * animationInterpolation);
    }

    private void Flip()
    {
        OnFlip?.Invoke();

        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    public void SetSpeed(float value) => speed = value;
}