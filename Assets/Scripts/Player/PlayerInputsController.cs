using UnityEngine;
using UnityEngine.InputSystem;

namespace DenizYanar
{
    public class PlayerInputsController : MonoBehaviour
    {
        [SerializeField] private PlayerInput _input;
        
        public void ReadMovementInput(InputAction.CallbackContext context)
        {
            _input.HorizontalMovement = context.ReadValue<float>();
        }

        public void ReadJumpInput(InputAction.CallbackContext context)
        {
            _input.Jump = context;
        }
    }
}
