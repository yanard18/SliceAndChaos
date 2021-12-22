using UnityEngine;
using UnityEngine.InputSystem;

namespace DenizYanar
{
    public class PlayerInputsController : MonoBehaviour
    {
        [SerializeField] private PlayerInput _input;
        
        public void ReadMovementInput(InputAction.CallbackContext context)
        {
            if(context.phase == InputActionPhase.Performed)
                _input.InvokeHorizontalMovementEvent(context.ReadValue<float>());

        }

        public void ReadJumpInput(InputAction.CallbackContext context)
        {
            if(context.phase == InputActionPhase.Performed)
                _input.InvokeJumpEvent();
            if(context.phase == InputActionPhase.Canceled)
                _input.InvokeJumpReleased();
        }
        
    }
}
