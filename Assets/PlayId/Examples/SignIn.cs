using Assets.PlayId.Scripts;
using Assets.PlayId.Scripts.Data;
using Assets.PlayId.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.PlayId.Examples
{
    public class SignIn : MonoBehaviour
    {
        public Text Log;
        public Text Output;

        public void Start()
        {
            Application.logMessageReceived += (condition, _, _) => Log.text += condition + '\n';
            PlayIdServices.Instance.Auth.TryResume(OnSignIn);
        }

        public void DoSignIn()
        {
            PlayIdServices.Instance.Auth.SignIn(OnSignIn, caching: false);
        }

        void OnSignIn(bool success, string error, User user)
        {
            Output.text = success ? $"Hello, {user.Name}!" : error;

            if (success)
            {
                var jwt = new JWT(user.TokenResponse.IdToken);

                jwt.ValidateSignature(PlayIdServices.Instance.Auth.SavedUser.ClientId);

                Output.text += "\nId Token (JWT) validated.";
            }
        }

        public void SignOut()
        {
            PlayIdServices.Instance.Auth.SignOut(revokeAccessToken: false);
            Output.text = "Not signed in";
        }

        public void SignInWithPlatforms()
        {
            PlayIdServices.Instance.Auth.SignIn(OnSignIn, platforms: Platform.Google | Platform.Apple | Platform.Facebook);
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

        public void LinkPlatform()
        {
            if (PlayIdServices.Instance.Auth.SavedUser == null)
            {
                Output.text = "Please sign in first.";
            }
            else
            {
                PlayIdServices.Instance.Auth.Link(OnLinkPlatform);
            }

            void OnLinkPlatform(bool success, string error, User user)
            {
                Output.text = success ? $"Hello, {user.Platforms}!" : error;
            }
        }

        public void UnlinkPlatform()
        {
            var playId = new PlayIdServices();

            if (playId.Auth.SavedUser == null || !playId.Auth.SavedUser.Platforms.HasFlag(Platform.Google))
            {
                Output.text = "Please sign in with Google first.";
            }
            else
            {
                playId.Auth.Unlink(OnUnlinkPlatform, Platform.Google);
            }

            void OnUnlinkPlatform(bool success, string error, User user)
            {
                Output.text = success ? $"Hello, {user.Platforms}!" : error;
            }
        }

        public void Navigate(string url)
        {
            Application.OpenURL(url);
        }
    }
}