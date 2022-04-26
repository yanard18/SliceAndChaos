using System;
using DenizYanar.FSM;
using DenizYanar.Inputs;
using UnityEngine;


namespace DenizYanar.PlayerSystem.Attacks
{
    public class ThrowSwordState : State
    {

        private readonly Func<Vector2, float, float, KatanaProjectile> _throwKatana;
        private readonly Action _onCalled;
        private readonly Action _onReturned;
        private readonly Transform _playerTransform;
        private readonly PlayerConfigurations m_Configurations;
        private readonly PlayerInputs _inputs;
        
        private KatanaProjectile _katana;

        #region Constructor

        public ThrowSwordState(
            Func<Vector2, float, float, KatanaProjectile> throwKatana,
            Action onCalled,
            Action onReturned,
            Transform playerTransform,
            PlayerConfigurations configurations,
            PlayerInputs inputs
            )
        {
            _inputs = inputs;
            _throwKatana = throwKatana;
            _onCalled = onCalled;
            _onReturned = onReturned;
            _playerTransform = playerTransform;
            m_Configurations = configurations;
        }

        #endregion

        #region State Callbacks

        public override void OnExit()
        {
            base.OnExit();
            _inputs.e_OnAttack2Started -= CallRequest;
            
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            _inputs.e_OnAttack2Started += CallRequest;
            
            if (Camera.main is null) return;
            var dir = _inputs.m_MousePosition - (Vector2)Camera.main.WorldToScreenPoint(_playerTransform.position);

            

            _katana = _throwKatana(dir, m_Configurations.SwordThrowSpeed, m_Configurations.SwordThrowAngularVelocity);
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
