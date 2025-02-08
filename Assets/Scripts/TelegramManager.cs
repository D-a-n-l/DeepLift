using SimpleFirebaseUnity;
using System.Runtime.InteropServices;
using System;
using UnityEngine;
using System.Collections.Generic;
using Assets.PlayId.Scripts;
using Newtonsoft.Json;

public class TelegramManager : MonoBehaviour
{
    public static TelegramManager Instance;

    [SerializeField]
    private ChangeLevelManager levelManager;

    private Firebase database;

    private string tgUsers = "tg_users";

    private string googleUsers = "google_users";

    private string vkUsers = "vk_users";

    private string usernameCurrentUser;

    private int levelCurrentUser;

    private bool isHaveUser = false;

    private void Awake()
    {
        Instance = this;

        database = Firebase.CreateNew("https://webdeeplift-default-rtdb.europe-west1.firebasedatabase.app/", "AIzaSyBBXnBzxqUZ_H1sHF4fX34Mcm_e27bv0GY");

        Subscription();

        DontDestroyOnLoad(this);
    }

    public void InitTgUser()
    {
        //usernameCurrentUser = username;
        database.Child($"{tgUsers}", true).GetValue();
    }

    public void InitGoogleUser()
    {
        //usernameCurrentUser = username;

        database.Child($"{googleUsers}", true).GetValue();
    }

    public void InitVKUser()
    {
        //usernameCurrentUser = username;
        database.Child($"{vkUsers}", true).GetValue();
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
        database.Child($"{tgUsers}/{usernameCurrentUser}/level").SetValue(level);
    }

    public int GetLevel()
    {
        database.Child($"{tgUsers}/{usernameCurrentUser}/level").GetValue();

        print("Get level " + levelCurrentUser);
        return levelCurrentUser;
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
                    print("YYYYYYYEEEEEEESSSSSSSS");
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

                string users = "";

                if (PlayIdServices.Instance.Auth.SavedUser.Platforms == Assets.PlayId.Scripts.Enums.Platform.Google)
                    users = googleUsers;
                else if (PlayIdServices.Instance.Auth.SavedUser.Platforms == Assets.PlayId.Scripts.Enums.Platform.Telegram)
                    users = tgUsers;
                else if (PlayIdServices.Instance.Auth.SavedUser.Platforms == Assets.PlayId.Scripts.Enums.Platform.VK)
                    users = vkUsers;

                database.Child($"{users}/{PlayIdServices.Instance.Auth.SavedUser.Id}").SetValue(userJson, true);
            }
        }

        //levelManager.UnlockLevels(user.level);

        //levelCurrentUser = user.level;
        //print("OK Handler " + levelCurrentUser);
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