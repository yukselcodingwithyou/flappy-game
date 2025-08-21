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

    private bool m_interstitialReady;
    private bool m_rewardedReady;

    public bool IsInterstitialReady => m_interstitialReady;
    public bool IsRewardedReady => m_rewardedReady;

    public void Initialize(ConsentState consent)
    {
        Debug.Log($"[UnityMediation] Initialize with consent {consent}");
    }

    public void LoadInterstitial()
    {
        Debug.Log("[UnityMediation] Load interstitial");
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
        Debug.Log("[UnityMediation] Show interstitial");
        m_interstitialReady = false;
        OnAdShown?.Invoke();
        OnAdClosed?.Invoke();
    }

    public void LoadRewarded()
    {
        Debug.Log("[UnityMediation] Load rewarded");
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
        Debug.Log("[UnityMediation] Show rewarded");
        m_rewardedReady = false;
        OnAdShown?.Invoke();
        OnAdRewarded?.Invoke();
        OnAdClosed?.Invoke();
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
