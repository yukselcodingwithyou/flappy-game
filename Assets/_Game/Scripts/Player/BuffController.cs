using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles timed player buffs.
/// </summary>
public class BuffController : MonoBehaviour
{
    public enum BuffType { Shield, MonsterWard, Magnet, DoubleCoins, SlowTime }

    private readonly Dictionary<BuffType, float> m_active = new Dictionary<BuffType, float>();

    /// <summary>
    /// Invoked when a buff begins. The float parameter represents the duration in seconds.
    /// </summary>
    public event Action<BuffType, float> OnBuffStarted;

    /// <summary>
    /// Invoked when a buff expires.
    /// </summary>
    public event Action<BuffType> OnBuffEnded;

    /// <summary>
    /// Apply a buff for a duration in seconds.
    /// </summary>
    public void Apply(BuffType type, float duration)
    {
        float end = Time.time + duration;
        if (m_active.ContainsKey(type))
        {
            m_active[type] = end;
        }
        else
        {
            m_active.Add(type, end);
            // Notify listeners with the total duration of the buff so UI timers can be displayed.
            OnBuffStarted?.Invoke(type, duration);
        }
    }

    private void Update()
    {
        if (m_active.Count == 0) return;
        var ended = new List<BuffType>();
        foreach (var kvp in m_active)
        {
            if (Time.time >= kvp.Value)
                ended.Add(kvp.Key);
        }
        foreach (var type in ended)
        {
            m_active.Remove(type);
            OnBuffEnded?.Invoke(type);
        }
    }

    /// <summary>
    /// Check if the buff is active.
    /// </summary>
    public bool Has(BuffType type) => m_active.ContainsKey(type);
}
