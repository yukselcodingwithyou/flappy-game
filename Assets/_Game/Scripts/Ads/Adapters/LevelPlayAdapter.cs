using System;
using UnityEngine;

/// <summary>
/// Stub implementation for Unity LevelPlay (ironSource) adapter.
/// </summary>
public class LevelPlayAdapter : IAdNetworkAdapter
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
        Debug.Log($"[LevelPlay] Initialize with consent {consent}");
    }

    public void LoadInterstitial()
    {
        Debug.Log("[LevelPlay] Load interstitial");
    }

    public void ShowInterstitial()
    {
        Debug.Log("[LevelPlay] Show interstitial");
    }

    public void LoadRewarded()
    {
        Debug.Log("[LevelPlay] Load rewarded");
    }

    public void ShowRewarded()
    {
        Debug.Log("[LevelPlay] Show rewarded");
    }

    public void ShowBanner()
    {
        Debug.Log("[LevelPlay] Show banner");
    }

    public void HideBanner()
    {
        Debug.Log("[LevelPlay] Hide banner");
    }
}
