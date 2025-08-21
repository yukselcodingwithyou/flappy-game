using UnityEngine;

/// <summary>
/// Tracks player score and combos.
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public ComboSystem combo;
    private int score;

    public void AddScore(int baseValue)
    {
        int mult = 1;
        if (combo != null)
        {
            combo.RegisterAction();
            mult = Mathf.Max(1, combo.CurrentCombo);
        }
        score += baseValue * mult;
    }

    public int CurrentScore => score;
}
