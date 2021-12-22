using DenizYanar.Events;
using JetBrains.Annotations;
using UnityEngine;

namespace DenizYanar
{
    public class PlayerAirStrafeState : State
    {
        private const float _airStrafeSpeed = 100.0f;

        private readonly Rigidbody2D _rb;
        private readonly Player _player;


        public PlayerAirStrafeState(Rigidbody2D rb, Player player, StringEventChannelSO nameInformerEventChannel = null, [CanBeNull] string stateName = null)
        {
            _rb = rb;
            _player = player;
            _stateNameInformerEventChannel = nameInformerEventChannel;
            _stateName = stateName ?? GetType().Name;
        }

        public override void PhysicsTick()
        {
            _rb.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * _airStrafeSpeed, 0), ForceMode2D.Force);
        }
        
    }
}
