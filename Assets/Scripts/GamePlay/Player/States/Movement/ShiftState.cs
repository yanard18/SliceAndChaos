using DenizYanar.Events;
using DenizYanar.SenseEngine;
using JetBrains.Annotations;
using UnityEngine;
using DenizYanar.FSM;
using DenizYanar.Inputs;
using DenizYanar.YanarPro;


namespace DenizYanar.PlayerSystem.Movement
{
    public class ShiftState : State
    {
        private readonly Rigidbody2D _rb;
        private readonly PlayerInputs _inputs;
        private readonly SenseEnginePlayer _enterShiftSense;
        private readonly SenseEnginePlayer _leaveShiftSense;
        private readonly float _originalGravity;
        private readonly float _speed;
        private readonly float _turnSpeed;


        #region Constructor

        public ShiftState(Rigidbody2D rb,
            PlayerInputs inputs,
            PlayerSettings settings,
            SenseEnginePlayer enterShiftSense,
            SenseEnginePlayer leaveShiftSense,
            StringEvent nameInformerEvent = null,
            [CanBeNull] string stateName = null)
        {
            m_ecStateName = nameInformerEvent;
            m_StateName = stateName;

            _rb = rb;
            _inputs = inputs;
            _originalGravity = rb.gravityScale;
            _speed = settings.ShiftModeSpeed;
            _enterShiftSense = enterShiftSense;
            _leaveShiftSense = leaveShiftSense;
            _turnSpeed = settings.ShiftModeTurnSpeed;
        }

        #endregion

        #region State Callbacks

        public override void PhysicsTick()
        {
            base.PhysicsTick();
            MoveForward();
            SetAngle();
        }

        public override void OnEnter()
        {
            base.OnEnter();

            SetAngleInstant();
            _rb.gravityScale = 0;
            _rb.freezeRotation = false;
            _enterShiftSense.PlayIfExist();
        }

        public override void OnExit()
        {
            base.OnExit();
            _rb.gravityScale = _originalGravity;
            _rb.rotation = 0;
            _rb.freezeRotation = true;
            _leaveShiftSense.PlayIfExist();
            GiveSpeedBoost(1.5f);
        }

        private void GiveSpeedBoost(float boostValue) => _rb.velocity *= boostValue;

        #endregion

        #region Local Methods

        private void MoveForward()
        {
            _rb.velocity = _rb.transform.right * (_speed * Time.fixedDeltaTime);
        }

        private void SetAngle()
        {
            var angle = YanarUtils.FindAngleBetweenPositionAndMouse(_rb.transform.position, _inputs.m_MousePosition);
            _rb.rotation = Mathf.MoveTowardsAngle(_rb.rotation, angle, Time.fixedDeltaTime * _turnSpeed);
        }

        private void SetAngleInstant()
        {
            var angle = YanarUtils.FindAngleBetweenPositionAndMouse(_rb.transform.position, _inputs.m_MousePosition);
            _rb.rotation = angle;
        }

        #endregion
    }
}