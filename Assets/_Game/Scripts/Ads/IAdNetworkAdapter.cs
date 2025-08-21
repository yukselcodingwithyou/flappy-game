using System;

/// <summary>
/// Interface for ad network adapters.
/// </summary>
public interface IAdNetworkAdapter
{
    /// <summary>
    /// Raised when an ad is successfully loaded.
    /// </summary>
    event Action OnAdLoaded;

    /// <summary>
    /// Raised when an ad fails to load.
    /// </summary>
    event Action<string> OnAdFailed;

    /// <summary>
    /// Raised when an ad is shown to the user.
    /// </summary>
    event Action OnAdShown;

    /// <summary>
    /// Raised when the ad is closed by the user.
    /// </summary>
    event Action OnAdClosed;

    /// <summary>
    /// Raised when a reward is earned from a rewarded ad.
    /// </summary>
    event Action OnAdRewarded;

    /// <summary>
    /// Initialize the ad network adapter with consent information.
    /// </summary>
    /// <param name="consent">Consent state of the user.</param>
    void Initialize(ConsentState consent);

    /// <summary>
    /// Begin loading an interstitial ad.
    /// </summary>
    void LoadInterstitial();

    /// <summary>
    /// Indicates whether an interstitial ad is ready to be shown.
    /// </summary>
    bool IsInterstitialReady { get; }

    /// <summary>
    /// Show the interstitial ad if ready.
    /// </summary>
    void ShowInterstitial();

    /// <summary>
    /// Begin loading a rewarded ad.
    /// </summary>
    void LoadRewarded();

    /// <summary>
    /// Indicates whether a rewarded ad is ready to be shown.
    /// </summary>
    bool IsRewardedReady { get; }

    /// <summary>
    /// Show the rewarded ad if ready.
    /// </summary>
    void ShowRewarded();

    /// <summary>
    /// Show a banner ad.
    /// </summary>
    void ShowBanner();

    /// <summary>
    /// Hide the banner ad if visible.
    /// </summary>
    void HideBanner();
}

/// <summary>
/// Enumeration for user consent states.
/// </summary>
public enum ConsentState
{
    Unknown,
    Accepted,
    Rejected
}
