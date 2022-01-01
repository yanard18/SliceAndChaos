using DenizYanar.FSM;
using UnityEngine;
using UnityEngine.InputSystem;


namespace DenizYanar
{
    public class PlayerAttackHookState : State
    {

        private KatanaProjectile _katana;
        
        private readonly PlayerAttackController _player;
        
        public PlayerAttackHookState(PlayerAttackController player)
        {
            _player = player;
        }

        public override void OnExit()
        {
            base.OnExit();
            if(_katana != null)
                _katana.CallbackKatana();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(_player.transform.position);


            _katana = _player.ThrowKatana(dir, 30f, 2000f);
        }
    }
}
