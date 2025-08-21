using System;
using UnityEngine;

/// <summary>
/// Handles core game state transitions and scoring.
/// </summary>
public class GameManager : MonoBehaviour
{
    public enum GameState { Menu, Countdown, Playing, Paused, GameOver }

    [SerializeField] private SaveSystem m_saveSystem;

    public GameState State { get; private set; } = GameState.Menu;

    public event Action OnRunStarted;
    public event Action OnRunEnded;
    public event Action<int> OnScoreChanged;
    public event Action<int> OnBestScoreUpdated;

    private int m_score;

    /// <summary>
    /// Begin a new run.
    /// </summary>
    public void StartRun()
    {
        m_score = 0;
        State = GameState.Playing;
        OnRunStarted?.Invoke();
    }

    /// <summary>
    /// End the current run.
    /// </summary>
    public void EndRun()
    {
        State = GameState.GameOver;
        OnRunEnded?.Invoke();
        if (m_score > m_saveSystem.BestScore)
        {
            m_saveSystem.BestScore = m_score;
            OnBestScoreUpdated?.Invoke(m_score);
        }
        m_saveSystem.Save();
    }

    /// <summary>
    /// Add to current score and notify listeners.
    /// </summary>
    public void AddScore(int amount)
    {
        m_score += amount;
        OnScoreChanged?.Invoke(m_score);
    }

    /// <summary>
    /// Load saves on awake.
    /// </summary>
    private void Awake()
    {
        m_saveSystem?.Load();
    }
}
