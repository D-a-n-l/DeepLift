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

        Subscription();
    }

    public void StartInit()
    {
        database = Firebase.CreateNew("https://webdeeplift-default-rtdb.europe-west1.firebasedatabase.app/", "AIzaSyBBXnBzxqUZ_H1sHF4fX34Mcm_e27bv0GY");

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
        User user = new User(0, "", 1);

        Dictionary<string, User> data = JsonConvert.DeserializeObject<Dictionary<string, User>>(snapshot.RawJson);

        try
        {
            user = JsonUtility.FromJson<User>(snapshot.RawJson);

            foreach (var entry in data)
            {
                if (entry.Value.username == usernameCurrentUser)
                {
                    user = new User(idCurrentUser, entry.Value.username, entry.Value.level);

                    if (entry.Value.id != idCurrentUser)
                    {
                        database.Child($"{tgUsers}/{entry.Value.id}").Delete();

                        string userJson = JsonUtility.ToJson(user);

                        database.Child($"{tgUsers}/{idCurrentUser}").SetValue(userJson, true);

                        isHaveUser = true;

                        break;
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

        if (isHaveUser == false)
        {
            user = new User(idCurrentUser, usernameCurrentUser, 1);

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

    public string username;

    public int level;

    public User(int id, string username, int level)
    {
        this.id = id;

        this.username = username;

        this.level = level;
    }
}