using System;
using DenizYanar.FSM;
using UnityEngine;


namespace DenizYanar.Player
{
    public class PlayerAttackSwordThrowState : State
    {

        private readonly Func<Vector2, float, float, KatanaProjectile> _throwKatana;
        private readonly Transform _playerTransform;
        private readonly PlayerSettings _settings;
        
        private KatanaProjectile _katana;

        #region Constructor

        public PlayerAttackSwordThrowState(Func<Vector2, float, float, KatanaProjectile> throwKatana, Transform playerTransform, PlayerSettings settings)
        {
            _throwKatana = throwKatana;
            _playerTransform = playerTransform;
            _settings = settings;
        }

        #endregion

        #region State Callbacks

        public override void OnExit()
        {
            base.OnExit();
            if(_katana != null)
                _katana.CallbackKatana();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            if (Camera.main is null) return;
            var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(_playerTransform.position);


            _katana = _throwKatana(dir, _settings.SwordThrowSpeed, _settings.SwordThrowAngularVelocity);
        }

        #endregion
        
        
        
    }
}
