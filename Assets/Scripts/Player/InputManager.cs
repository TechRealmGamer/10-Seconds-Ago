using UnityEngine;

namespace LuciferGamingStudio
{
    public class InputManager : MonoBehaviour
    {
        // This script manages player input using the new Input System.

        public static InputManager Instance { get; private set; }

        private PlayerInputActions playerInputActions;

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;

            playerInputActions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            playerInputActions.Enable();
        }

        private void OnDisable()
        {
            playerInputActions.Disable();
        }

        public Vector2 GetMovementInput()
        {
            return playerInputActions.Ghost.Movement.ReadValue<Vector2>().normalized;
        }
    }
}
