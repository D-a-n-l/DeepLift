using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(RandomAnimations))]
public class TransitionLevel : MonoBehaviour
{
    [SerializeField]
    private Canvas canvasTransitionNextLevel;

    [SerializeField]
    private Animator counter;

    [Header("Triggers")]
    [SerializeField]
    private string nameAnimation;

    [SerializeField]
    private string nameCounterUp;

    [SerializeField]
    private string nameCounterDown;

    private RandomAnimations animator;

    private int currentLevel;

    private bool isJustTransition;

    private void Start()
    {
        animator = GetComponent<RandomAnimations>();
    }

    public void LoadLevel(bool isNext)
    {
        canvasTransitionNextLevel.enabled = true;

        if (isNext == true)
        {
            currentLevel = SceneManager.GetActiveScene().buildIndex + 1;

            if (currentLevel >= SceneManager.sceneCountInBuildSettings)
            {
                ChangeTime.Set(0);

                return;
            }
        }
        else
        {
            if (SceneManager.GetActiveScene().buildIndex - 1 == 1)
                currentLevel = SceneManager.GetActiveScene().buildIndex;
            else
                currentLevel = SceneManager.GetActiveScene().buildIndex - 1;
        }

        SetTrigger(isNext);
    }

    public void JustTransition()
    {
        canvasTransitionNextLevel.enabled = true;

        currentLevel = SceneManager.GetActiveScene().buildIndex + 1;

        if (currentLevel >= SceneManager.sceneCountInBuildSettings)
        {
            ChangeTime.Set(0);

            return;
        }

        SetTrigger(false);

        isJustTransition = true;
    }

    public void LoadLevelChoiceNoDelayNoAnim(int level)
    {
        SceneManager.LoadScene(level);

        if (currentLevel > Database.Instance.GetLevel())
            Database.Instance.SaveLevel(currentLevel);

        if (Time.timeScale == 0)
            ChangeTime.Set(1);
    }

    public void LoadLevelChoiceNoDelay(int level)
    {
        StartCoroutine(LevelChoice(level));
    }

    public void LoadLevelChoice(int level, float delay = 0)
    {
        StartCoroutine(LevelChoice(level, delay));
    }

    private IEnumerator LevelChoice(int level, float delay = 0)
    {
        yield return new WaitForSeconds(delay);

        canvasTransitionNextLevel.enabled = true;

        SetTrigger(false);

        currentLevel = level;
    }

    public void EndAnimation()
    {
        if (currentLevel >= SceneManager.sceneCountInBuildSettings)
            return;

        SceneManager.LoadScene(currentLevel);

        if (isJustTransition == true)
            isJustTransition = false;
        else if (currentLevel > Database.Instance.GetLevel())
            Database.Instance.SaveLevel(currentLevel);

        if (Time.timeScale == 0)
            ChangeTime.Set(1);
    }

    private void SetTrigger(bool isNext)
    {
        int index = Random.Range(0, animator.AnimatorOverrideControllers.Length);

        animator.SetRandomTrigger(nameAnimation, index);

        if (isNext == false)
            counter.SetBool(nameCounterUp, true);
        else
            counter.SetBool(nameCounterDown, true);
    }
}