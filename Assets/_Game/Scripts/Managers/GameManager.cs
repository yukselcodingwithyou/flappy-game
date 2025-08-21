using System;
using UnityEngine;

/// <summary>
/// Handles core game state transitions and scoring.
/// </summary>
public class GameManager : MonoBehaviour
{ 
    public enum GameState { Menu, Countdown, Playing, Paused, GameOver }

    [SerializeField] private SaveSystem m_saveSystem;

    public static GameManager Instance { get; private set; }

    public GameState State { get; private set; } = GameState.Menu;

    public event Action OnRunStarted;
    public event Action OnRunEnded;
    public event Action OnPaused;
    public event Action OnResumed;
    public event Action<int> OnScoreChanged;
    public event Action<int> OnBestScoreUpdated;
    public event Action<int> OnCoinsChanged;
    public event Action<float> OnComboChanged;

    private int m_score;
    private int m_coins;
    private float m_combo;

    /// <summary>
    /// Begin a new run.
    /// </summary>
    public void StartRun()
    {
        m_score = 0;
        m_coins = 0;
        m_combo = 0f;
        State = GameState.Playing;
        Time.timeScale = 1f;
        OnRunStarted?.Invoke();
        OnCoinsChanged?.Invoke(m_coins);
        OnComboChanged?.Invoke(m_combo);
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
        m_saveSystem.TotalCoins += m_coins;
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
    /// Add coins for the current run and notify listeners.
    /// </summary>
    public void AddCoins(int amount)
    {
        m_coins += amount;
        OnCoinsChanged?.Invoke(m_coins);
    }

    /// <summary>
    /// Update the current combo meter value.
    /// </summary>
    public void UpdateCombo(float value)
    {
        m_combo = value;
        OnComboChanged?.Invoke(m_combo);
    }

    /// <summary>
    /// Pause the game.
    /// </summary>
    public void Pause()
    {
        if (State != GameState.Playing) return;
        State = GameState.Paused;
        Time.timeScale = 0f;
        OnPaused?.Invoke();
    }

    /// <summary>
    /// Resume the game from pause.
    /// </summary>
    public void Resume()
    {
        if (State != GameState.Paused) return;
        State = GameState.Playing;
        Time.timeScale = 1f;
        OnResumed?.Invoke();
    }

    /// <summary>
    /// Load saves on awake.
    /// </summary>
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        m_saveSystem?.Load();
    }
}
