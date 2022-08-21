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

            var hit = new RaycastHit();
            if (Physics.Raycast(transform.position, -Vector3.up, out hit))
            {
                var distanceToGround = hit.distance;
                if ((distanceToGround > 1.25 || distanceToGround < 0.75) && finalSpeed == _moveSpeed)
                {
                    finalSpeed = _moveSpeed / 5;
                    Debug.Log($"Not touching ground, slowing down.\ndistanceToGround: {distanceToGround}");
                }
                else if (finalSpeed != _moveSpeed)
                {
                    finalSpeed = _moveSpeed;
                    Debug.Log("Touching ground, reverting speed");
                }
            }

            _rb.MovePosition(_rb.position + movement * finalSpeed * Time.fixedDeltaTime);
        }

        // Event Listeners
        private void OnMove(Vector2 movement)
        {
            _movement = movement;
        }
    }
}
