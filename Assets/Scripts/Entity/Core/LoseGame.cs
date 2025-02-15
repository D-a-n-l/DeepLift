using UnityEngine;

public class LoseGame : MonoBehaviour
{
    public static LoseGame Instance;

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

    private bool _isUsedSecondLife = false;

    public bool IsUsedSecondLife => _isUsedSecondLife;

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

        _isUsedSecondLife = true;

        canvas.enabled = false;

        backgorund.enabled = false;

        StartCoroutine(switchOffColliders.SetWithDelay(true, delay));

        player.GetHeal(player.MaxHealth);
    }
}