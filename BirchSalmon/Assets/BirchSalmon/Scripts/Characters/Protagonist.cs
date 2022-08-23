using UnityEngine;

namespace BirchSalmon
{
    public class Protagonist : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader = default;
        [SerializeField] private LayerMask _groundLayerMask;

        private CapsuleCollider _collider;
        private Rigidbody _rb;
        private Vector2 _movement;
        private float _moveSpeed = 5f;
        private float _rotationSpeed = 360f;
        private float _jumpForce = 50f;
        private float _groundCheckDistance;

        private void OnEnable()
        {
            _inputReader.MoveEvent += OnMove;
            _inputReader.JumpEvent += OnJump;
        }

        private void OnDisable()
        {
            _inputReader.MoveEvent -= OnMove;
            _inputReader.JumpEvent -= OnJump;
        }

        private void Start()
        {
            _collider = GetComponent<CapsuleCollider>();
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            var movement = new Vector3(_movement.x, 0f, _movement.y);
            var finalSpeed = _moveSpeed;

            _rb.MovePosition(_rb.position + movement * finalSpeed * Time.fixedDeltaTime);
            if (movement != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
            }
        }

        private bool IsGrounded()
        {
            var extraHeightText = 0.51f;
            Physics.SphereCast(_collider.bounds.center, _collider.radius, Vector3.down, out var hit, extraHeightText, _groundLayerMask);
            return hit.collider != null;
        }

        // Event Listeners
        private void OnMove(Vector2 movement)
        {
            _movement = movement;
        }

        private void OnJump()
        {
            if (IsGrounded())
                _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
        }
    }
}
