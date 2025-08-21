using UnityEngine;

/// <summary>
/// Simple insect AI that chases the player horizontally.
/// </summary>
public class Insect : MonoBehaviour
{
    public float speed = 2f;
    public int damage = 1;
    private Transform target;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    void Update()
    {
        if (target == null) return;
        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealth health = collision.GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
