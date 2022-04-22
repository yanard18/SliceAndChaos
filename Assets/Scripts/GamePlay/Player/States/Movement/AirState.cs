using DenizYanar.Events;
using JetBrains.Annotations;
using UnityEngine;
using DenizYanar.FSM;
using DenizYanar.Inputs;

namespace DenizYanar.PlayerSystem.Movement
{
    public class AirState : State
    {

        private readonly Rigidbody2D _rb;
        private readonly PlayerInputs _inputs;

        private readonly float _xAcceleration;
        private readonly float _maxXVelocity;
        private readonly float _yAcceleration;
        private readonly float _maxYVelocity;

        private bool _dive;

        #region Constructor
        
        
        public AirState(Rigidbody2D rb, PlayerSettings settings, PlayerInputs inputs, StringEventChannelSO nameInformerChannel = null, [CanBeNull] string stateName = null)
        {
            _stateName = stateName ?? GetType().Name;
            _stateNameInformerEventChannel = nameInformerChannel;
            _rb = rb;
            _inputs = inputs;

            _xAcceleration = settings.AirStrafeXAcceleration;
            _maxXVelocity = settings.AirStrafeMaxXVelocity;
            _yAcceleration = settings.AirStrafeYAcceleration;
            _maxYVelocity = settings.AirStrafeMaxYVelocity;
        }

        #endregion
        
        #region State Callbacks

        public override void OnEnter()
        {
            base.OnEnter();
            _inputs.e_OnDiveStarted += OnDiveStarted;
            _inputs.e_OnDiveCancelled += OnDiveCancelled;
        }

        public override void OnExit()
        {
            _inputs.e_OnDiveStarted -= OnDiveStarted;
            _inputs.e_OnDiveCancelled -= OnDiveCancelled;
            _dive = false;
        }

        public override void PhysicsTick()
        {
            base.PhysicsTick();
            
            HandleAirStrafe();
            HandleDive();
        }

        private void HandleAirStrafe()
        {
            var horizontalKeyInput = _inputs.m_HorizontalMovement;

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
        }

        private void HandleDive()
        {
            // Y
            if (VerticalSpeedLessThanMax() && PressedToDiveKey())
                _rb.AddForce(new Vector2(0, _yAcceleration), ForceMode2D.Force);
        }

        private bool VerticalSpeedLessThanMax() => Mathf.Abs(_rb.velocity.y) < _maxYVelocity;

        private bool PressedToDiveKey() => _dive;

        #endregion


        private void OnDiveStarted() => _dive = true;
        private void OnDiveCancelled() => _dive = false;

    }
}
