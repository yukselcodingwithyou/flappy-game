using System;
using UnityEngine;

/// <summary>
/// Central manager that coordinates ad display and enforces policy caps.
/// </summary>
public class AdsManager : MonoBehaviour
{
    [SerializeField] private AdPolicy m_policy;
    [SerializeField] private bool m_testMode = true;

    private IAdNetworkAdapter m_adapter;
    private float m_lastInterstitialTime;
    private int m_runsSinceInterstitial;
    private int m_sessionInterstitialCount;
    private bool m_skipInterstitial;

    /// <summary>
    /// Initialize the ads system using the provided adapter.
    /// </summary>
    /// <param name="adapter">Concrete network adapter.</param>
    /// <param name="consent">User consent state.</param>
    public void Initialize(IAdNetworkAdapter adapter, ConsentState consent)
    {
        m_adapter = adapter;
        m_adapter.Initialize(consent);
        m_adapter.LoadInterstitial();
        m_adapter.LoadRewarded();
    }

    /// <summary>
    /// Should be called whenever a new run starts.
    /// </summary>
    public void NotifyRunStarted()
    {
        m_runsSinceInterstitial++;
    }

    /// <summary>
    /// Try to show an interstitial respecting frequency caps.
    /// </summary>
    public void TryShowInterstitial()
    {
        if (m_policy == null || m_adapter == null) return;
        if (m_skipInterstitial)
        {
            m_skipInterstitial = false;
            return;
        }
        if (m_runsSinceInterstitial < m_policy.showInterstitialEveryNRuns) return;
        if (Time.realtimeSinceStartup - m_lastInterstitialTime < m_policy.minSecondsBetweenInterstitial) return;
        if (m_sessionInterstitialCount >= m_policy.maxInterstitialsPerSession) return;
        if (!m_adapter.IsInterstitialReady) return;
        m_adapter.ShowInterstitial();
        m_adapter.LoadInterstitial();
        m_lastInterstitialTime = Time.realtimeSinceStartup;
        m_sessionInterstitialCount++;
        m_runsSinceInterstitial = 0;
    }

    /// <summary>
    /// Try to show a rewarded ad and invoke callback on reward.
    /// </summary>
    public void TryShowRewarded(Action onReward)
    {
        if (m_adapter == null || !m_adapter.IsRewardedReady) return;
        void RewardHandler()
        {
            onReward?.Invoke();
            m_adapter.OnAdRewarded -= RewardHandler;
            m_skipInterstitial = true;
        }
        m_adapter.OnAdRewarded += RewardHandler;
        m_adapter.ShowRewarded();
        m_adapter.LoadRewarded();
    }

    /// <summary>
    /// Show banner if policy allows it.
    /// </summary>
    public void ShowBanner()
    {
        if (m_policy != null && m_policy.bannerEnabled)
            m_adapter?.ShowBanner();
    }

    /// <summary>
    /// Hide banner if visible.
    /// </summary>
    public void HideBanner()
    {
        m_adapter?.HideBanner();
    }

    /// <summary>
    /// Apply ad-free purchase disabling all ads.
    /// </summary>
    public void ApplyAdFree()
    {
        m_policy = null;
        m_adapter?.HideBanner();
    }
}

/// <summary>
/// Scriptable object defining ad frequency and placements.
/// </summary>
[CreateAssetMenu(menuName = "_Game/Ads/AdPolicy")]
public class AdPolicy : ScriptableObject
{
    [Tooltip("Show interstitial every N runs."), Min(1)]
    public int showInterstitialEveryNRuns = 2;

    [Tooltip("Minimum seconds between interstitial impressions."), Min(0f)]
    public float minSecondsBetweenInterstitial = 90f;

    [Tooltip("Maximum interstitials per session."), Min(0)]
    public int maxInterstitialsPerSession = 2;

    public bool bannerEnabled = true;
    public bool rewardedContinueEnabled = true;
    public bool rewardedDoubleCoinsEnabled = true;
    public bool rewardedStartPowerUpEnabled = true;
}
