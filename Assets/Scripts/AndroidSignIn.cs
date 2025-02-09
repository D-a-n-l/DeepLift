using Assets.PlayId.Examples;
using Assets.PlayId.Scripts.Enums;
using Assets.PlayId.Scripts;
using UnityEngine;
using Assets.PlayId.Scripts.Data;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class AndroidSignIn : MonoBehaviour
{

    private string platform;

    private void Start()
    {
        if (PlayIdServices.Instance.Auth.SavedUser != null || PlayerPrefs.GetInt("Anonimus") == 1)
        {
            SceneManager.LoadScene("Menu");
        }
    }

    [Button]
    public void SignOut()
    {
        PlayIdServices.Instance.Auth.SignOut(revokeAccessToken: false);
    }

    void OnSignIn(bool success, string error, Assets.PlayId.Scripts.Data.User user)
    {
        Debug.Log(success ? $"Hello, {user.Email}!" : error);

        if (success)
        {
            var jwt = new JWT(user.TokenResponse.IdToken);

            jwt.ValidateSignature(PlayIdServices.Instance.Auth.SavedUser.ClientId);

            //Debug.Log("\nId Token (JWT) validated.");

            if (platform == Platform.Google.ToString())
            {
                TelegramManager.Instance.InitGoogleUser();
            }
            else if (platform == Platform.Telegram.ToString())
            {
                TelegramManager.Instance.InitTgUser();
            }
            else if (platform == Platform.VK.ToString())//idk VK == Discord
            {
                TelegramManager.Instance.InitVKUser();
            }

            SceneManager.LoadScene("Menu");
        }
    }

    public void SignInGoogle()
    {
        PlayIdServices.Instance.Auth.SignIn(OnSignIn, platforms: Platform.Google);

        platform = Platform.Google.ToString();
    }

    public void SignInTg()
    {
        PlayIdServices.Instance.Auth.SignIn(OnSignIn, platforms: Platform.Telegram);

        platform = Platform.Telegram.ToString();
    }

    public void SignInVK()
    {
        PlayIdServices.Instance.Auth.SignIn(OnSignIn, platforms: Platform.VK);

        platform = Platform.VK.ToString();
    }

    public void SignInAnonimus()
    {
        //предупредительное окно

        PlayerPrefs.SetInt("Anonimus", 1);

        SceneManager.LoadScene("Menu");
    }
}