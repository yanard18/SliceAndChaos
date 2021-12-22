using DenizYanar.Events;
using JetBrains.Annotations;
using UnityEngine;

namespace DenizYanar
{
    public class PlayerMoveState : State
    {


        
        private readonly float _desiredXVelocity;
        private readonly float _acceleration;
        
        private float _xVelocity;
        
        private readonly Rigidbody2D _rb;

        public PlayerMoveState(Rigidbody2D rb, PlayerSettings settings, StringEventChannelSO nameInformerEvent = null, [CanBeNull] string stateName = null)
        {
            _stateName = stateName ?? GetType().Name;
            _stateNameInformerEventChannel = nameInformerEvent;
            _rb = rb;

            _desiredXVelocity = settings.DesiredMovementVelocity;
            _acceleration = settings.MovementAcceleration;
        }


        public override void PhysicsTick()
        {
            var movementDirection = Mathf.Sign(Input.GetAxisRaw("Horizontal"));
            
            _xVelocity = Mathf.MoveTowards(
                _xVelocity, 
                _desiredXVelocity * movementDirection, 
                Time.fixedDeltaTime * _acceleration);
            
            
            _rb.velocity = new Vector2(_xVelocity, _rb.velocity.y);
            
        }
        
        
        public override void OnEnter()
        {
            base.OnEnter();
            _xVelocity = _rb.velocity.x;
        }

    }
}
