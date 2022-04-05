using DenizYanar.Events;
using DenizYanar.FSM;
using JetBrains.Annotations;
using UnityEngine;

namespace DenizYanar.PlayerSystem
{
    public class PlayerMovementMoveState : State
    {
        private readonly float _desiredXVelocity;
        private readonly float _acceleration;
        private readonly Rigidbody2D _rb;
        private readonly PlayerInputs _inputs;
        private readonly PlayerAnimationController _animationController;
        
        private float _xVelocity;


        #region Constructor

        public PlayerMovementMoveState(Rigidbody2D rb, PlayerSettings settings, PlayerInputs inputs, PlayerAnimationController animationController, StringEventChannelSO nameInformerEvent = null, [CanBeNull] string stateName = null)
        {
            _stateName = stateName ?? GetType().Name;
            _stateNameInformerEventChannel = nameInformerEvent;
            _rb = rb;
            _inputs = inputs;
            _animationController = animationController;

            _desiredXVelocity = settings.DesiredMovementVelocity;
            _acceleration = settings.MovementAcceleration;
        }

        #endregion

        #region State Callbacks

        public override void PhysicsTick() => Move();
        
        public override void OnEnter()
        {
            base.OnEnter();
            _xVelocity = _rb.velocity.x;
        }

        #endregion

        #region Local Methods

        private void Move()
        {
            var movementDirection = Mathf.Sign(_inputs.HorizontalMovement);
            
            
            _xVelocity = Mathf.MoveTowards(
                _xVelocity,
                _desiredXVelocity * movementDirection,
                Time.fixedDeltaTime * _acceleration);

            _animationController.HandleDirection(_xVelocity);
            
            _rb.velocity = new Vector2(_xVelocity, _rb.velocity.y);
        }

        #endregion


        

    }
}
