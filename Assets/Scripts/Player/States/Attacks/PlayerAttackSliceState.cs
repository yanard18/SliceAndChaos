using DenizYanar.FSM;
using UnityEngine;

namespace DenizYanar
{
    public class PlayerAttackSliceState : State
    {
        private readonly Rigidbody2D _rb;
        private const float _dashSpeed = 100.0f;
        private Vector2 _movementDirection;

        public PlayerAttackSliceState(Rigidbody2D rb)
        {
            _rb = rb;
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            _movementDirection = _rb.velocity.normalized;
            _rb.velocity = _movementDirection * _dashSpeed;
            Debug.Log("SLICE");
        }
    }
}