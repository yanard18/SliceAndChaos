using System.Collections;
using DenizYanar.FSM;
using UnityEngine;

namespace DenizYanar
{
    public class PlayerMovementSliceState : State
    {

        private readonly Rigidbody2D _rb;
        private const float _teleportDistance = 15.0f;
        private const float _speedReductionAfterTeleport = 2.0f;
        public bool HasFinished;

        public PlayerMovementSliceState(Rigidbody2D rb)
        {
            _rb = rb;
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            Vector2 direction = _rb.velocity.normalized;
            _rb.MovePosition(_rb.position + direction * _teleportDistance);
            HasFinished = true;
        }

        public override void OnExit()
        {
            _rb.velocity /= _speedReductionAfterTeleport;
            HasFinished = false;
        }
        
    }
}