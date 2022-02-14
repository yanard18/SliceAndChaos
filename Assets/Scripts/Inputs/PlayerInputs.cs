using UnityEngine;
using UnityEngine.Events;

namespace DenizYanar
{
    [CreateAssetMenu(menuName = "Player Inputs")]
    public class PlayerInputs : ScriptableObject
    {
        public float HorizontalMovement;
        public UnityAction OnDiveStarted;
        public UnityAction OnDiveCancelled;
        public UnityAction OnShiftStarted;
        public UnityAction OnJumpStarted;
        public UnityAction OnAttack1Started;
        public UnityAction OnAttack2Started;
        public UnityAction OnTelekinesisStarted;
        public UnityAction OnTelekinesisCancelled;
        
    }
}
