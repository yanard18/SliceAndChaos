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
                _inputs.OnJumpStarted?.Invoke();
        }

        public void HandleDiveInput(InputAction.CallbackContext context)
        {
            if (context.started)
                _inputs.OnDiveStarted?.Invoke();

            if (context.canceled)
                _inputs.OnDiveCancelled?.Invoke();
            
        }

        public void HandleShiftModeInput(InputAction.CallbackContext context)
        {
            if (context.started)
                _inputs.OnShiftStarted?.Invoke();
        }
        
        public void HandleHorizontalInput(InputAction.CallbackContext context)
        {
            _inputs.HorizontalMovement = context.ReadValue<float>();
        }

        public void HandleAttack1Input(InputAction.CallbackContext context)
        {
            if (context.started)
                _inputs.OnAttack1Started?.Invoke();
        }
        
        public void HandleAttack2Input(InputAction.CallbackContext context)
        {
            if (context.started)
                _inputs.OnAttack2Started?.Invoke();
        }

        public void HandleTelekinesisInput(InputAction.CallbackContext context)
        {
            if (context.started)
                _inputs.OnTelekinesisStarted?.Invoke();

            if (context.canceled)
                _inputs.OnTelekinesisCancelled?.Invoke();
        }
    }
}
