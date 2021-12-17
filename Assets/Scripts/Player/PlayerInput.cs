using UnityEngine;
using UnityEngine.InputSystem;

namespace DenizYanar
{
    [CreateAssetMenu(menuName = "Slice And Chaos/Player/Input System")]
    public class PlayerInput : ScriptableObject
    {
        public float HorizontalMovement;
        public InputAction.CallbackContext Jump;

    }
}
