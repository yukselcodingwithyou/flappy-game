using UnityEngine;

/// <summary>
/// Simple mine that explodes when the player gets close.
/// Shows a telegraph before dealing damage.
/// </summary>
public class Mine : MonoBehaviour
{
    public float warningDuration = 1.0f;
    public int damage = 1;
    private bool armed = false;

    private void Start()
    {
        // Begin telegraph as soon as the mine becomes active
        StartCoroutine(Telegraph());
    }

    private System.Collections.IEnumerator Telegraph()
    {
        yield return new WaitForSeconds(warningDuration);
        armed = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!armed) return;

        PlayerHealth health = collision.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
