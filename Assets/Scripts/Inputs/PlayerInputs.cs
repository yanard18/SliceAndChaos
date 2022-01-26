using UnityEngine;
using UnityEngine.Events;

namespace DenizYanar
{
    [CreateAssetMenu(menuName = "Player Inputs")]
    public class PlayerInputs : ScriptableObject
    {
        public float HorizontalMovement;
        public bool Jump;
        public bool Shift;
        public bool Dive;
        public bool Attack1;
        public bool Attack2;
        public bool Telekinesis;
        public UnityAction OnDiveStarted;
        public UnityAction OnDiveCancelled;
        public UnityAction OnShiftStarted;
        public UnityAction OnJumpStarted;
        public UnityAction OnAttack1Started;
        public UnityAction OnAttack2Started;
        public UnityAction OnTelekinesisStarted;
        public UnityAction OnTelekinesisCancelled; 


        public void ResetAllInputs()
        {
            Jump = false;
            Shift = false;
            Dive = false;
            Attack1 = false;
            Attack2 = false;
            Telekinesis = false;
        }
    }
}
