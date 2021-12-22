using DenizYanar.Events;
using JetBrains.Annotations;
using UnityEngine;

namespace DenizYanar
{
    public class PlayerDiveState : State
    {
        private readonly Rigidbody2D _rb;
        private float yForce;

        public PlayerDiveState(Rigidbody2D rb, StringEventChannelSO nameInformerEventChannel = null, [CanBeNull] string stateName = null)
        {
            _rb = rb;
            
            _stateNameInformerEventChannel = nameInformerEventChannel;
            _stateName = stateName ?? GetType().Name;
        }

        public override void Tick()
        {
            yForce = Input.GetKey(KeyCode.S) ? 1 : 0;
        }
        
        public override void PhysicsTick()
        {
            base.PhysicsTick();
            if(_rb.velocity.y > -20.0f)
                _rb.AddForce(Vector2.down * yForce);
        }
    }
}
