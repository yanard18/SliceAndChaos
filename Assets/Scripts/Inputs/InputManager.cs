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

        public void HandleMagnetPullInput(InputAction.CallbackContext context)
        {
            if (context.started)
                _inputs.OnMagnetPullStarted?.Invoke();

            if (context.canceled)
                _inputs.OnMagnetPullCancelled?.Invoke();
        }

        public void HandleMagnetPushInput(InputAction.CallbackContext context)
        {
            if(context.started)
                _inputs.OnMagnetPushPressed?.Invoke();
        }

        public void HandleDevConsoleKeyPressed(InputAction.CallbackContext context)
        {
            if(context.started)
                _inputs.OnDevConsoleKeyPressed?.Invoke();
        }
    }
}
