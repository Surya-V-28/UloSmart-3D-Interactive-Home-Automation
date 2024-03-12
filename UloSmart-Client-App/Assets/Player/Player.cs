using UnityEngine;
using UnityEngine.InputSystem;

using InteractableSystem;

using RoomSystem;

namespace PlayerSystem
{
    public class Player : MonoBehaviour
    {
        private void Start()
        {
            rotator = GetComponent<Rotator>();
            movement = GetComponent<Movement>();
            
            interactor = GetComponent<Interactor>();

            inputActions = new PlayerInputActions();
            inputActions.Default.Interact.performed += onInteractButtonPressed;
            inputActions.Default.CursorMode.performed += onCursorModeButtonPressed;
            inputActions.Enable();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            if (isMovementEnabled)
            {
                handleMovement();
            }
        }

        public void ToCursorModeState()
        {            
            interactor.enabled = false;
            DisableMovement();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void ExitCursorModeState()
        {
            interactor.enabled = true;
            EnableMovement();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


        public void EnableMovement()
        {
            if (isMovementEnabled) return;
            
            isMovementEnabled = true;
        }

        public void DisableMovement()
        {
            if (!isMovementEnabled) return;

            isMovementEnabled = false;
        }
        
        public void TeleportToRoom(Room room)
        {
            transform.position = room.TeleportPoint + Vector3.up * 0.01f;
        }

        private void handleMovement()
        {
            Vector2 movementInput = inputActions.Default.Movement.ReadValue<Vector2>();
            movement.AddInput(movementInput);

            Vector2 mouseMove = inputActions.Default.MouseMove.ReadValue<Vector2>();
            rotator.AddInput(mouseMove);
        }

        private void onCursorModeButtonPressed(InputAction.CallbackContext context)
        {
            if (isMovementEnabled) ToCursorModeState();
            else ExitCursorModeState();
        }

        private void onInteractButtonPressed(InputAction.CallbackContext context)
        {
            if (interactor.HighlightedInteractable == null) return;

            interactor.Interact();
        }





        private bool isMovementEnabled = true;

        private Rotator rotator = null;
        private Movement movement = null;

        private Interactor interactor = null;

        private PlayerInputActions inputActions = null;
    }
}