using System;
using UnityEngine;
using DenizYanar.FSM;



namespace DenizYanar.Player
{
    public class PlayerAttackController : MonoBehaviour
    {
        private StateMachine _stateMachine;
        
        [SerializeField] private GameObject _katanaGameObject;
        [SerializeField] private PlayerSettings _settings;
        
        public bool IsSwordTurnedBack { get; set; }


        #region Monobehaviour
        
        private void Awake()
        {
            _stateMachine = new StateMachine();

            var idle = new PlayerAttackIdleState();
            var slash = new PlayerAttackSlashState(this, _katanaGameObject);
            var swordThrow = new PlayerAttackSwordThrowState(ThrowKatana, transform, _settings);
            var waitSword = new PlayerAttackWaitSwordState(this);

            _stateMachine.InitState(idle);
            
            To(idle,slash,HasPressedMouse0());
            To(slash, idle, IsSwordSlashFinished());
            To(idle, swordThrow, HasPressedMouse1());
            To(swordThrow, waitSword, HasPressedMouse1());
            To(waitSword, idle, WhenSwordReturned());


            void To(State from, State to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

            Func<bool> HasPressedMouse0() => () => Input.GetMouseButtonDown(0);
            Func<bool> HasPressedMouse1() => () => Input.GetMouseButtonDown(1);
            Func<bool> IsSwordSlashFinished() => () => slash.IsFinished;
            Func<bool> WhenSwordReturned() => () => IsSwordTurnedBack;

        }

        private void Update() => _stateMachine.Tick();
        

        #endregion
        
        #region Method Referances
        
        private KatanaProjectile ThrowKatana(Vector2 dir, float throwSpeed, float angularVelocity)
        {
            var p = Instantiate(_settings.SwordProjectile, transform.position, Quaternion.identity);
            p.Init(dir.normalized * throwSpeed, angularVelocity: angularVelocity, author: gameObject, lifeTime: 0f);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            p.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            return p.GetComponent<KatanaProjectile>();
        }

        #endregion
        
        

    }
}
