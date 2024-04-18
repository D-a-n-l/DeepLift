using UnityEngine;

public class LoseGame : MonoBehaviour
{
    public static LoseGame Instance { get; private set;}

    [SerializeField]
    private RewardedAdsButton rewardedAdsButton;

    [Space(5)]
    [SerializeField]
    private HealthControl player;

    public HealthControl Player => player;

    [SerializeField]
    private SwitchOffColliders switchOffColliders;

    [SerializeField]
    private float delay = 3f;

    [Space(5)]
    [SerializeField]
    private TransitionLevel transitionLevel;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private Canvas backgorund;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        player.OnDead.AddListener(() => 
        {
            Time.timeScale = 0;

            rewardedAdsButton.LoadAd();

            switchOffColliders.Set(false);

            canvas.enabled = true;

            backgorund.enabled = true;
        });
    }

    public void Continue()
    {
        Time.timeScale = 1;

        canvas.enabled = false;

        backgorund.enabled = false;

        transitionLevel.LoadLevel(false);
    }

    public void SecondLife()
    {
        Time.timeScale = 1;

        canvas.enabled = false;

        backgorund.enabled = false;

        StartCoroutine(switchOffColliders.SetWithDelay(true, delay));

        player.GetHeal(player.MaxHealth);
    }
}