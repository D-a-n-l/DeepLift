using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class JohnWickController : MonoBehaviour
{
    [Header("Настройки Джона")]
    [SerializeField] 
    private float speed;

    [SerializeField, Tooltip("Пистолет/Ак"), Space(5)] 
    private bool isAk;

    [SerializeField, Tooltip("Есть ли фонарик"), Space(5)] 
    private bool isFlashlight;

    [SerializeField, Tooltip("Джойстик управления"), Space(5)] 
    private FixedJoystick joystick;

    private Rigidbody2D rigibody;

    private Animator animator;

    [HideInInspector] 
    public float health = 1;

    [Header("Настройки рук Джона")]
    [SerializeField] 
    private GameObject hand;

    [SerializeField] 
    private Animator animatorHand;

    [SerializeField, Space(10)] 
    private float xHand;
    [SerializeField] 
    private float yHand;

    [SerializeField]
    private CanvasBlood blood;

    [Header("Тряска камеры")]
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;

    private CinemachineBasicMultiChannelPerlin virtualCameraNoisePerlin;

    [Header("Выстрел")]
    [SerializeField, Tooltip("Время перезарядки")] private float cooldownTime = 0;

    private float nextFire = 0;

    [SerializeField, Tooltip("image кнопка выстрела")] private Animator imageButtonAttack;

    [Header("Позиция и эффект выстрела")]
    [SerializeField] private GameObject bullet;

    [SerializeField, Space(10)] private Transform shootPosition;
    [SerializeField] private Transform bulletEffectPosition;

    [SerializeField, Space(10)] private ParticleSystem bulletEffectLeft;
    [SerializeField] private ParticleSystem bulletEffectRight;

    private ParticleSystem bulletEffect;

    [Header("Звуки")]
    [SerializeField] private AudioSource shootJonh;
    [SerializeField] private AudioClip soundClipShot;
    [SerializeField] private AudioSource lungeJonh;

    [Header("Прочее")]
    [SerializeField] private Light flashlight;

    [SerializeField] private Animator elevator;
    [SerializeField] private GameObject elev;

    [SerializeField, Space(10)] 
    private Canvas secondLife;

    [SerializeField] private SpriteRenderer[] spriteJohn;

    private float animationInterpolation = 1f;

    private bool facingRight = true;

    private Quaternion flipShootEffect;

    private bool lockLunge = false;
    [SerializeField] private float lungeImpulse;

    [SerializeField] private float time;
    [SerializeField] private BoxCollider2D box;

    [SerializeField, Tooltip("image кнопка выстрела")] private Animator imageButtonLunge;

    //Реклама
    private string rewardedId = "ca-app-pub-3940256099942544/5224354917";

    private RewardedAd _rewardedAd;

    private float moveX;

    private float moveY;

    private void Start()
    {
        virtualCameraNoisePerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        MoveRefactoring(bulletEffectLeft, -90);

        animator = GetComponent<Animator>();
        rigibody = GetComponent<Rigidbody2D>();

        health = Mathf.Clamp(health, 0, 1);

        flipShootEffect = Quaternion.Euler(0, -90, 0);
    }

    private void FixedUpdate()
    {
        Run();
    }

    private void Run()
    {
        rigibody.velocity = new Vector2(moveX * speed * Time.fixedDeltaTime, moveY * speed * Time.fixedDeltaTime);

        if (moveX > 0 && !facingRight) { Flip(); }
        else if (moveX < 0 && facingRight) { Flip(); }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void Update()
    {
        moveX = joystick.Horizontal;
        moveY = joystick.Vertical;

        animationInterpolation = Mathf.Lerp(animationInterpolation, 1.5f, Time.deltaTime * 3);

        animator.SetFloat("x", moveX * animationInterpolation);
        animatorHand.SetFloat("x", moveX * animationInterpolation);

        if (moveX > 0)
        {
            MoveRefactoring(bulletEffectLeft, -90);
        }
        else if (moveX < 0)
        {
            MoveRefactoring(bulletEffectRight, 90);
        }
    }

    private void MoveRefactoring(ParticleSystem LeftOrRight, int Angle)
    {
        bulletEffect = LeftOrRight;
        flipShootEffect = Quaternion.Euler(0, Angle, 0);

        if (isFlashlight)
        {
            flashlight.transform.position = new Vector3(flashlight.transform.position.x, flashlight.transform.position.y, -0.08f);
        }
    }

    public void Shoot()
    {
        StartCoroutine(Shot());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.CompareTag("EnemyBullet")) { StartCoroutine(setBlood()); }
        //if (col.CompareTag("Enemy") || col.CompareTag("Shield")) { health -= .25f; StartCoroutine(setBlood()); }
    }

    public void EnableBlood()
    {
        Handheld.Vibrate();
        blood.PlayAnimation(1);
        StartCoroutine(ManagementVirtualCamera.Shake(virtualCameraNoisePerlin, 4f, 4f, .2f));
    }

    //public IEnumerator setBlood()
    //{
    //    if (health <= 0) { Time.timeScale = 0;  secondLife.SetActive(true); }
    //}

    private void OnEnable()
    {
        _rewardedAd = new RewardedAd(rewardedId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        _rewardedAd.LoadAd(adRequest);
        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
    }

    private void HandleUserEarnedReward(object sender, Reward e)
    {
        health = +1;
        Time.timeScale = 1;
        secondLife.enabled = false;
    }

    public void ShowAd()
    {
        if (_rewardedAd.IsLoaded())
            _rewardedAd.Show();
    }

    public void ContinuePlayInGame()
    {
        StartCoroutine(deadJohn());
        secondLife.enabled = true;
    }

    private IEnumerator deadJohn()
    {
        elev.SetActive(true);
        elevator.SetTrigger("dead");

        yield return new WaitForSecondsRealtime(3f);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex - 1);
        Time.timeScale = 1;
    }

    private IEnumerator Shot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + cooldownTime;

            Instantiate(bullet, shootPosition.position, transform.rotation);

            if (isAk)
            {
                shotManagment(1f, "fast");
            }
            if (!isAk)
            {
                shotManagment(1.5f, "slow");
            }
        }

        yield return null;
    }

    public void Lunge()
    {
        StartCoroutine(LungeCoroutine());
    }

    private IEnumerator LungeCoroutine()
    {
        /*if (!lockLunge)
        {
            lockLunge = true;
            Invoke("LungeLock", 2f);

            animator.SetTrigger("lunge");
            animatorHand.SetTrigger("lunge");
            lungeJonh.Play();
            imageButtonLunge.SetTrigger("Bt");
            rigibody.velocity = new Vector2(0, 0);

            if (facingRight) { rigibody.AddForce(Vector2.right * lungeImpulse); box.enabled = false; }
            else if (!facingRight) { rigibody.AddForce(Vector2.left * lungeImpulse); box.enabled = false; }

            yield return new WaitForSeconds(time);

            box.enabled = true;
        }*/

        if (!lockLunge)
        {
            lockLunge = true;
            Invoke("LungeLock", 2f);

            lungeJonh.Play();
            imageButtonLunge.SetTrigger("Bt");

            for (int i = 0; i < spriteJohn.Length; i++)
            {
                spriteJohn[i].maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            }

            rigibody.velocity = new Vector2(0, 0);

            if (facingRight) { rigibody.AddForce(Vector2.right * lungeImpulse); box.enabled = false; }
            else if (!facingRight) { rigibody.AddForce(Vector2.left * lungeImpulse); box.enabled = false; }

            yield return new WaitForSeconds(time);

            for (int i = 0; i < spriteJohn.Length; i++)
            {
                spriteJohn[i].maskInteraction = SpriteMaskInteraction.None;
            }

            box.enabled = true;
        }
    }

    private void LungeLock()
    {
        lockLunge = false;
    }

    private void shotManagment(float pitchSound, string slowOrFast)
    {
        shootJonh.pitch = pitchSound;
        shootJonh.PlayOneShot(soundClipShot);
        animatorHand.SetTrigger("Shoot");
        imageButtonAttack.SetTrigger(slowOrFast);
        Instantiate(bulletEffect, bulletEffectPosition.position, flipShootEffect);
    }
}