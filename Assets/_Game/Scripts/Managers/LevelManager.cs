using System;
using UnityEngine;

/// <summary>
/// Tracks level progression based on distance traveled.
/// </summary>
public class LevelManager : MonoBehaviour
{
    [SerializeField] private DifficultyCurve m_curve;
    [SerializeField] private float m_distancePerLevel = 100f;

    private int m_level;
    private float m_distance;

    public int Level => m_level;

    public event Action<int> OnLevelChanged;

    /// <summary>
    /// Add distance and update level accordingly.
    /// </summary>
    public void AddDistance(float delta)
    {
        m_distance += delta;
        int newLevel = Mathf.FloorToInt(m_distance / m_distancePerLevel);
        if (newLevel > m_level)
        {
            m_level = newLevel;
            OnLevelChanged?.Invoke(m_level);
        }
    }

    /// <summary>
    /// Gets current scroll speed from difficulty curve.
    /// </summary>
    public float GetScrollSpeed()
    {
        return m_curve != null ? m_curve.scrollSpeed.Evaluate(m_level) : 0f;
    }

    /// <summary>
    /// Gets obstacle spawn interval from difficulty curve.
    /// </summary>
    public float GetObstacleInterval()
    {
        return m_curve != null ? m_curve.obstacleSpawnInterval.Evaluate(m_level) : 1f;
    }
}
