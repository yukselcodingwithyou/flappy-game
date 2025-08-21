using UnityEngine;

/// <summary>
/// Frog that hops periodically and damages the player on contact.
/// </summary>
public class Frog : MonoBehaviour
{
    public float hopForce = 5f;
    public float hopInterval = 2f;
    public int damage = 1;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating(nameof(Hop), 0, hopInterval);
    }

    void Hop()
    {
        rb.velocity = Vector2.up * hopForce;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth health = collision.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }
}
