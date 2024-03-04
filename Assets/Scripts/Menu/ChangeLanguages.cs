using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Dropdown))]
public class ChangeLanguages : MonoBehaviour
{
    [SerializeField]
    private string nameKey;

    private TMP_Dropdown dropdown;

    private void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();

        dropdown.value = PlayerPrefs.GetInt(nameKey, 0);
    }

    public void ChangeLang(int value)
    {
        LangsList.SetLanguage(dropdown.value, true);

        PlayerPrefs.SetInt(nameKey, value);
    }
}