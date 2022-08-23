using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace BirchSalmon
{
    [CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
    public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IMenuActions
    {
        public event UnityAction<Vector2> MoveEvent = delegate { };
        public event UnityAction JumpEvent = delegate { };
        public event UnityAction InteractEvent = delegate { };

        private GameInput _gameInput;

        private void OnEnable()
        {
            if (_gameInput == null)
            {
                _gameInput = new GameInput();

                _gameInput.Gameplay.SetCallbacks(this);
                _gameInput.Menu.SetCallbacks(this);

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

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
                JumpEvent.Invoke();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {

        }

        public void EnableGameplayInput()
        {
            _gameInput.Gameplay.Enable();
            _gameInput.Menu.Disable();
        }

        public void DisableAllInput()
        {
            _gameInput.Gameplay.Disable();
            _gameInput.Menu.Disable();
        }

    }
}
