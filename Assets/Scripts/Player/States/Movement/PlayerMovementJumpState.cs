using System.Collections;
using DenizYanar.Events;
using JetBrains.Annotations;
using UnityEngine;
using DenizYanar.FSM;

namespace DenizYanar
{
    public class PlayerMovementJumpState : State
    {
        private readonly JumpData _jumpData;
        private readonly PlayerMovementController _playerMovementController;

        public PlayerMovementJumpState(PlayerMovementController playerMovementController, StringEventChannelSO nameInformerChannel = null, [CanBeNull] string stateName = null)
        {
            _playerMovementController = playerMovementController;
            _jumpData = playerMovementController.JumpDataInstance;
            _stateName = stateName ?? GetType().Name;
            _stateNameInformerEventChannel = nameInformerChannel;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Jump();
        }

        private void Jump()
        {
            _jumpData.RB.velocity = new Vector2(_jumpData.RB.velocity.x, _jumpData.JumpForce);
            _jumpData.JumpCount--;
            _playerMovementController.StartCoroutine(StartJumpCooldown(0.15f));
        }

        private IEnumerator StartJumpCooldown(float duration)
        {
            _jumpData.HasCooldown = true;
            yield return new WaitForSeconds(duration);
            _jumpData.HasCooldown = false;
        }

    }
    
    public class JumpData
    {
        private readonly int _maxJumpCount;
        
        
        public int JumpCount;
        public bool HasCooldown = false;
        public readonly float JumpForce;
        public readonly Rigidbody2D RB;

        public bool CanJump => JumpCount > 0 && HasCooldown == false;

        public void ResetJumpCount() => JumpCount = _maxJumpCount;

        public JumpData(int maxJumpCount, float jumpForce, Rigidbody2D rb)
        {
            _maxJumpCount = maxJumpCount;
            JumpCount = _maxJumpCount;
            JumpForce = jumpForce;
            RB = rb;
        }

    }
}
