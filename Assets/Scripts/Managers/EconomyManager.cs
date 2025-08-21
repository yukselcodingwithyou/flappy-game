using UnityEngine;

/// <summary>
/// Tracks coins and handles purchases.
/// </summary>
public class EconomyManager : MonoBehaviour
{
    private int coins;

    public void AddCoins(int amount)
    {
        coins += amount;
    }

    public bool Purchase(int cost)
    {
        if (coins >= cost)
        {
            coins -= cost;
            return true;
        }
        return false;
    }

    public int Coins => coins;
}
