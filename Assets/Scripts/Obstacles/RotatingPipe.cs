using UnityEngine;

/// <summary>
/// Rotating pipe that harms the player on contact.
/// </summary>
public class RotatingPipe : MonoBehaviour
{
    public float rotationSpeed = 45f;
    public int damage = 1;

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
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
