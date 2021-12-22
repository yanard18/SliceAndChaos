using DenizYanar.Events;
using JetBrains.Annotations;
using UnityEngine;

namespace DenizYanar
{
    public class PlayerAirState : State
    {

        private readonly Rigidbody2D _rb;
        
        private readonly float _xAcceleration;
        private readonly float _maxXVelocity;
        private readonly float _yAcceleration;
        private readonly float _maxYVelocity;

        public PlayerAirState(Rigidbody2D rb, PlayerSettings settings, StringEventChannelSO nameInformerChannel = null, [CanBeNull] string stateName = null)
        {
            _stateName = stateName ?? GetType().Name;
            _stateNameInformerEventChannel = nameInformerChannel;
            _rb = rb;

            _xAcceleration = settings.AirStrafeXAcceleration;
            _maxXVelocity = settings.AirStrafeMaxXVelocity;
            _yAcceleration = settings.AirStrafeYAcceleration;
            _maxYVelocity = settings.AirStrafeMaxYVelocity;

        }
        

        public override void PhysicsTick()
        {
            base.PhysicsTick();
            
            var sKeyInput = Input.GetAxisRaw("Vertical") == -1 ? 1 : 0;
            var horizontalKeyInput = Input.GetAxisRaw("Horizontal");
            
            // X


            switch (horizontalKeyInput)
            {
                //+x
                case 1:
                {
                    if (_rb.velocity.x < _maxXVelocity)
                        _rb.AddForce(new Vector2(_xAcceleration, 0), ForceMode2D.Force);
                    break;
                }
                //-x
                case -1:
                {
                    if (_rb.velocity.x > -_maxXVelocity)
                        _rb.AddForce(new Vector2(-_xAcceleration, 0), ForceMode2D.Force);
                    break;
                }
            }



            // Y
            if(Mathf.Abs(_rb.velocity.y) < _maxYVelocity)
                _rb.AddForce(new Vector2(0, sKeyInput * _yAcceleration), ForceMode2D.Force);
        }
    }
}
