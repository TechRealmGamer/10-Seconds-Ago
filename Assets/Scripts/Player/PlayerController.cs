using UnityEngine;

namespace LuciferGamingStudio
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float rotationSpeed = 720f; // Degrees per second
        public float gravity = -9.81f;

        private InputManager inputManager;
        private CharacterController controller;
        private Animator animator;

        private void Awake()
        {
            inputManager = InputManager.Instance;
            controller = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Vector2 input = inputManager.GetMovementInput();
            Vector3 camForward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.right;

            // Calculate movement direction relative to camera
            Vector3 moveDir = (camForward * input.y + camRight * input.x).normalized;
            moveDir.y = 0; // Ensure we only move on the horizontal plane

            if (moveDir.magnitude > 0.1f)
            {
                // Determine target rotation
                Quaternion targetRotation = Quaternion.LookRotation(moveDir, Vector3.up);

                // Rotate smoothly
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            // Apply movement and gravity
            Vector3 velocity = moveDir * moveSpeed;
            velocity.y += gravity;

            controller.Move(velocity * Time.deltaTime);
            animator.SetFloat("MovementSpeed", moveDir.magnitude * moveSpeed);
        }
    }
}
