using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class TransitionLevel : MonoBehaviour
{
    [SerializeField]
    private Canvas canvasTransitionNextLevel;

    [Header("Triggers")]
    [SerializeField]
    private string nameNextLevel;

    [SerializeField]
    private string namePastLevel;

    private Animator animator;

    private int currentLevel;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void LoadLevel(bool isNext)
    {
        canvasTransitionNextLevel.enabled = true;

        if (isNext == true)
        {
            animator.SetTrigger(nameNextLevel);

            currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
        }
        else
        {
            animator.SetTrigger(namePastLevel);

            currentLevel = SceneManager.GetActiveScene().buildIndex - 1;
        }
    }

    public void LoadLevelChoice(int level, float delay = 0)
    {
        StartCoroutine(LevelChoice(level, delay));
    }

    private IEnumerator LevelChoice(int level, float delay = 0)
    {
        yield return new WaitForSeconds(delay);

        canvasTransitionNextLevel.enabled = true;

        animator.SetTrigger(nameNextLevel);

        currentLevel = level;
    }

    public void EndAnimation()
    {
        SceneManager.LoadScene(currentLevel);

        if (currentLevel < PlayerPrefs.GetInt("scene"))
            PlayerPrefs.SetInt("scene", currentLevel);
    }

    //public void TransitionNext(int isGoMenu)
    //{
    //    if (SceneManager.GetActiveScene().name == "Floor10")
    //    {
    //        completeGame.enabled = true;
    //    }
    //    else
    //    {
    //        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    //        if (isGoMenu == 0)
    //            SceneManager.LoadScene(0);
    //        else
    //        {
    //            SceneManager.LoadScene(currentSceneIndex + 1);

    //            PlayerPrefs.SetInt("scene", currentSceneIndex + 1);
    //        }
    //    }
    //}
}