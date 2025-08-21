using UnityEngine;

/// <summary>
/// Interface for pooled objects.
/// </summary>
public interface IPooled
{
    void OnSpawned();
    void OnDespawned();
}
