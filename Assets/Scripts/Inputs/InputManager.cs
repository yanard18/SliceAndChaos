using UnityEngine;
using UnityEngine.InputSystem;

namespace DenizYanar.Inputs
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private PlayerInputs _inputs;

        public void HandleJumpInput(InputAction.CallbackContext context)
        {
            if (context.started)
                _inputs.Jump = true;

            if (context.canceled)
                _inputs.Jump = false;
        }

        public void HandleDiveInput(InputAction.CallbackContext context)
        {
            if (context.started)
                _inputs.Dive = true;

            if (context.canceled)
                _inputs.Dive = false;
        }

        public void HandleShiftModeInput(InputAction.CallbackContext context)
        {
            if (context.started)
                _inputs.Shift = true;

            if (context.canceled)
                _inputs.Shift = false;
        }
        
        public void HandleHorizontalInput(InputAction.CallbackContext context)
        {
            _inputs.HorizontalMovement = context.ReadValue<float>();
        }

        public void HandleAttack1Input(InputAction.CallbackContext context)
        {
            if (context.started)
                _inputs.Attack1 = true;

            if (context.canceled)
                _inputs.Attack1 = false;
        }
        
        public void HandleAttack2Input(InputAction.CallbackContext context)
        {
            if (context.started)
                _inputs.Attack2 = true;

            if (context.canceled)
                _inputs.Attack2 = false;
        }

        public void HandleTelekinesisInput(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                _inputs.Telekinesis = true;
                _inputs.OnTelekinesisStarted?.Invoke();
            }

            if (context.canceled)
            {
                _inputs.Telekinesis = false;
                _inputs.OnTelekinesisCancelled?.Invoke();
            }
            


        }
    }
}
