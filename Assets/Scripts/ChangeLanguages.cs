using UnityEngine;
using TMPro;

public class ChangeLanguages : MonoBehaviour
{
    private TMP_Dropdown dropdown;

    private void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        dropdown.value = PlayerPrefs.GetInt("Lang", 0);
    }

    public void ChangeLang(int a)
    {
        LangsList.SetLanguage(dropdown.value, true);
        PlayerPrefs.SetInt("Lang", a);
    }
}