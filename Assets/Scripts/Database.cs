using SimpleFirebaseUnity;
using System;
using UnityEngine;
using System.Collections.Generic;
using Assets.PlayId.Scripts;
using Newtonsoft.Json;

public class Database : MonoBehaviour
{
    public static Database Instance;

    private Firebase database;

    public int Level => level;

    private int level;

    private int id;

    private bool isHaveUser = false;

    private string users = "";

    private const string tgUsers = "tg_users";

    private const string googleUsers = "google_users";

    private const string vkUsers = "vk_users";

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(this);

        database = Firebase.CreateNew("https://webdeeplift-default-rtdb.europe-west1.firebasedatabase.app/", "AIzaSyBBXnBzxqUZ_H1sHF4fX34Mcm_e27bv0GY");

        Subscription();

        if (PlayIdServices.Instance.Auth.SavedUser != null || PlayerPrefs.HasKey(DataBasePlayerPrefs.ANON))
            InitAuth();
    }

    public void InitAuth()
    {
        if (PlayerPrefs.GetInt(DataBasePlayerPrefs.GOOGLE) == 1)
            InitGoogleUser();
        else if (PlayerPrefs.GetInt(DataBasePlayerPrefs.TG) == 1)
            InitTgUser();
        else if (PlayerPrefs.GetInt(DataBasePlayerPrefs.VK) == 1)
            InitVKUser();
        else
            GetLevel();
    }

    public void InitGoogleUser()
    {
        database.Child($"{googleUsers}", true).GetValue();

        users = googleUsers;
    }

    public void InitTgUser()
    {
        database.Child($"{tgUsers}", true).GetValue();

        users = tgUsers;
    }

    public void InitVKUser()
    {
        database.Child($"{vkUsers}", true).GetValue();

        users = vkUsers;
    }

    private void OnDisable()
    {
        Unsubscription();
    }

    private void Subscription()
    {
        database.OnGetSuccess += GetOKHandler;
        database.OnGetFailed += GetFailHandler;
    }

    private void Unsubscription()
    {
        database.OnGetSuccess -= GetOKHandler;
        database.OnGetFailed -= GetFailHandler;
    }

    public void SaveLevel(int level)
    {
        level--;//because LevelManager

        if (PlayerPrefs.HasKey(DataBasePlayerPrefs.ANON))
            PlayerPrefs.SetInt(DataBasePlayerPrefs.ANON_LEVEL, level);
        else
            database.Child($"{users}/{id}/level").SetValue(level);
    }

    public int GetLevel()
    {
        if (PlayerPrefs.HasKey(DataBasePlayerPrefs.ANON))
            return level = PlayerPrefs.GetInt(DataBasePlayerPrefs.ANON_LEVEL, level);
        else
            database.Child($"{users}/{id}/level").GetValue();

        return level;
    }

    private void GetOKHandler(SimpleFirebaseUnity.Firebase sender, DataSnapshot snapshot)
    {
        User user = new User(0, "", 0);

        string currentUsername = PlayIdServices.Instance.Auth.SavedUser.Email;

        if (PlayIdServices.Instance.Auth.SavedUser.Platforms == Assets.PlayId.Scripts.Enums.Platform.Telegram)
            currentUsername = PlayIdServices.Instance.Auth.SavedUser.Email.Remove(0, 1);

        try
        {
            Dictionary<string, User> data = JsonConvert.DeserializeObject<Dictionary<string, User>>(snapshot.RawJson);

            foreach (var entry in data)
            {
                if (entry.Value.username == currentUsername)
                {
                    user = new User(entry.Value.id, entry.Value.username, entry.Value.level);

                    isHaveUser = true;

                    break;
                }
            }
        }
        catch
        {
            if (isHaveUser == false)
            {
                user = new User(PlayIdServices.Instance.Auth.SavedUser.Id, currentUsername, 1);

                string userJson = JsonUtility.ToJson(user);

                database.Child($"{users}/{PlayIdServices.Instance.Auth.SavedUser.Id}").SetValue(userJson, true);
            }
        }

        id = user.id;

        level = user.level;
    }

    private void GetFailHandler(Firebase sender, FirebaseError err)
    {
        Debug.Log("[ERR] Get from key: <" + sender.FullKey + ">,  " + err.Message + " (" + (int)err.Status + ")");
    }
}

[Serializable]
public class User
{
    public int id;
    public string username;
    public int level;

    public User(int id, string username, int level)
    {
        this.id = id;
        this.username = username;
        this.level = level;
    }
}