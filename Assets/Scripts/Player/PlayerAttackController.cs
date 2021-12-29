using System;
using UnityEngine;
using DenizYanar.FSM;



namespace DenizYanar
{
    public class PlayerAttackController : MonoBehaviour
    {
        [SerializeField] private GameObject _katanaGameObject;

        private StateMachine _stateMachine;

        [HideInInspector] public bool IsSlashFinished = false;
        private void Awake()
        {
            _stateMachine = new StateMachine();

            PlayerAttackIdleState idle = new PlayerAttackIdleState();
            PlayerAttackSlashState slash = new PlayerAttackSlashState(this, _katanaGameObject);

            _stateMachine.InitState(idle);
            
            To(idle,slash,HasPressedMouse0());
            To(slash, idle, IsSwordSlashFinished());


            void To(State from, State to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);

            Func<bool> HasPressedMouse0() => () => Input.GetMouseButtonDown(0);
            Func<bool> IsSwordSlashFinished() => () => IsSlashFinished;

        }

        private void Update() => _stateMachine.Tick();
        

    }
}
