using System;
using UnityEngine;

/// <summary>
/// Simple coin collectible.
/// </summary>
public class Coin : MonoBehaviour, IPooled
{
    [SerializeField] private int m_value = 1;

    public event Action<int> OnCollected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            Collect();
    }

    private void Collect()
    {
        OnCollected?.Invoke(m_value);

        // Inform the GameManager of the coin pickup so HUD can update the count.
        var gm = FindObjectOfType<GameManager>();
        gm?.AddCoins(m_value);

        gameObject.SetActive(false);
    }

    public void OnSpawned() { }
    public void OnDespawned() { }
}
