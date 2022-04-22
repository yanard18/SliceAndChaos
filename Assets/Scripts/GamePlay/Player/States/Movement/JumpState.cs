using System.Collections;
using DenizYanar.Events;
using DenizYanar.External.Sense_Engine.Scripts.Core;
using JetBrains.Annotations;
using UnityEngine;
using DenizYanar.FSM;

namespace DenizYanar.PlayerSystem.Movement
{
    public class JumpState : State
    {
        private readonly JumpProperties _jumpProperties;
        private readonly PlayerMovementController _playerMovementController;
        private readonly SenseEnginePlayer _jumpSense;

        #region Constructor

        public JumpState(PlayerMovementController playerMovementController, SenseEnginePlayer jumpSense, StringEventChannelSO nameInformerChannel = null, [CanBeNull] string stateName = null)
        {
            _playerMovementController = playerMovementController;
            _jumpProperties = playerMovementController.JumpPropertiesInstance;
            _stateName = stateName ?? GetType().Name;
            _stateNameInformerEventChannel = nameInformerChannel;
            _jumpSense = jumpSense;
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
            _jumpProperties.Rb.velocity = new Vector2(_jumpProperties.Rb.velocity.x, _jumpProperties.JumpForce);
            _jumpProperties.JumpCount--;
            _jumpSense.PlayIfExist();
            _playerMovementController.StartCoroutine(StartJumpCooldown(0.15f));
        }

        private IEnumerator StartJumpCooldown(float duration)
        {
            _jumpProperties.HasCooldown = true;
            yield return new WaitForSeconds(duration);
            _jumpProperties.HasCooldown = false;
        }

        #endregion
    }
    
    public class JumpProperties
    {
        private readonly int _maxJumpCount;
        
        public readonly float JumpForce;
        public readonly Rigidbody2D Rb;
        
        public int JumpCount;
        public bool HasCooldown;
        

        public bool CanJump => JumpCount > 0 && HasCooldown == false;

        public void ResetJumpCount() => JumpCount = _maxJumpCount;

        public JumpProperties(int maxJumpCount, float jumpForce, Rigidbody2D rb)
        {
            _maxJumpCount = maxJumpCount;
            JumpCount = _maxJumpCount;
            JumpForce = jumpForce;
            Rb = rb;
        }

    }
}
