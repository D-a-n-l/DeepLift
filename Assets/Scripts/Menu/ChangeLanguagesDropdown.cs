using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Dropdown))]
public class ChangeLanguagesDropdown : MonoBehaviour
{
    [SerializeField]
    private string nameKey;

    private TMP_Dropdown dropdown;

    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();

        if (PlayerPrefs.HasKey(nameKey) == true)
            dropdown.value = PlayerPrefs.GetInt(nameKey, 0);
        else
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
                ChangeLang(1);
            else
                ChangeLang(0);
        }
    }

    public void ChangeLang(int value)
    {
        LangsList.SetLanguage(dropdown.value, true);

        PlayerPrefs.SetInt(nameKey, value);
    }
}