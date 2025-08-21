using System;
using UnityEngine;

/// <summary>
/// Stub implementation for Unity Mediation adapter.
/// </summary>
public class UnityMediationAdapter : IAdNetworkAdapter
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
        Debug.Log($"[UnityMediation] Initialize with consent {consent}");
    }

    public void LoadInterstitial()
    {
        Debug.Log("[UnityMediation] Load interstitial");
    }

    public void ShowInterstitial()
    {
        Debug.Log("[UnityMediation] Show interstitial");
    }

    public void LoadRewarded()
    {
        Debug.Log("[UnityMediation] Load rewarded");
    }

    public void ShowRewarded()
    {
        Debug.Log("[UnityMediation] Show rewarded");
    }

    public void ShowBanner()
    {
        Debug.Log("[UnityMediation] Show banner");
    }

    public void HideBanner()
    {
        Debug.Log("[UnityMediation] Hide banner");
    }
}
