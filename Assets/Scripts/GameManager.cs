using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("�������������� � ������")]
    [SerializeField] 
    private Animator anim;

    [Header("����� �����")]
    [SerializeField] 
    private AudioSource audioLift;

    [SerializeField] 
    private AudioClip openLift;

    [SerializeField] 
    private AudioClip closeLift;

    [Header("�������� �������� �� �������")]
    [SerializeField]
    private Canvas transitionNextLvl;

    [SerializeField] 
    private Animator animTransitionNextLvl;

    [Header("����� �� ����������� ������")]
    [SerializeField]
    private float timeLvl;

    [Header("���������� ������")]
    [SerializeField] 
    private Animator JohnWick;

    [Space(10)]
    [SerializeField] 
    private GameObject LegsJohnWick;

    [SerializeField] 
    private GameObject HandJohnWick;

    private Rigidbody2D rigidBodyJohnWick;

    [Header("������")]
    [SerializeField]
    private Canvas buttons;

    [Header("������")]
    [SerializeField] 
    private GameObject completeGame;

    private void Start()
    {
        rigidBodyJohnWick = JohnWick.GetComponent<Rigidbody2D>();

        StartCoroutine(elevatorOpen());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<JohnWickController>())
        {
            StartCoroutine(elevatorClose());
        }
    }

    public void ContinueInMenu()
    {
        StartCoroutine(TransitionInMenu());
    }

    private IEnumerator elevatorOpen()
    {
        yield return new WaitForSeconds(timeLvl);

        anim.SetTrigger("LiftOpen");
        audioLift.PlayOneShot(openLift);
    }

    private IEnumerator elevatorClose()
    {
        buttons.enabled = false;

        yield return new WaitForSecondsRealtime(.1f);

        rigidBodyJohnWick.constraints = RigidbodyConstraints2D.FreezeAll;

        yield return new WaitForSecondsRealtime(.5f);

        HandJohnWick.SetActive(false);
        LegsJohnWick.SetActive(false);

        JohnWick.SetTrigger("idleLift");

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
            transitionNextLvl.enabled = true;
            animTransitionNextLvl.SetTrigger("Next");

            yield return new WaitForSecondsRealtime(2f);

            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(currentSceneIndex + 1);
            PlayerPrefs.SetInt("scene", currentSceneIndex + 1);

            Time.timeScale = 1;
        }
    }

    private IEnumerator TransitionInMenu()
    {
        transitionNextLvl.enabled = true;
        animTransitionNextLvl.SetTrigger("Next");

        yield return new WaitForSecondsRealtime(2f);

        SceneManager.LoadScene(0);

        Time.timeScale = 1;
    }
}