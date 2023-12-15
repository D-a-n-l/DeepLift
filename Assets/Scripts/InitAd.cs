using UnityEngine;
using GoogleMobileAds.Api;

public class InitAd : MonoBehaviour
{
    private void Awake()
    {
        MobileAds.Initialize(initStatus => { });
    }
}