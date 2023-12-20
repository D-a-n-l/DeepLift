using TMPro;
using UnityEngine;

public class TextTranslator : MonoBehaviour
{
    public string key;

    private void Start()
    {
        LangsList.langs.activatedTexts.Add(this);
        ReTranslate();
    }

    private void OnDestroy()
    {
        LangsList.langs.activatedTexts.Remove(this);
    }

    public void ReTranslate()
    {
        var tmpT = GetComponent<TMP_Text>();

        tmpT.text = LangsList.GetWord(key);
    }
}