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

    private bool m_interstitialReady;
    private bool m_rewardedReady;

    public bool IsInterstitialReady => m_interstitialReady;
    public bool IsRewardedReady => m_rewardedReady;

    public void Initialize(ConsentState consent)
    {
        Debug.Log($"[LevelPlay] Initialize with consent {consent}");
    }

    public void LoadInterstitial()
    {
        Debug.Log("[LevelPlay] Load interstitial");
        m_interstitialReady = true;
        OnAdLoaded?.Invoke();
    }

    public void ShowInterstitial()
    {
        if (!m_interstitialReady)
        {
            OnAdFailed?.Invoke("Interstitial not ready");
            return;
        }
        Debug.Log("[LevelPlay] Show interstitial");
        m_interstitialReady = false;
        OnAdShown?.Invoke();
        OnAdClosed?.Invoke();
    }

    public void LoadRewarded()
    {
        Debug.Log("[LevelPlay] Load rewarded");
        m_rewardedReady = true;
        OnAdLoaded?.Invoke();
    }

    public void ShowRewarded()
    {
        if (!m_rewardedReady)
        {
            OnAdFailed?.Invoke("Rewarded not ready");
            return;
        }
        Debug.Log("[LevelPlay] Show rewarded");
        m_rewardedReady = false;
        OnAdShown?.Invoke();
        OnAdRewarded?.Invoke();
        OnAdClosed?.Invoke();
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
