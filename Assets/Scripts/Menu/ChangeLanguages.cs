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

        if (PlayerPrefs.HasKey(nameKey) == true)
            ChangeLang(PlayerPrefs.GetInt(nameKey, 0));
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
        LangsList.SetLanguage(value, true);

        dropdown.value = value;

        PlayerPrefs.SetInt(nameKey, value);
    }
}