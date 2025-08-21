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

    public event Action<BuffType> OnBuffStarted;
    public event Action<BuffType> OnBuffEnded;

    /// <summary>
    /// Apply a buff for a duration in seconds.
    /// </summary>
    public void Apply(BuffType type, float duration)
    {
        float end = Time.time + duration;
        if (m_active.ContainsKey(type))
            m_active[type] = end;
        else
        {
            m_active.Add(type, end);
            OnBuffStarted?.Invoke(type);
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
