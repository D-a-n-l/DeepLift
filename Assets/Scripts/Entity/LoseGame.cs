using UnityEngine;

public class LoseGame : MonoBehaviour
{
    public static LoseGame Instance;

    [SerializeField]
    private RewardedAdsButton rewardedAdsButton;

    [Space(5)]
    [SerializeField]
    private HealthControl player;

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
        if (Instance != null)
        {
            Debug.LogError("More than one Player");
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        player.OnDead.AddListener(EnableMenu);
    }

    private void EnableMenu()
    {
        ChangeTime.Set(0);

        rewardedAdsButton.LoadAd();

        switchOffColliders.Set(false);

        canvas.enabled = true;

        backgorund.enabled = true;
    }

    public void Continue()
    {
        ChangeTime.Set(1);

        canvas.enabled = false;

        backgorund.enabled = false;

        transitionLevel.LoadLevel(false);
    }

    public void SecondLife()
    {
        ChangeTime.Set(1);

        canvas.enabled = false;

        backgorund.enabled = false;

        StartCoroutine(switchOffColliders.SetWithDelay(true, delay));

        player.GetHeal(player.MaxHealth);
    }
}