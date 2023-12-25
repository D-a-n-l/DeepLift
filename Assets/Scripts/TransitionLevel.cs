using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionLevel : MonoBehaviour
{
    [SerializeField]
    private Canvas canvasTransitionNextLevel;

    [SerializeField]
    private Animator animatorTransitionNextLevel;

    [SerializeField]
    private Canvas completeGame;

    public void Enable()
    {
        canvasTransitionNextLevel.enabled = true;
        animatorTransitionNextLevel.SetTrigger("Next");
    }

    public void Transition(int isGoMenu)
    {
        if (SceneManager.GetActiveScene().name == "Floor10")
        {
            completeGame.enabled = true;
        }
        else
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (isGoMenu == 0)
                SceneManager.LoadScene(0);
            else
            {
                SceneManager.LoadScene(currentSceneIndex + 1);

                PlayerPrefs.SetInt("scene", currentSceneIndex + 1);
            }
        }
    }
}