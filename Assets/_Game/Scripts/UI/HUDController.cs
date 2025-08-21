using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the heads up display elements such as health, coins and buffs.
/// </summary>
public class HUDController : MonoBehaviour
{
    [SerializeField] private Text m_healthText;
    [SerializeField] private Text m_coinText;
    [SerializeField] private Text m_scoreText;
    [SerializeField] private Text m_comboText;

    [Serializable]
    private class BuffTimer
    {
        public BuffController.BuffType type;
        public Image fillImage;
        [NonSerialized] public float duration;
        [NonSerialized] public float start;
        public void Begin(float d)
        {
            duration = d;
            start = Time.time;
            if (fillImage != null)
                fillImage.gameObject.SetActive(true);
        }
        public void End()
        {
            if (fillImage != null)
            {
                fillImage.fillAmount = 0f;
                fillImage.gameObject.SetActive(false);
            }
        }
        public void Tick()
        {
            if (fillImage == null || !fillImage.gameObject.activeSelf) return;
            float remaining = (start + duration) - Time.time;
            fillImage.fillAmount = Mathf.Clamp01(remaining / duration);
        }
    }

    [SerializeField] private BuffTimer[] m_buffTimers;
    private Dictionary<BuffController.BuffType, BuffTimer> m_timerLookup;

    private void Awake()
    {
        m_timerLookup = new Dictionary<BuffController.BuffType, BuffTimer>();
        foreach (var timer in m_buffTimers)
            m_timerLookup[timer.type] = timer;
    }

    private void Update()
    {
        if (m_timerLookup == null) return;
        foreach (var timer in m_timerLookup.Values)
            timer.Tick();
    }

    public void Bind(GameManager gm, Health health, BuffController buffs)
    {
        if (gm != null)
        {
            gm.OnScoreChanged += HandleScore;
            gm.OnCoinsChanged += HandleCoins;
            gm.OnComboChanged += HandleCombo;
        }
        if (health != null)
            health.OnHealthChanged += HandleHealth;
        if (buffs != null)
        {
            buffs.OnBuffStarted += HandleBuffStart;
            buffs.OnBuffEnded += HandleBuffEnd;
        }
    }

    private void HandleHealth(int value)
    {
        if (m_healthText != null)
            m_healthText.text = $"HP: {value}";
    }

    private void HandleCoins(int value)
    {
        if (m_coinText != null)
            m_coinText.text = $"Coins: {value}";
    }

    private void HandleScore(int value)
    {
        if (m_scoreText != null)
            m_scoreText.text = $"Score: {value}";
    }

    private void HandleCombo(float value)
    {
        if (m_comboText != null)
            m_comboText.text = $"Combo: {value:0.0}";
    }

    private void HandleBuffStart(BuffController.BuffType type, float duration)
    {
        if (m_timerLookup != null && m_timerLookup.TryGetValue(type, out var timer))
            timer.Begin(duration);
    }

    private void HandleBuffEnd(BuffController.BuffType type)
    {
        if (m_timerLookup != null && m_timerLookup.TryGetValue(type, out var timer))
            timer.End();
    }
}
