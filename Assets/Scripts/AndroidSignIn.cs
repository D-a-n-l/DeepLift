using Assets.PlayId.Examples;
using Assets.PlayId.Scripts.Enums;
using Assets.PlayId.Scripts;
using UnityEngine;
using Assets.PlayId.Scripts.Data;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class AndroidSignIn : MonoBehaviour
{

    private void Start()
    {

        if (PlayIdServices.Instance.Auth.SavedUser != null)
        {
            Debug.Log("Platforms " + PlayIdServices.Instance.Auth.SavedUser.Platforms);
            Debug.Log("Id " + PlayIdServices.Instance.Auth.SavedUser.Id);
            Debug.Log("Email " + PlayIdServices.Instance.Auth.SavedUser.Email);
            //SceneManager.LoadScene("Menu");
        }
    }

    [Button]
    public void SignOut()
    {
        PlayIdServices.Instance.Auth.SignOut(revokeAccessToken: false);
    }

    void OnSignIn(bool success, string error, User user)
    {
        Debug.Log(success ? $"Hello, {user.Email}!" : error);

        if (PlayIdServices.Instance.Auth.SavedUser.Platforms == Platform.Telegram)
        {

        }

        if (success)
        {
            var jwt = new JWT(user.TokenResponse.IdToken);

            jwt.ValidateSignature(PlayIdServices.Instance.Auth.SavedUser.ClientId);

            Debug.Log("\nId Token (JWT) validated.");

            //SceneManager.LoadScene("Menu");
        }
    }

    public void SignInSinglePlatform()
    {
        PlayIdServices.Instance.Auth.SignIn(OnSignIn, platforms: Platform.Google);
    }

    public void SignInTg()
    {
        PlayIdServices.Instance.Auth.SignIn(OnSignIn, platforms: Platform.Telegram);
    }

    public void SignInVK()
    {
        PlayIdServices.Instance.Auth.SignIn(OnSignIn, platforms: Platform.VK);
    }
}