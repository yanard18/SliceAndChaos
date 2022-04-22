using UnityEngine;
using UnityEngine.Events;

namespace DenizYanar
{
    [CreateAssetMenu(menuName = "Player Inputs")]
    public class PlayerInputs : ScriptableObject
    {
        public float m_HorizontalMovement;
        public UnityAction e_OnDiveStarted;
        public UnityAction e_OnDiveCancelled;
        public UnityAction e_OnShiftStarted;
        public UnityAction e_OnJumpStarted;
        public UnityAction e_OnAttack1Started;
        public UnityAction e_OnAttack2Started;
        public UnityAction e_OnMagnetPullStarted;
        public UnityAction e_OnMagnetPullCancelled;
        public UnityAction e_OnMagnetPushPressed;
        public UnityAction e_OnOpenDevConsoleKeyPressed;
        public UnityAction e_OnCloseDevConsoleKeyPressed;
        public UnityAction e_OnEnterCommandKeyPressed;

    }
}
