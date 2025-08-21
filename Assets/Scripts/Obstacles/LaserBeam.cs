using UnityEngine;

/// <summary>
/// Laser beam that sweeps across the screen after a telegraph.
/// </summary>
public class LaserBeam : MonoBehaviour
{
    public LineRenderer renderer;
    public float warningDuration = 1.0f;
    public float activeDuration = 2.0f;
    public int damage = 1;

    private void Start()
    {
        StartCoroutine(FireRoutine());
    }

    private System.Collections.IEnumerator FireRoutine()
    {
        renderer.enabled = true; // telegraph
        yield return new WaitForSeconds(warningDuration);

        float timer = activeDuration;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerHealth health = collision.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
    }
}
