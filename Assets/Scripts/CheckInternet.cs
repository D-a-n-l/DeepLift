using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CheckInternet : MonoBehaviour
{
    [SerializeField]
    private float checkInterval = 5f;

    [Space(10)]
    [SerializeField] 
    private UnityEvent OnEnabledInternet;

    [SerializeField] 
    private UnityEvent OnDisabledInternet;

    private WaitForSecondsRealtime wait;

    private bool isConnected = true;

    private void Start()
    {
        if (PlayerPrefs.GetInt(DataBasePlayerPrefs.ANON) == 1)
        {
            Destroy(gameObject);

            return;
        }

        wait = new WaitForSecondsRealtime(checkInterval);

        DontDestroyOnLoad(this);

        StartCoroutine(CheckInternetCoroutine());
    }

    private IEnumerator CheckInternetCoroutine()
    {
        while (true)
        {
            bool newConnectionState = IsInternetAvailable();

            if (newConnectionState != isConnected)
            {
                isConnected = newConnectionState;

                if (isConnected)
                {
                    ChangeTime.Set(1);

                    OnEnabledInternet?.Invoke();
                }
                else
                {
                    ChangeTime.Set(0);

                    OnDisabledInternet?.Invoke();
                }
            }

            yield return wait;
        }
    }

    private bool IsInternetAvailable()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }
}