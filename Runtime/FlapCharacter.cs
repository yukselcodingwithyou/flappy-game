using UnityEngine;

namespace FlappyGame.Runtime
{
    /// <summary>
    /// Simple controller that applies an upward force when the configured key is pressed.
    /// It requires a <see cref="Rigidbody2D"/> on the same GameObject.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class FlapCharacter : MonoBehaviour
    {
        [Tooltip("Force applied vertically when flapping.")]
        public float flapForce = 6f;

        [Tooltip("Input key used to trigger a flap.")]
        public KeyCode input = KeyCode.Space;

        private Rigidbody2D _body;

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(input))
            {
                var velocity = _body.velocity;
                velocity.y = flapForce;
                _body.velocity = velocity;
            }

            // Apply a small tilt based on vertical velocity.
            float angle = Mathf.Clamp(_body.velocity.y * 5f, -30f, 30f);
            _body.MoveRotation(angle);
        }
    }
}
