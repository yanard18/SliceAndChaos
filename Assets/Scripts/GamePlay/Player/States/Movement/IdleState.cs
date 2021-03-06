using DenizYanar.Events;
using JetBrains.Annotations;
using UnityEngine;
using DenizYanar.FSM;


namespace DenizYanar.PlayerSystem.Movement
{
    public class IdleState : State
    {
        private const float FRICTION_ACCELERATION = 40.0f;
        
        private readonly Rigidbody2D _rb;


        #region Constructor

        public IdleState(Rigidbody2D rb, StringEvent nameInformerEvent = null, [CanBeNull] string stateName = null)
        {
            _rb = rb;
            m_StateName = stateName ?? GetType().Name;
            m_ecStateName = nameInformerEvent;
        }

        #endregion
        
        #region State Callbacks
        
        public override void PhysicsTick() => SlowDown();

        private void SlowDown()
        {
            var currentXVelocity = _rb.velocity.x;
            currentXVelocity = Mathf.MoveTowards(currentXVelocity, 0, Time.fixedDeltaTime * FRICTION_ACCELERATION);
            _rb.velocity = new Vector2(currentXVelocity, _rb.velocity.y);
        }

        #endregion
        
        
    }
}
