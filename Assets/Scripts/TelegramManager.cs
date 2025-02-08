using SimpleFirebaseUnity;
using System.Runtime.InteropServices;
using System;
using UnityEngine;
using System.Web;

public class TelegramManager : MonoBehaviour
{
    public static TelegramManager Instance;

    [SerializeField]
    private ChangeLevelManager levelManager;

    private SimpleFirebaseUnity.Firebase database;

    private string tgUsers = "tg_users";

    private int idCurrentUser;

    private string usernameCurrentUser;

    private int levelCurrentUser;

    [DllImport("__Internal")]
    private static extern int GetTelegramUserId();

    string GetUsernameFromUrl()
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

    private void Awake()
    {
        Instance = this;

        database = SimpleFirebaseUnity.Firebase.CreateNew("https://webdeeplift-default-rtdb.europe-west1.firebasedatabase.app/", "AIzaSyBBXnBzxqUZ_H1sHF4fX34Mcm_e27bv0GY");

        Subscription();

        InitTgUser();

        DontDestroyOnLoad(this);
    }

    private void InitTgUser()
    {
        //int id_user = GetTelegramUserId();
        idCurrentUser = GetTelegramUserId();
        usernameCurrentUser = GetUsernameFromUrl();
        database.Child($"{tgUsers}/{idCurrentUser}", true).GetValue();

        //TelegramUser user = new TelegramUser(id_user, 0);

        //string userJson = JsonUtility.ToJson(user);

        //database.Child($"{tg_users}/{id_user}").SetValue(userJson, true);
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
        database.Child($"{tgUsers}/{idCurrentUser}/level").SetValue(level);
    }

    public int GetLevel()
    {
        database.Child($"{tgUsers}/{idCurrentUser}/level").GetValue();

        print("Get level " + levelCurrentUser);
        return levelCurrentUser;
    }

    private void GetOKHandler(SimpleFirebaseUnity.Firebase sender, DataSnapshot snapshot)
    {
        TelegramUser user = new TelegramUser(0, "", 0);

        try
        {
            user = JsonUtility.FromJson<TelegramUser>(snapshot.RawJson);
        }
        catch
        {
            user = new TelegramUser(idCurrentUser, usernameCurrentUser,1);

            string userJson = JsonUtility.ToJson(user);

            database.Child($"{tgUsers}/{idCurrentUser}").SetValue(userJson, true);
        }

        levelManager.UnlockLevels(user.level);

        levelCurrentUser = user.level;
        print("OK Handler " +levelCurrentUser);
    }

    private void GetFailHandler(SimpleFirebaseUnity.Firebase sender, FirebaseError err)
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