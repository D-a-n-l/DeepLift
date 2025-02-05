using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckOrientation : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SetLockOrientation();

    [SerializeField]
    private string _nameScene;

    private bool _isApply = false;

    private void Awake()
    {
        Check();
    }

    private void Update()
    {
        Check();
    }

    private void Check()
    {
        if (_isApply == false)
        {
            if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
                return;

            _isApply = true;

            SceneManager.LoadSceneAsync(_nameScene);

            SetLockOrientation();
        }
    }
}