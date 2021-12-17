using UnityEngine;

namespace DenizYanar
{
    public class PlayerMoveState : State
    {

        private const float _desiredXVelocity = 20.0f;
        private const float _acceleration = 30.0f;
        
        private float _xVelocity;

        private readonly PlayerInput _input;
        private readonly Rigidbody2D _rb;
        
        public PlayerMoveState(PlayerInput input, Rigidbody2D rb)
        {
            _input = input;
            _rb = rb;
        }
        
        public override void Tick()
        {
            Debug.Log("MOVE");
        }

        public override void PhysicsTick()
        {
            var movementDirection = Mathf.Sign(_input.HorizontalMovement);
            
            _xVelocity = Mathf.MoveTowards(
                _xVelocity, 
                _desiredXVelocity * movementDirection, 
                Time.fixedDeltaTime * _acceleration);
            
            
            _rb.velocity = new Vector2(_xVelocity, _rb.velocity.y);
        }

        public override void OnEnter()
        {
            _xVelocity = _rb.velocity.x;
        }

        public override void OnExit()
        {
            
        }
    }
}
