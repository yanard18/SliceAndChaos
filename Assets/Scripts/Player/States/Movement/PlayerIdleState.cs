using DenizYanar.Events;
using JetBrains.Annotations;
using UnityEngine;
using DenizYanar.FSM;


namespace DenizYanar
{
    public class PlayerIdleState : State
    {
        private readonly Rigidbody2D _rb;

        private const float frictionAcceleration = 40.0f;
        
        public PlayerIdleState(Rigidbody2D rb, StringEventChannelSO nameInformerEvent = null, [CanBeNull] string stateName = null)
        {
            _rb = rb;
            _stateName = stateName ?? GetType().Name;
            _stateNameInformerEventChannel = nameInformerEvent;
        }

        public override void PhysicsTick()
        {
            var currentXVelocity = _rb.velocity.x;
            currentXVelocity = Mathf.MoveTowards(currentXVelocity, 0, Time.fixedDeltaTime * frictionAcceleration);
            _rb.velocity = new Vector2(currentXVelocity, _rb.velocity.y);
        }
        
    }
}
