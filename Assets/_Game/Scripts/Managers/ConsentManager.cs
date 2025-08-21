using UnityEngine;

/// <summary>
/// Handles user consent flows such as GDPR and ATT prompts.
/// </summary>
public class ConsentManager : MonoBehaviour
{
    public ConsentState CurrentConsent { get; private set; } = ConsentState.Unknown;

    /// <summary>
    /// Simulate displaying consent dialogs and storing the result.
    /// </summary>
    public void RequestConsent()
    {
        // In a production game this would display native dialogs.
        CurrentConsent = ConsentState.Accepted;
    }
}
