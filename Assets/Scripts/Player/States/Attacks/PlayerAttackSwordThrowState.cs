using System;
using DenizYanar.FSM;
using UnityEngine;
using UnityEngine.InputSystem;


namespace DenizYanar.PlayerSystem
{
    public class PlayerAttackSwordThrowState : State
    {

        private readonly Func<Vector2, float, float, KatanaProjectile> _throwKatana;
        private readonly Action _onCalled;
        private readonly Action _onReturned;
        private readonly Transform _playerTransform;
        private readonly PlayerSettings _settings;
        private readonly PlayerInputs _inputs;
        
        private KatanaProjectile _katana;

        #region Constructor

        public PlayerAttackSwordThrowState(
            Func<Vector2, float, float, KatanaProjectile> throwKatana,
            Action onCalled,
            Action onReturned,
            Transform playerTransform,
            PlayerSettings settings,
            PlayerInputs inputs
            )
        {
            _inputs = inputs;
            _throwKatana = throwKatana;
            _onCalled = onCalled;
            _onReturned = onReturned;
            _playerTransform = playerTransform;
            _settings = settings;
        }

        #endregion

        #region State Callbacks

        public override void OnExit()
        {
            base.OnExit();
            _inputs.OnAttack2Started -= CallRequest;
            
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            _inputs.OnAttack2Started += CallRequest;
            
            if (Camera.main is null) return;
            var dir = Mouse.current.position.ReadValue() - (Vector2)Camera.main.WorldToScreenPoint(_playerTransform.position);

            

            _katana = _throwKatana(dir, _settings.SwordThrowSpeed, _settings.SwordThrowAngularVelocity);
            _katana.SetOnSwordReturned(_onReturned);
            _katana.SetOnSwordCalled(_onCalled);
        }


        private void CallRequest()
        {
            if(_katana != null)
                _katana.CallbackKatana();
        }
        #endregion
        
        
        
    }
}
