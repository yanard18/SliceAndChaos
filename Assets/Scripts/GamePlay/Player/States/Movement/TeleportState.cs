using DenizYanar.FSM;
using DenizYanar.YanarPro;
using UnityEngine;

namespace DenizYanar.PlayerSystem.Movement
{
    public class TeleportState : State
    {
        private readonly Rigidbody2D m_Rb;
        private readonly PlayerConfigurations m_Configurations;
        private readonly float m_SpeedReductionAfterTeleport;
        
        public bool m_bHasFinished;

        #region Constructor

        public TeleportState(Rigidbody2D rb, PlayerConfigurations configurations)
        {
            m_Rb = rb;
            m_Configurations = configurations;
            m_SpeedReductionAfterTeleport = configurations.SliceSpeedReductionAfterTeleport;
        }

        #endregion
        
        #region State Callbacks

        public override void OnEnter()
        {
            base.OnEnter();
            var velocityDirection = m_Rb.velocity.normalized;
            var desiredPosition = m_Rb.position + velocityDirection * m_Configurations.SliceTeleportDistance;

            if (IsThereObstacleBetweenPositions(m_Rb.position, desiredPosition))
                m_Rb.MovePosition(desiredPosition);
            else
                m_Rb.position = desiredPosition;

            m_bHasFinished = true;
        }

        private bool IsThereObstacleBetweenPositions(Vector2 startPos, Vector2 endPos)
        {
            var dir= YanarUtils.FindDisplacementBetweenTwoPosition(startPos, endPos);
            var hit = Physics2D.Raycast(startPos, dir.normalized, dir.magnitude, m_Configurations.ObstacleLayerMask);
            return hit;
        }

        public override void OnExit()
        {
            m_Rb.velocity /= m_SpeedReductionAfterTeleport;
            m_bHasFinished = false;
        }

        #endregion
        
    }
}