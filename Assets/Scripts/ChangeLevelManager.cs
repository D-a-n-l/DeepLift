using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLevelManager : MonoBehaviour
{
    [Header("Кнопки")]
    [SerializeField] private Button[] buttons;
    [Header("Анимация перехода на уровень")]
    [SerializeField] private Animator animTransitionNextLvl;
    [SerializeField] private GameObject gameObjectTransitionNextLvl;
    [Header("Затенение меню")]
    [SerializeField] private Animator animTransperent;
    [SerializeField] private GameObject panelTransperent;
    private int _unlockLevel;

    private void Start()
    {
        _unlockLevel = PlayerPrefs.GetInt("scene", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        for (int i = 0; i < _unlockLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void ChoiceLevel(int lvl)
    {
        StartCoroutine(GameStart(lvl));
    }

    private IEnumerator GameStart(int lvl)
    {
        yield return new WaitForSecondsRealtime(.5f);
        panelTransperent.SetActive(true);
        animTransperent.SetTrigger("next");
        yield return new WaitForSecondsRealtime(1.1f);
        gameObjectTransitionNextLvl.SetActive(true);
        animTransitionNextLvl.SetTrigger("next");
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(lvl);
    }
}