using DenizYanar.FSM;
using DenizYanar.YanarPro;
using UnityEngine;

namespace DenizYanar.PlayerSystem.Movement
{
    public class TeleportState : State
    {

        private readonly Rigidbody2D _rb;
        private readonly PlayerSettings _settings;
        private readonly float _speedReductionAfterTeleport;
        
        public bool HasFinished;

        #region Constructor

        public TeleportState(Rigidbody2D rb, PlayerSettings settings)
        {
            _rb = rb;
            _settings = settings;
            _speedReductionAfterTeleport = settings.SliceSpeedReductionAfterTeleport;
        }

        #endregion
        
        #region State Callbacks

        public override void OnEnter()
        {
            base.OnEnter();
            var velocityDirection = _rb.velocity.normalized;
            var desiredPosition = _rb.position + velocityDirection * _settings.SliceTeleportDistance;




            if (IsThereObstacleBetweenPositions(_rb.position, desiredPosition))
                _rb.MovePosition(desiredPosition);
            else
                _rb.position = desiredPosition;

            HasFinished = true;
        }

        private bool IsThereObstacleBetweenPositions(Vector2 startPos, Vector2 endPos)
        {
            var dir= YanarUtils.FindDisplacementBetweenTwoPosition(startPos, endPos);
            var hit = Physics2D.Raycast(startPos, dir.normalized, dir.magnitude, _settings.ObstacleLayerMask);
            return hit;
        }

        public override void OnExit()
        {
            _rb.velocity /= _speedReductionAfterTeleport;
            HasFinished = false;
        }

        #endregion
        
        
    }
}