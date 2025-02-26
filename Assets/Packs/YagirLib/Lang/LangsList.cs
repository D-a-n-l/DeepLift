﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WordKey : Word
{
    public string key = "";
    public bool hide = true;
}

[System.Serializable]
public class Word
{
    public List<LangPhrase> phrases = new List<LangPhrase>();
    public Word()
    {

        try
        {
            for (int i = 0; i < LangsList.langs.translates.languages.Count; i++)
            {
                phrases.Add(new LangPhrase() { langName = LangsList.langs.translates.languages[i], phrase = "" });
            }
        }
        catch (System.Exception)
        {
        }
    }
    public Word(WordKey wordKey)
    {
        for (int i = 0; i < wordKey.phrases.Count; i++)
        {
            phrases.Add(wordKey.phrases[i]);
        }
    }
}

[System.Serializable]
public class LangPhrase
{
    public string langName;
    public string phrase;
}

[ExecuteInEditMode]
public class LangsList : MonoBehaviour
{
    public static LangsList langs;
    public static int currLang = 0;
    public LangListObjects translates;

    public static Dictionary<string, Word> dictionary = new Dictionary<string, Word>();
    public List<TextTranslator> activatedTexts;


    /// <summary>
    /// Set language id (Use in Awake to start init)
    /// </summary>
    /// <param name="id">Language id 0-n</param>
    public static void SetLanguage(int id)
    {
        currLang = id;
    }
    /// <summary>
    /// Set language id (Use in Awake to start init) with all active TextTranslators refresh
    /// </summary>
    /// <param name="id">Language id 0-n</param>
    /// <param name="retranslate">Refresh all initialized texts</param>
    public static void SetLanguage(int id, bool retranslate)
    {
        currLang = id;
        foreach (var item in langs.activatedTexts)
        {
            item.ReTranslate();
        } 
    }

    private void Awake()
    {
        if (FindObjectsOfType<LangsList>().ToList().Find(x => x.gameObject != gameObject) != null)
        {
            Destroy(gameObject);
            return;
        }
        langs = this;
        dictionary = new Dictionary<string, Word>();
        if (translates != null)
        {
            for (int i = 0; i < translates.words.Count; i++)
            {
                try
                {
                    dictionary.Add(translates.words[i].key, new Word(translates.words[i]));
                }
                catch (System.Exception)
                {
                    
                }
            }
        }
        else
        {
            print("Set Translation Asset!");
        }

        if (PlayerPrefs.HasKey("Language") == false)
        {
            if (Application.systemLanguage == SystemLanguage.Russian)
            {
                SetLanguage(1, true);

                PlayerPrefs.SetInt("Language", 1);
            }
            else
            {
                SetLanguage(0, true);

                PlayerPrefs.SetInt("Language", 0);
            }
        }
        else
            SetLanguage(PlayerPrefs.GetInt("Language"), true);
    }

    public static string GetWord(string key)
    {
        try
        {
            return dictionary[key].phrases[currLang].phrase;
        }
        catch (System.Exception)
        {
            Debug.LogError("YagirLib: Word \"" + key + "\" not found in list");
            return key;
        }
    }
}