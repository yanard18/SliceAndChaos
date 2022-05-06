using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DenizYanar.Inputs
{
    public class InputManager : MonoBehaviour
    {
        [Required] [SerializeField]
        private PlayerInputs m_Inputs;


        public void HandleMousePositionInput(InputAction.CallbackContext context)
        {
            if (Camera.main != null)
                m_Inputs.m_MousePosition = context.ReadValue<Vector2>();
        }

        public void HandleHorizontalInput(InputAction.CallbackContext context)
        {
            m_Inputs.m_HorizontalMovement = context.ReadValue<float>();
        }

        public void HandleJumpInput(InputAction.CallbackContext context)
        {
            if (context.started)
                m_Inputs.e_OnJumpStarted?.Invoke();
        }

        public void HandleDiveInput(InputAction.CallbackContext context)
        {
            if (context.started)
                m_Inputs.e_OnDiveStarted?.Invoke();

            if (context.canceled)
                m_Inputs.e_OnDiveCancelled?.Invoke();
        }

        public void HandleShiftModeInput(InputAction.CallbackContext context)
        {
            if (context.started)
                m_Inputs.e_OnShiftStarted?.Invoke();
        }


        public void HandleAttack1Input(InputAction.CallbackContext context)
        {
            if (context.started)
                m_Inputs.e_OnAttack1Started?.Invoke();
        }

        public void HandleAttack2Input(InputAction.CallbackContext context)
        {
            if (context.started)
                m_Inputs.e_OnAttack2Started?.Invoke();
        }

        public void HandleMagnetPullInput(InputAction.CallbackContext context)
        {
            if (context.started)
                m_Inputs.e_OnMagnetPullStarted?.Invoke();

            if (context.canceled)
                m_Inputs.e_OnMagnetPullCancelled?.Invoke();
        }

        public void HandleMagnetPushInput(InputAction.CallbackContext context)
        {
            if (context.started)
                m_Inputs.e_OnMagnetPushPressed?.Invoke();
        }

        public void HandleOpenDevConsoleInput(InputAction.CallbackContext context)
        {
            if (context.started)
                m_Inputs.e_OnOpenDevConsoleKeyPressed?.Invoke();
        }

        public void HandleCloseDevConsoleInput(InputAction.CallbackContext context)
        {
            if (context.started)
                m_Inputs.e_OnCloseDevConsoleKeyPressed?.Invoke();
        }

        public void HandleEnterCommandInput(InputAction.CallbackContext context)
        {
            if (context.started)
                m_Inputs.e_OnEnterCommandKeyPressed?.Invoke();
        }

        public void HandleCameraZoomInput(InputAction.CallbackContext context)
        {
            if (context.started)
                m_Inputs.e_OnCameraZoomPressed?.Invoke();
            if (context.canceled)
                m_Inputs.e_OnCameraZoomCancelled?.Invoke();
        }
    }
}