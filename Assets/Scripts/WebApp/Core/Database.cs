using SimpleFirebaseUnity;
using System.Runtime.InteropServices;
using System;
using UnityEngine;
using System.Web;
using Newtonsoft.Json;
using System.Collections.Generic;

public class Database : MonoBehaviour
{
    public static Database Instance;

    [SerializeField]
    private string url;

    [SerializeField]
    private ChangeLevelManager levelManager;

    private Firebase database;

    private string tgUsers = "tg_users";

    private int idCurrentUser;

    private string usernameCurrentUser;

    private int levelCurrentUser;

    private bool isHaveUser = false;

    [DllImport("__Internal")]
    private static extern int GetTelegramUserId();

    private void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(this);

        StartInit();
    }

    public void StartInit()
    {
        database = Firebase.CreateNew(url);

        Subscription();

        InitUser();
    }

    private void InitUser()
    {
        idCurrentUser = GetTelegramUserId();

        usernameCurrentUser = GetUsernameFromUrl();

        database.Child($"{tgUsers}", true).GetValue();
    }

    private string GetUsernameFromUrl()
    {
        string url = Application.absoluteURL;

        Uri uri = new Uri(url);

        string query = uri.Query;

        if (!string.IsNullOrEmpty(query))
        {
            var parameters = HttpUtility.ParseQueryString(query);

            return parameters["username"];
        }

        return null;
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
        level--;

        database.Child($"{tgUsers}/{idCurrentUser}/level").SetValue(level);
    }

    public int GetLevel()
    {
        database.Child($"{tgUsers}/{idCurrentUser}/level").GetValue();

        return levelCurrentUser;
    }

    private void GetOKHandler(Firebase sender, DataSnapshot snapshot)
    {
        User user = new User(0, 1, "");

        try
        {
            Dictionary<string, User> data = JsonConvert.DeserializeObject<Dictionary<string, User>>(snapshot.RawJson);

            user = JsonUtility.FromJson<User>(snapshot.RawJson);

            foreach (var entry in data)
            {
                if (entry.Value.id == idCurrentUser || entry.Value.username == usernameCurrentUser)
                {
                    if (entry.Value.id != idCurrentUser)
                        database.Child($"{tgUsers}/{entry.Value.id}").Delete();

                    if (entry.Value.username != usernameCurrentUser)
                        database.Child($"{tgUsers}/{entry.Value.username}").Delete();

                    user = new User(idCurrentUser, entry.Value.level, usernameCurrentUser);

                    string userJson = JsonUtility.ToJson(user);

                    database.Child($"{tgUsers}/{idCurrentUser}").SetValue(userJson, true);

                    isHaveUser = true;

                    break;
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        if (isHaveUser == false)
        {
            user = new User(idCurrentUser, 1, usernameCurrentUser);

            string userJson = JsonUtility.ToJson(user);

            database.Child($"{tgUsers}/{idCurrentUser}").SetValue(userJson, true);
        }

        levelManager.UnlockLevels(user.level);

        levelCurrentUser = user.level;
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

    public int level;

    public string username;

    public User(int id, int level, string username)
    {
        this.id = id;

        this.level = level;

        this.username = username;
    }
}