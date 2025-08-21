using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Simple JSON save system backed by PlayerPrefs.
/// </summary>
public class SaveSystem : MonoBehaviour
{
    private const string c_SaveKey = "_game_save";
    private const int c_Version = 1;

    [Serializable]
    private class SaveData
    {
        public int version;
        public int bestScore;
        public int totalCoins;
        public bool adFree;
        public List<string> unlockedCharacters = new List<string>();
        public List<string> purchasedUpgrades = new List<string>();
        public SettingsData settings = new SettingsData();
        public int adTotalRunCount;
        public string lastInterstitial;
        public string lastRewarded;
    }

    [Serializable]
    public class SettingsData
    {
        public float musicVolume = 1f;
        public float sfxVolume = 1f;
    }

    private SaveData m_data = new SaveData { version = c_Version };

    /// <summary>
    /// Load persistent data from PlayerPrefs.
    /// </summary>
    public void Load()
    {
        if (PlayerPrefs.HasKey(c_SaveKey))
        {
            string json = PlayerPrefs.GetString(c_SaveKey);
            m_data = JsonUtility.FromJson<SaveData>(json);
            if (m_data.version != c_Version)
                Migrate(m_data.version);
        }
        else
        {
            m_data = new SaveData { version = c_Version };
        }
    }

    /// <summary>
    /// Save current data to PlayerPrefs.
    /// </summary>
    public void Save()
    {
        m_data.version = c_Version;
        string json = JsonUtility.ToJson(m_data);
        PlayerPrefs.SetString(c_SaveKey, json);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Gets or sets the best score.
    /// </summary>
    public int BestScore
    {
        get => m_data.bestScore;
        set => m_data.bestScore = Mathf.Max(m_data.bestScore, value);
    }

    /// <summary>
    /// Total accumulated coins.
    /// </summary>
    public int TotalCoins
    {
        get => m_data.totalCoins;
        set => m_data.totalCoins = value;
    }

    /// <summary>
    /// Returns true if the player purchased ad removal.
    /// </summary>
    public bool AdFree
    {
        get => m_data.adFree;
        set => m_data.adFree = value;
    }

    public IList<string> UnlockedCharacters => m_data.unlockedCharacters;

    public bool IsCharacterUnlocked(string id) => m_data.unlockedCharacters.Contains(id);

    public void UnlockCharacter(string id)
    {
        if (!m_data.unlockedCharacters.Contains(id))
            m_data.unlockedCharacters.Add(id);
    }

    public IList<string> PurchasedUpgrades => m_data.purchasedUpgrades;

    public bool HasUpgrade(string id) => m_data.purchasedUpgrades.Contains(id);

    public void PurchaseUpgrade(string id)
    {
        if (!m_data.purchasedUpgrades.Contains(id))
            m_data.purchasedUpgrades.Add(id);
    }

    public SettingsData Settings => m_data.settings;

    public int AdTotalRunCount
    {
        get => m_data.adTotalRunCount;
        set => m_data.adTotalRunCount = value;
    }

    public DateTime LastInterstitialShown
    {
        get => DateTime.TryParse(m_data.lastInterstitial, out var dt) ? dt : DateTime.MinValue;
        set => m_data.lastInterstitial = value.ToString("o");
    }

    public DateTime LastRewardedShown
    {
        get => DateTime.TryParse(m_data.lastRewarded, out var dt) ? dt : DateTime.MinValue;
        set => m_data.lastRewarded = value.ToString("o");
    }

    private void Migrate(int fromVersion)
    {
        // Placeholder for future save migrations
        m_data.version = c_Version;
    }
}
