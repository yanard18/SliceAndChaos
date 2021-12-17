using TMPro;
using UnityEngine;

namespace DenizYanar
{
    public class PlayerIdleState : State
    {
        private readonly Rigidbody2D _rb;
        
        private const float frictionAcceleration = 20.0f;
        
        public PlayerIdleState(Rigidbody2D rb)
        {
            _rb = rb;
        }
        
        public override void Tick()
        {
            Debug.Log("Idle");
        }

        public override void PhysicsTick()
        {
            var currentXVelocity = _rb.velocity.x;
            currentXVelocity = Mathf.MoveTowards(currentXVelocity, 0, Time.fixedDeltaTime * frictionAcceleration);
            _rb.velocity = new Vector2(currentXVelocity, _rb.velocity.y);
        }

        public override void OnEnter()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}
