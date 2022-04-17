using System;
using System.Collections;
using DenizYanar.External.Sense_Engine.Scripts.Core;
using UnityEngine;
using DenizYanar.FSM;

namespace DenizYanar.PlayerSystem
{
    public class PlayerAttackController : MonoBehaviour
    {
        #region Private Variables

        private StateMachine _stateMachine;
        private Rigidbody2D _rb;
        private bool _hasAttackCooldown;
        

        private PlayerAttackSlashState _slash;
        private PlayerAttackSwordThrowState _throw;
        private PlayerAttackWaitSwordState _wait;
        private PlayerAttackIdleState _idle;

        #endregion

        #region Serialized Variables
        
        [SerializeField] private PlayerSettings _settings;
        [SerializeField] private PlayerInputs _inputs;

        [Header("Slash Sense Players")]
        [SerializeField] private SenseEnginePlayer _attackSensePlayer;
        [SerializeField] private SenseEnginePlayer _hitSensePlayer;

        #endregion
        
        #region Public Variables

        #endregion


        #region Monobehaviour

        private void OnEnable()
        {
            _inputs.OnAttack1Started += OnAttack1Pressed;
            _inputs.OnAttack2Started += OnAttack2Pressed;
        }

        private void OnDisable()
        {
            _inputs.OnAttack1Started -= OnAttack1Pressed;
            _inputs.OnAttack2Started -= OnAttack2Pressed;
        }

        private void Awake()
        {
            _stateMachine = new StateMachine();
            _rb = GetComponent<Rigidbody2D>();

            _idle = new PlayerAttackIdleState();
            _slash = new PlayerAttackSlashState(this, _settings, CreateAttackCooldown, _rb, _attackSensePlayer, _hitSensePlayer);
            _throw = new PlayerAttackSwordThrowState(ThrowKatana, OnSwordCalled, OnSwordReturned, transform, _settings, _inputs);
            _wait = new PlayerAttackWaitSwordState();

            _stateMachine.InitState(_idle);
            
            To(_idle,_slash,() => false);
            To(_idle, _throw, () => false);
            To(_throw, _wait, () => false);
            To(_wait, _idle, () => false);
            To(_slash, _idle, HasNotAttackCooldown());
            
            void To(State from, State to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            
            Func<bool> HasNotAttackCooldown() => () => !_hasAttackCooldown;
        }

        private void Update() => _stateMachine.Tick();

        #endregion

        #region Inputs

        private void OnAttack1Pressed()
        {
            _stateMachine.TriggerState(_slash);
        }

        private void OnAttack2Pressed()
        {
           /* if(_stateMachine.TriggerState(_wait))
                return;*/

            _stateMachine.TriggerState(_throw);
        }

        private void OnSwordCalled()
        {
            _stateMachine.TriggerState(_wait);
        }

        private void OnSwordReturned()
        {
            _stateMachine.TriggerState(_idle);
        }

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

        private void CreateAttackCooldown(float duration) => StartCoroutine(AttackCooldownCoroutine(duration));
        
        private IEnumerator AttackCooldownCoroutine(float duration)
        {
            _hasAttackCooldown = true;
            yield return new WaitForSeconds(duration);
            _hasAttackCooldown = false;
        }

        #endregion
        
        

    }
}
