using UnityEngine;

/// <summary>
/// Provides damage information for collision handlers.
/// </summary>
public interface IDamageDealer
{
    int Damage { get; }
}
