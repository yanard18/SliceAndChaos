using System;
using UnityEngine;

namespace DenizYanar
{
    [CreateAssetMenu(menuName = "Slice And Chaos/Player/Input System")]
    public class PlayerInput : ScriptableObject
    {
        public event Action JumpStarted;
        public event Action JumpReleased; 

        public event Action<float> HorizontalMovementPerformed;
        public event Action HorizontalMovementReleased; 

        public void InvokeJumpEvent() => JumpStarted?.Invoke();

        public void InvokeJumpReleased() => JumpReleased?.Invoke();
        
        public void InvokeHorizontalMovementEvent(float value) => HorizontalMovementPerformed?.Invoke(value);

        public void InvokeHorizontalMovementReleased() => HorizontalMovementReleased?.Invoke();
        

    }

}
