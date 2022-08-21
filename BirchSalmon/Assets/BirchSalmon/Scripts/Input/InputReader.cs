using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace BirchSalmon
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
    public class InputReader : ScriptableObject, GameInput.IGameplayActions
    {
        public event UnityAction<Vector2> MoveEvent = delegate { };

        private GameInput _gameInput;

        private void OnEnable()
        {
            if (_gameInput == null)
            {
                _gameInput = new GameInput();

                _gameInput.Gameplay.SetCallbacks(this);

                EnableGameplayInput();
            }
        }

        private void OnDisable()
        {
            DisableAllInput();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveEvent.Invoke(context.ReadValue<Vector2>());
        }

        public void EnableGameplayInput()
        {
            _gameInput.Gameplay.Enable();
        }

        public void DisableAllInput()
        {
            _gameInput.Gameplay.Disable();
        }
    }
}
