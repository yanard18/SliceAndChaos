using DenizYanar.Events;
using JetBrains.Annotations;
using UnityEngine;
using DenizYanar.FSM;


namespace DenizYanar
{
    public class PlayerShiftState : State
    {
        private readonly Rigidbody2D _rb;
        private readonly float _originalGravity;
        private readonly float _speed;
        private readonly float _turnSpeed;
        

        public PlayerShiftState(Rigidbody2D rb, PlayerSettings settings, StringEventChannelSO nameInformerEvent = null, [CanBeNull] string stateName = null)
        {
            _stateNameInformerEventChannel = nameInformerEvent;
            _stateName = stateName;
            
            _rb = rb;
            _originalGravity = rb.gravityScale;
            _speed = settings.ShiftModeSpeed;
            _turnSpeed = settings.ShiftModeTurnSpeed;
        }

        public override void PhysicsTick()
        {
            base.PhysicsTick(); 
            MoveForward();
            SetAngle();
        }

        private void MoveForward()
        {
            _rb.velocity = _rb.transform.right * (_speed * Time.fixedDeltaTime);
        }


        public override void OnEnter()
        {
            base.OnEnter();
            

            SetAngleInstant();

            _rb.gravityScale = 0;
            _rb.freezeRotation = false;


        }
        

        public override void OnExit()
        {
            base.OnExit();
            _rb.gravityScale = _originalGravity;
            _rb.velocity *= 1.5f;
            _rb.rotation = 0;
            _rb.freezeRotation = true;
        }
        
        
        private void SetAngle()
        {
            if (Camera.main is null) return;
            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(_rb.transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            _rb.rotation = Mathf.MoveTowardsAngle(_rb.rotation, angle, Time.fixedDeltaTime * _turnSpeed);
        }
        
        private void SetAngleInstant()
        {
            if (Camera.main is null) return;
            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(_rb.transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            _rb.rotation = angle;
        }
    }
    
}