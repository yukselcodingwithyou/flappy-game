using System;
using UnityEngine;

/// <summary>
/// Manages player health and damage.
/// </summary>
public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int m_maxHealth = 3;

    private int m_current;

    public int MaxHealth => m_maxHealth;
    public int Current => m_current;

    public event Action<int> OnHealthChanged;
    public event Action OnDied;

    private void Awake()
    {
        m_current = m_maxHealth;
    }

    /// <summary>
    /// Apply damage to the entity.
    /// </summary>
    public void TakeDamage(int amount)
    {
        m_current = Mathf.Max(0, m_current - amount);
        OnHealthChanged?.Invoke(m_current);
        if (m_current == 0)
            OnDied?.Invoke();
    }

    /// <summary>
    /// Heal up to max health.
    /// </summary>
    public void Heal(int amount)
    {
        m_current = Mathf.Min(m_maxHealth, m_current + amount);
        OnHealthChanged?.Invoke(m_current);
    }

    /// <summary>
    /// Reset health to max.
    /// </summary>
    public void ResetHealth()
    {
        m_current = m_maxHealth;
        OnHealthChanged?.Invoke(m_current);
    }
}
