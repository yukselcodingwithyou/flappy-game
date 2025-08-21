using UnityEngine;

/// <summary>
/// Handles player input and movement.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_flapForce = 5f;
    [SerializeField] private float m_verticalClamp = 4f;
    [SerializeField] private Rigidbody2D m_body;
    [SerializeField] private Health m_health;
    [SerializeField] private BuffController m_buffs;

    private void Awake()
    {
        if (m_body == null)
            m_body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            Flap();
    }

    private void FixedUpdate()
    {
        Vector2 pos = m_body.position;
        pos.y = Mathf.Clamp(pos.y, -m_verticalClamp, m_verticalClamp);
        m_body.position = pos;
    }

    private void Flap()
    {
        m_body.velocity = new Vector2(m_body.velocity.x, 0f);
        m_body.AddForce(Vector2.up * m_flapForce, ForceMode2D.Impulse);
    }
}
