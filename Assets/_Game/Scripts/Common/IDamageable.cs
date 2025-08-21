using UnityEngine;

/// <summary>
/// Represents an object that can receive damage.
/// </summary>
public interface IDamageable
{
    void TakeDamage(int amount);
}
