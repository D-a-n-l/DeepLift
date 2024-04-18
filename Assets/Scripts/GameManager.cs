using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RandomAnimations))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Min(0.1f)]
    [SerializeField]
    private float timeLevel = 5f;

    [Space(10)]
    [SerializeField]
    private BoxCollider2D outFence;

    [SerializeField]
    private Animator counter;

    [SerializeField]
    private TransitionLevel transitionLevel;

    [Header("Triggers")]
    [SerializeField]
    private string namePlayerLift;

    [SerializeField]
    private string nameLiftOpen;

    [SerializeField]
    private string nameLiftClose;

    [SerializeField]
    private string nameCounterUp;

    [SerializeField]
    private string nameCounterDown;

    [Space(10)]
    [SerializeField]
    private Canvas buttons;

    [SerializeField]
    private PresetSpriteRenderer sprite;

    private RandomAnimations animator;

    private bool isPlayerStay = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one Player");
            return;
        }

        Instance = this;
    }

    private IEnumerator Start()
    {
        animator = GetComponent<RandomAnimations>();

        yield return new WaitForSeconds(timeLevel);

        isPlayerStay = true;

        StartAnimation(nameLiftOpen);

        isPlayerStay = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out PlayerMovement player))
        {
            player.GetComponent<Animator>().SetTrigger(namePlayerLift);

            player.SetSpeed(0f);

            player.GetComponent<SwitchOffColliders>().Set(false);

            buttons.enabled = false;

            sprite.spriteRenderer.sortingOrder = sprite.newSortingOrder;

            isPlayerStay = true;

            if (animator.Animator.GetCurrentAnimatorStateInfo(0).IsName("isOpen"))
                StartAnimation(nameLiftClose);
        }
    }

    public void Close() => transitionLevel.LoadLevel(true);

    public void SetCounter() => counter.SetBool(nameCounterUp, false);

    public void SetActiveFence() => outFence.enabled = false;

    public void StartAnimation(string name)
    {
        if (isPlayerStay == false)
            return;

        int index = Random.Range(0, animator.AnimatorOverrideControllers.Length);

        animator.SetRandomTrigger(name, index);

        if (name == nameLiftOpen)
            counter.SetBool(nameCounterUp, true);
        else
            counter.SetBool(nameCounterDown, true);
    }
}