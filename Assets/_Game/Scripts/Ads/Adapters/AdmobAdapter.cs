using System;
using UnityEngine;

/// <summary>
/// Stub implementation of the Google AdMob adapter.
/// </summary>
public class AdmobAdapter : IAdNetworkAdapter
{
    public event Action OnAdLoaded;
    public event Action<string> OnAdFailed;
    public event Action OnAdShown;
    public event Action OnAdClosed;
    public event Action OnAdRewarded;

    public bool IsInterstitialReady => false;
    public bool IsRewardedReady => false;

    public void Initialize(ConsentState consent)
    {
        Debug.Log($"[Admob] Initialize with consent {consent}");
    }

    public void LoadInterstitial()
    {
        Debug.Log("[Admob] Load interstitial");
    }

    public void ShowInterstitial()
    {
        Debug.Log("[Admob] Show interstitial");
    }

    public void LoadRewarded()
    {
        Debug.Log("[Admob] Load rewarded");
    }

    public void ShowRewarded()
    {
        Debug.Log("[Admob] Show rewarded");
    }

    public void ShowBanner()
    {
        Debug.Log("[Admob] Show banner");
    }

    public void HideBanner()
    {
        Debug.Log("[Admob] Hide banner");
    }
}
