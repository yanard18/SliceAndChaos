using System.Collections;
using DenizYanar.Events;
using DenizYanar.SenseEngine;
using JetBrains.Annotations;
using UnityEngine;
using DenizYanar.FSM;
using DenizYanar.Movement;

namespace DenizYanar.PlayerSystem.Movement
{
    public class JumpState : State
    {
        private readonly JumpData m_JumpData;
        private readonly PlayerMovementController m_MovementController;
        private readonly SenseEnginePlayer m_sepJump;
        private readonly IMove m_iJump;

        #region Constructor

        public JumpState(PlayerMovementController movementController, SenseEnginePlayer sepJump, StringEvent nameInformerChannel = null, [CanBeNull] string stateName = null)
        {
            m_MovementController = movementController;
            m_JumpData = movementController.s_JumpDataInstance;
            m_StateName = stateName ?? GetType().Name;
            m_ecStateName = nameInformerChannel;
            m_sepJump = sepJump;
        }

        #endregion

        #region State Callbacks

        public override void OnEnter()
        {
            base.OnEnter();
            Jump();
        }

        #endregion

        #region Local Methods

        private void Jump()
        {
            m_JumpData.m_Rb.velocity = new Vector2(m_JumpData.m_Rb.velocity.x, m_JumpData.m_JumpForce);
            m_JumpData.m_nAvailableJump--;
            m_sepJump.PlayIfExist();
            m_MovementController.StartCoroutine(StartJumpCooldown(0.15f));
        }

        private IEnumerator StartJumpCooldown(float duration)
        {
            m_JumpData.m_bHasCooldown = true;
            yield return new WaitForSeconds(duration);
            m_JumpData.m_bHasCooldown = false;
        }

        #endregion
    }
    
    public class JumpData
    {
        private readonly int m_nMaxJump;
        
        public readonly float m_JumpForce;
        public readonly Rigidbody2D m_Rb;
        
        public int m_nAvailableJump;
        public bool m_bHasCooldown;
        

        public bool CanJump => m_nAvailableJump > 0 && m_bHasCooldown == false;

        public void ResetJumpCount() => m_nAvailableJump = m_nMaxJump;

        public JumpData(int nMaxJump, float jumpForce, Rigidbody2D rb)
        {
            m_nMaxJump = nMaxJump;
            m_nAvailableJump = m_nMaxJump;
            m_JumpForce = jumpForce;
            m_Rb = rb;
        }

    }
}
