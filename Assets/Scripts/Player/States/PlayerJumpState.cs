

using UnityEngine;

namespace DenizYanar
{
    public class PlayerJumpState : State
    {
        private readonly Rigidbody2D _rb;
        private readonly float _jumpForce;

        public PlayerJumpState(Rigidbody2D rb, float jumpForce)
        {
            _rb = rb;
            _jumpForce = jumpForce;
        }
        
        public override void Tick()
        {
            Debug.Log("jump");
        }

        public override void PhysicsTick()
        {
            
        }

        public override void OnEnter()
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        }

        public override void OnExit()
        {
            
        }
    }
}
