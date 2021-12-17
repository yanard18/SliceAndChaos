using UnityEngine;

namespace DenizYanar
{
    public class PlayerAirStrafeState : State
    {
        private const float _airStrafeSpeed = 100.0f;

        private readonly Rigidbody2D _rb;
        private readonly PlayerInput _input;
        
        public PlayerAirStrafeState(Rigidbody2D rb, PlayerInput input)
        {
            _rb = rb;
            _input = input;
        }
        
        public override void Tick()
        {
            //Debug.Log("Air Strafing");
        }

        public override void PhysicsTick()
        {
            _rb.AddForce(new Vector2(_input.HorizontalMovement * _airStrafeSpeed, 0), ForceMode2D.Force);
        }

        public override void OnEnter()
        {
            
        }

        public override void OnExit()
        {
            
        }
    }
}
