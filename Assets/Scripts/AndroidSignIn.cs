using Assets.PlayId.Examples;
using Assets.PlayId.Scripts.Enums;
using Assets.PlayId.Scripts;
using UnityEngine;
using Assets.PlayId.Scripts.Data;
using UnityEngine.SceneManagement;
using NaughtyAttributes;
using UnityEngine.Events;

public class AndroidSignIn : MonoBehaviour
{
    [SerializeField]
    private string nameSceneBeforeAuth;

    [SerializeField]
    public UnityEvent OnSignedInGoogle;

    [SerializeField]
    public UnityEvent OnSignedInTg;

    [SerializeField]
    public UnityEvent OnSignedInVK;

    [SerializeField]
    public UnityEvent OnSignedInAnon;

    private string platform;

    private void Awake()
    {
        if (PlayIdServices.Instance.Auth.SavedUser != null || PlayerPrefs.GetInt("Anon") == 1)
            SceneManager.LoadSceneAsync(nameSceneBeforeAuth);
    }

    [Button]
    public void SignOut()
    {
        PlayIdServices.Instance.Auth.SignOut(revokeAccessToken: false);

        if (PlayerPrefs.HasKey("Anon") == true)
            PlayerPrefs.DeleteKey("Anon");
    }

    private void OnSignIn(bool success, string error, Assets.PlayId.Scripts.Data.User user)
    {
        if (success)
        {
            var jwt = new JWT(user.TokenResponse.IdToken);

            jwt.ValidateSignature(PlayIdServices.Instance.Auth.SavedUser.ClientId);

            if (platform == Platform.Google.ToString())
            {
                Database.Instance.InitGoogleUser();

                OnSignedInGoogle?.Invoke();
            }
            else if (platform == Platform.Telegram.ToString())
            {
                Database.Instance.InitTgUser();

                OnSignedInTg?.Invoke();
            }
            else if (platform == Platform.VK.ToString())//idk VK == Discord
            {
                Database.Instance.InitVKUser();

                OnSignedInVK?.Invoke();
            }
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

    public void SignInAnon()
    {
        PlayerPrefs.SetInt("Anon", 1);

        OnSignedInAnon?.Invoke();
    }
}