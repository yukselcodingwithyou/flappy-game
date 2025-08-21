using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Centralized random number provider with deterministic seed support.
/// </summary>
public class RandomService : MonoBehaviour
{
    [SerializeField] private int m_seed = 0;
    private System.Random m_random;

    private void Awake()
    {
        m_random = m_seed == 0 ? new System.Random() : new System.Random(m_seed);
    }

    /// <summary>
    /// Re-seed the random generator.
    /// </summary>
    public void SetSeed(int seed)
    {
        m_seed = seed;
        m_random = new System.Random(m_seed);
    }

    /// <summary>
    /// Returns a value in [0,1).
    /// </summary>
    public float Value => (float)m_random.NextDouble();

    /// <summary>
    /// Weighted random selection from list.
    /// </summary>
    public T WeightedRandom<T>(IList<T> items, IList<float> weights)
    {
        if (items == null || weights == null || items.Count != weights.Count || items.Count == 0)
            throw new ArgumentException("Invalid weights");
        float total = 0f;
        for (int i = 0; i < weights.Count; i++)
            total += Mathf.Max(0f, weights[i]);
        float roll = Value * total;
        for (int i = 0; i < items.Count; i++)
        {
            roll -= Mathf.Max(0f, weights[i]);
            if (roll <= 0f)
                return items[i];
        }
        return items[items.Count - 1];
    }
}
