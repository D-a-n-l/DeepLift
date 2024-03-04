using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

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

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one Player");
            return;
        }

        instance = this;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        rigidbod = GetComponent<Rigidbody2D>();

        animationInterpolation = 1f;
    }

    private void FixedUpdate()
    {
        rigidbod.velocity = new Vector2(moveX * speed * Time.fixedDeltaTime, moveY * speed * Time.fixedDeltaTime);

        if (moveX > 0 && !facingRight) Flip();
        else if (moveX < 0 && facingRight) Flip();
    }

    private void Update()
    {
        moveX = joystick.Horizontal;
        moveY = joystick.Vertical;

        animator.SetFloat("x", moveX * animationInterpolation);
        animator.SetFloat("y", moveY * animationInterpolation);
    }

    private void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    public void SetSpeed(float value) => speed = value;

    //private void OnEnable()
    //{
    //    _rewardedAd = new RewardedAd(rewardedId);
    //    AdRequest adRequest = new AdRequest.Builder().Build();
    //    _rewardedAd.LoadAd(adRequest);
    //    _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
    //}

    //private void HandleUserEarnedReward(object sender, Reward e)
    //{
    //    //HealthControl.instance.GetHeal(1);
    //    Time.timeScale = 1;
    //    //secondLife.enabled = false;
    //}

    //public void ShowAd()
    //{
    //    if (_rewardedAd.IsLoaded())
    //        _rewardedAd.Show();
    //}
}