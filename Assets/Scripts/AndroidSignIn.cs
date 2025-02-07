using Assets.PlayId.Examples;
using Assets.PlayId.Scripts.Enums;
using Assets.PlayId.Scripts;
using UnityEngine;
using Assets.PlayId.Scripts.Data;

public class AndroidSignIn : MonoBehaviour
{
    void OnSignIn(bool success, string error, User user)
    {
        Debug.Log(success ? $"Hello, {user.Name}!" : error);

        if (success)
        {
            var jwt = new JWT(user.TokenResponse.IdToken);

            jwt.ValidateSignature(PlayIdServices.Instance.Auth.SavedUser.ClientId);

            Debug.Log("\nId Token (JWT) validated.");
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