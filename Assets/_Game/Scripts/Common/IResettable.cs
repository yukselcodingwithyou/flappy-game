using UnityEngine;

/// <summary>
/// Resets an object's state for pooling or game restarts.
/// </summary>
public interface IResettable
{
    void ResetState();
}
