using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Dropdown))]
public class ChangeLanguages : MonoBehaviour
{
    private TMP_Dropdown dropdown;

    private void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();

        dropdown.value = PlayerPrefs.GetInt("Lang", 0);
    }

    public void ChangeLang(int value)
    {
        LangsList.SetLanguage(dropdown.value, true);

        PlayerPrefs.SetInt("Lang", value);
    }
}