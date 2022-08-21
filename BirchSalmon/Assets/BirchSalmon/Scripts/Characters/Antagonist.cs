using UnityEngine;

namespace BirchSalmon
{
    public class Antagonist : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader = default;

        private Rigidbody _rb;
        private Vector2 _movement;
        private float _moveSpeed = 5f;

        private void OnEnable()
        {
            _inputReader.MoveEvent += OnMove;
        }

        private void OnDisable()
        {
            _inputReader.MoveEvent -= OnMove;
        }

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();

        }

        private void FixedUpdate()
        {
            var movement = new Vector3(_movement.x, 0f, _movement.y);
            var finalSpeed = _moveSpeed;

            _rb.MovePosition(_rb.position + movement * finalSpeed * Time.fixedDeltaTime);
        }

        // Event Listeners
        private void OnMove(Vector2 movement)
        {
            _movement = movement;
        }
    }
}
