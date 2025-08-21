using System.IO;
using UnityEngine;

/// <summary>
/// Simple JSON save system backed by PlayerPrefs.
/// </summary>
public class SaveSystem : MonoBehaviour
{
    private const string c_SaveKey = "_game_save";

    [System.Serializable]
    private class SaveData
    {
        public int bestScore;
        public int totalCoins;
        public bool adFree;
    }

    private SaveData m_data = new SaveData();

    /// <summary>
    /// Load persistent data from PlayerPrefs.
    /// </summary>
    public void Load()
    {
        if (PlayerPrefs.HasKey(c_SaveKey))
        {
            string json = PlayerPrefs.GetString(c_SaveKey);
            m_data = JsonUtility.FromJson<SaveData>(json);
        }
    }

    /// <summary>
    /// Save current data to PlayerPrefs.
    /// </summary>
    public void Save()
    {
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
}
