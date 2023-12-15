using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class JohnWickController : MonoBehaviour
{
    [Header("Настройки Джона")]
    [SerializeField] private float speed;

    [SerializeField, Tooltip("Пистолет/Ак"), Space(5)] private bool isAk;

    [SerializeField, Tooltip("Есть ли фонарик"), Space(5)] private bool isFlashlight;

    [SerializeField, Tooltip("Джойстик управления"), Space(5)] private VariableJoystick joystick;

    private Rigidbody2D rigibody;

    private Animator animator;

    [HideInInspector] public float health = 1;

    [Header("Настройки рук Джона")]
    [SerializeField] private GameObject hand;
    [SerializeField] private Animator animatorHand;

    [SerializeField, Space(10)] private float xHand;
    [SerializeField] private float yHand;

    [Header("Объект для отображения получения урона")]
    [SerializeField, Tooltip("Канвас, на котором анимация ниже")] GameObject blood;

    [Header("Анимация получения урона")]
    [SerializeField] private Animator animBlood1;
    [SerializeField] private Animator animBlood2;
    [SerializeField] private Animator animBlood3;
    [SerializeField] private Animator animBlood4;

    [Header("Тряска камеры")]
    [SerializeField] private Camera mainCam;

    [SerializeField] private CinemachineVirtualCamera vCamera;

    [SerializeField, Tooltip("Сила тряски"), Space(10)] private float amplitude;
    [SerializeField, Tooltip("Частота тряски")] private float frequency;

    [Header("Выстрел")]
    [SerializeField, Tooltip("Время перезарядки")] private float cooldownTime = 0;

    private float nextFire = 0;

    [SerializeField, Tooltip("Кнопка выстрела"), Space(10)] private GameObject attackButton;

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

    [SerializeField, Space(10)] private GameObject secondLife;

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

    private void Start()
    {
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
        var MoveX = joystick.Horizontal;
        var MoveY = joystick.Vertical;

        rigibody.velocity = new Vector2(MoveX * speed * Time.fixedDeltaTime, MoveY * speed * Time.fixedDeltaTime);

        animationInterpolation = Mathf.Lerp(animationInterpolation, 1.5f, Time.deltaTime * 3);
        animator.SetFloat("x", MoveX * animationInterpolation);
        animator.SetFloat("y", MoveY * animationInterpolation);

        animatorHand.SetFloat("x", MoveX * animationInterpolation);
        animatorHand.SetFloat("y", MoveY * animationInterpolation);


        if (MoveX > 0 && !facingRight) { Flip(); }
        else if (MoveX < 0 && facingRight) { Flip(); }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        xHand *= -1;
        hand.transform.Rotate(0f, 180f, 0f);
        transform.Rotate(0f, 180f, 0f);
    }

    private void Update()
    {
        hand.transform.position = new Vector2(transform.position.x + xHand, transform.position.y + yHand) ;

        var MoveX = joystick.Horizontal;

        if (MoveX > 0)
        {
            MoveRefactoring(bulletEffectLeft, -90);
        }
        else if (MoveX < 0)
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
        if (col.CompareTag("EnemyBullet")) { StartCoroutine(setBlood()); }
        if (col.CompareTag("Enemy") || col.CompareTag("Shield")) { health -= .25f; StartCoroutine(setBlood()); }
    }

    public IEnumerator setBlood()
    {
        Handheld.Vibrate();
        blood.SetActive(true);
        animBlood1.SetTrigger("blood");
        animBlood2.SetTrigger("blood");
        animBlood3.SetTrigger("blood");
        animBlood4.SetTrigger("blood");
        StartCoroutine(Shake());
        yield return new WaitForSeconds(.4f);
        StopCoroutine(Shake());
        blood.SetActive(false);
        if (health <= 0) { Time.timeScale = 0;  secondLife.SetActive(true); }
    }

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
        secondLife.SetActive(false);
    }

    public void ShowAd()
    {
        if (_rewardedAd.IsLoaded())
            _rewardedAd.Show();
    }

    public void ContinuePlayInGame()
    {
        StartCoroutine(deadJohn());
        secondLife.SetActive(false);
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

            //animatorHand.SetTrigger("NotShoot");
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

    private IEnumerator Shake()
    {
        vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
        vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;
        yield return new WaitForSeconds(.2f);
        vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
    }
}