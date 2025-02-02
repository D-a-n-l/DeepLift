using SimpleFirebaseUnity;
using System.Runtime.InteropServices;
using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using NaughtyAttributes;
using Models;

public class TelegramManager : MonoBehaviour
{
    private Firebase _database;

    [DllImport("__Internal")]
    private static extern int GetTelegramUserId();

    private void Awake()
    {
        _database = Firebase.CreateNew("https://webdeeplift-default-rtdb.europe-west1.firebasedatabase.app/", "AIzaSyBBXnBzxqUZ_H1sHF4fX34Mcm_e27bv0GY");

        _database.OnGetSuccess += GetOKHandler;
        _database.OnGetFailed += GetFailHandler;

        TelegramUser user = new TelegramUser(452357, 1);
        string userJson = JsonUtility.ToJson(user);
        _database.Child($"users/{452357}", true).GetValue();
        //Debug.Log($"Пользователь: {user.id}");
        //_database.Child($"users/{user.id}").SetValue(userJson, true);
    }

    [Button]
    public void Gettt()
    {
        _database.Child($"users/{452357}", true).GetValue();
    }

    void GetFailHandler(Firebase sender, FirebaseError err)
    {
        Debug.Log("[ERR] Get from key: <" + sender.FullKey + ">,  " + err.Message + " (" + (int)err.Status + ")");
    }

    void GetOKHandler(Firebase sender, DataSnapshot snapshot)
    {
        Debug.Log("[OK] Get from key: <" + sender.FullKey + ">");
        Debug.Log("[OK] Raw Json: " + snapshot.RawJson);
        TelegramUser user = JsonUtility.FromJson<TelegramUser>(snapshot.RawJson);
        ChangeLevelManager cha = FindAnyObjectByType<ChangeLevelManager>();
        cha.unlockLevels = user.level;
        Dictionary<string, object> dict = snapshot.Value<Dictionary<string, object>>();
        List<string> keys = snapshot.Keys;

        if (keys != null)
            foreach (string key in keys)
            {
                Debug.Log(key + " = " + dict[key].ToString());
            }
    }

    //private IEnumerator AAA()
    //{
    //    yield return new WaitForSeconds(5f);
    //    string userDataJson = GetTelegramUserId().ToString();
    //    Debug.Log("Получены данные из Telegram: " + userDataJson);
    //    //userDataJson = "Loh";

    //    // Парсим JSON
    //    TelegramUser user = new TelegramUser(userDataJson, "0");
    //    Debug.Log($"Пользователь: {userDataJson}");
    //    _database.Child("users").Push(userDataJson);
    //}

    // Метод вызывается из JavaScript
    public void DataReceived(string firstName)
    {
        //_database = Firebase.CreateNew("https://webdeeplift-default-rtdb.europe-west1.firebasedatabase.app/", "AIzaSyBBXnBzxqUZ_H1sHF4fX34Mcm_e27bv0GY");

        //TelegramUser user = JsonUtility.FromJson<TelegramUser>(jsonData);

        //string json = JsonUtility.ToJson(user);
        Debug.Log(firstName);
        //Debug.Log($"User ID: {user.id}, Username: {user.username}, First Name: {user.first_name}");

        _database.Child("users").Push(firstName);
    }
}

[Serializable]
public class TelegramUser
{
    public int id;
    public int level;

    public TelegramUser(int id, int level)
    {
        this.id = id;
        this.level = level;
    }
}