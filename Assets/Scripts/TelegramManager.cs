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

    private string usernameCurrentUser;

    private int levelCurrentUser;

    private bool isHaveUser = false;

    private void Awake()
    {
        Instance = this;

        database = Firebase.CreateNew("https://webdeeplift-default-rtdb.europe-west1.firebasedatabase.app/", "AIzaSyBBXnBzxqUZ_H1sHF4fX34Mcm_e27bv0GY");

        Subscription();

        InitTgUser("");

        DontDestroyOnLoad(this);
    }

    private void InitTgUser(string username)
    {
        //usernameCurrentUser = username;
        database.Child($"{tgUsers}", true).GetValue();
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
        TelegramUser user = new TelegramUser(0, "", 0);

        Dictionary<string, TelegramUser> data = JsonConvert.DeserializeObject<Dictionary<string, TelegramUser>>(snapshot.RawJson);

        string currentUsername = PlayIdServices.Instance.Auth.SavedUser.Email.Remove(0, 1);

        foreach (var entry in data)
        {
            if (entry.Value.username == currentUsername)
            {
                user = new TelegramUser(entry.Value.id, entry.Value.username, entry.Value.level);

                isHaveUser = true;
                print("YYYYYYYEEEEEEESSSSSSSS");
                break;
            }
        }

        if (isHaveUser == false)
        {
            user = new TelegramUser(PlayIdServices.Instance.Auth.SavedUser.Id, currentUsername, 1);

            string userJson = JsonUtility.ToJson(user);

            database.Child($"{tgUsers}/{PlayIdServices.Instance.Auth.SavedUser.Id}").SetValue(userJson, true);
            print("NOOOOOOOOOOOO");
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
public class TelegramUser
{
    public int id;
    public string username;
    public int level;

    public TelegramUser(int id, string username, int level)
    {
        this.id = id;
        this.username = username;
        this.level = level;
    }
}