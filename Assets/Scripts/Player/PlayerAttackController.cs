using System;
using UnityEngine;
using DenizYanar.FSM;



namespace DenizYanar
{
    public class PlayerAttackController : MonoBehaviour
    {
        [SerializeField] private GameObject _katanaGameObject;
        [SerializeField] private Projectile _katanaProjectile;
        
        
        private StateMachine _stateMachine;

        public State ActiveState => _stateMachine.CurrentState;

        [HideInInspector] public bool IsSlashFinished = false;
        private void Awake()
        {
            _stateMachine = new StateMachine();

            PlayerAttackIdleState idle = new PlayerAttackIdleState();
            PlayerAttackSlashState slash = new PlayerAttackSlashState(this, _katanaGameObject);
            PlayerAttackHookState hook = new PlayerAttackHookState(this);
            PlayerAttackShiftModeState shift = new PlayerAttackShiftModeState();

            _stateMachine.InitState(idle);
            
            To(idle,slash,HasPressedMouse0());
            To(slash, idle, IsSwordSlashFinished());
            To(idle, hook, HasPressedMouse1());
            To(hook, idle, HasPressedMouse1());


            void To(State from, State to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

            Func<bool> HasPressedMouse0() => () => Input.GetMouseButtonDown(0);
            Func<bool> HasPressedMouse1() => () => Input.GetMouseButtonDown(1);
            Func<bool> IsSwordSlashFinished() => () => IsSlashFinished;

        }

        private void Update() => _stateMachine.Tick();

        public KatanaProjectile ThrowKatana(Vector2 dir, float throwSpeed, float angularVelocity)
        {
            Projectile p = Instantiate(_katanaProjectile, transform.position, Quaternion.identity);
            p.Init(dir.normalized * throwSpeed, angularVelocity: angularVelocity, author: gameObject);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            p.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            return p.GetComponent<KatanaProjectile>();
        }
        

    }
}
