using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Взаимодействие с лифтом")]
    [SerializeField] private Animator anim;
    [Space(10)]
    [SerializeField] private GameObject elevatorMain;
    [SerializeField] private SpriteRenderer spriteElevatorMain;
    [Space(10)]
    [SerializeField] private GameObject elevatortOff;
    [SerializeField] private GameObject elevatortOn;

    private BoxCollider2D box;

    [Header("Звуки лифта")]
    [SerializeField] private AudioSource audioLift;
    [SerializeField] private AudioClip openLift;
    [SerializeField] private AudioClip closeLift;

    [Header("Анимация перехода на уровень")]
    [SerializeField] private Animator animTransitionNextLvl;
    [SerializeField] private GameObject gameObjectTransitionNextLvl;

    [Header("Время на прохождение уровня")]
    [SerializeField] private float timeLvl;

    [Header("Управление Джоном")]
    [SerializeField] private Animator JohnWick;
    [Space(10)]
    [SerializeField] private GameObject LegsJohnWick;
    [SerializeField] private GameObject HandJohnWick;
    [Space(10)]
    [SerializeField] private Rigidbody2D rigidBodyJohnWick;

    [Header("Кнопки")]
    [SerializeField] private GameObject[] buttons;

    [Header("Прочее")]
    [SerializeField] private GameObject completeGame;
    [SerializeField] private BoxCollider2D fence;
    private void Start()
    {
        box = GetComponent<BoxCollider2D>();
        box.enabled = false;
        StartCoroutine(elevatorOpen());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            StartCoroutine(elevatorClose());
        }
    }

    private IEnumerator elevatorOpen()
    {
        yield return new WaitForSeconds(timeLvl);
        elevatortOn.SetActive(true);
        elevatorMain.SetActive(true);
        elevatortOff.SetActive(false);
        anim.SetTrigger("LiftOpen");
        audioLift.PlayOneShot(openLift);
        yield return new WaitForSeconds(2.5f);
        box.enabled = true;
        fence.enabled = false;
    }

    private IEnumerator elevatorClose()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetActive(false);
        }
        yield return new WaitForSecondsRealtime(.1f);
        rigidBodyJohnWick.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSecondsRealtime(.5f);
        HandJohnWick.SetActive(false);
        LegsJohnWick.SetActive(false);
        JohnWick.SetTrigger("idleLift");
        spriteElevatorMain.sortingOrder = 25;
        elevatortOn.SetActive(true);
        anim.SetTrigger("LiftClose");
        yield return new WaitForSecondsRealtime(.1f);
        audioLift.PlayOneShot(closeLift);
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(3f);

        if (SceneManager.GetActiveScene().name == "Floor10")
        {
            completeGame.SetActive(true);
            //PlayerPrefs.SetInt("scene", 1);
        }

        else
        {
            gameObjectTransitionNextLvl.SetActive(true);
            animTransitionNextLvl.SetTrigger("next");
            yield return new WaitForSecondsRealtime(2f);
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
            PlayerPrefs.SetInt("scene", currentSceneIndex + 1);
            Time.timeScale = 1;
        }
    }

    public void ContinueInMenu()
    {
        StartCoroutine(TransitionInMenu());
    }

    private IEnumerator TransitionInMenu()
    {
        gameObjectTransitionNextLvl.SetActive(true);
        animTransitionNextLvl.SetTrigger("next");
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}