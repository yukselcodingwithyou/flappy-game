using UnityEngine;

/// <summary>
/// Restores player health on pickup.
/// </summary>
public class HealthPack : MonoBehaviour
{
    public int amount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth health = collision.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.Heal(amount);
            Destroy(gameObject);
        }
    }
}
